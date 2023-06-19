using event_booking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using event_booking.Models;
using event_booking.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using SendGrid.Helpers.Mail;

namespace event_booking.Controllers.EventSystem
{
    public class ESTicketsPageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ESTicketsPageController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string button, int id, int? sectionId = null, int? groupDiscountId = null, Dictionary<int, int> discountTicketCounts = null)
        {
            var currentUserId = _userManager.GetUserId(User);
            Purchase purchase;

            // Get the PurchaseId from the session, if any
            var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

            if (purchaseId.HasValue)
            {
                // If the PurchaseId exists in the session, find the Purchase
                purchase = await _context.Purchases.FindAsync(purchaseId.Value);
            }
            else
            {
                // If the PurchaseId does not exist in the session, create a new Purchase
                purchase = new Purchase();
                // Set the session expiry time to the current time plus 5 minutes
                purchase.SessionExpiryTime = DateTime.UtcNow.AddMinutes(5);
                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("PurchaseId", purchase.PurchaseId);
            }

            var ticketsForEvent = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Venue)
                .Include(t => t.Seat)
                .Include(t => t.Seat.Section) // Include the Section of the Seat
                .Include(t => t.Discount)
                .Include(t => t.TicketType)
                .Where(t => t.EventId == id && (t.PurchaseId == null || t.PurchaseId == purchaseId))
                .ToListAsync();

            ViewBag.EventName = ticketsForEvent[0].Event.Name;
            ViewBag.VenueName = ticketsForEvent[0].Venue.Name;

            //
            //SECTION
            //Section dropdown
            var sections = await _context.Sections.ToListAsync();
            ViewBag.Sections = sections;

            // If a section has been selected, filter the tickets
            if (sectionId.HasValue)
            {
                ticketsForEvent = ticketsForEvent.Where(t => t.Seat.Section.SectionId == sectionId.Value).ToList();
            }
            else
            {
                // If no section has been selected yet, select the first one by default
                ViewBag.SelectedSectionId = sections.FirstOrDefault()?.SectionId;
                ticketsForEvent = ticketsForEvent.Where(t => t.Seat.Section.SectionId == ViewBag.SelectedSectionId).ToList();
            }

            // Pass the list of tickets to the view
            ViewBag.SelectedSectionId = sectionId;

            // Pass the number of tickets to the view
            ViewBag.TicketCount = ticketsForEvent.Count;

            //
            // GROUP DISCOUNTS
            // Pass the list of discounts to the view
            ViewBag.GroupDiscounts = await _context.GroupDiscounts.ToListAsync();

            // Set the selected group discount ID
            ViewBag.SelectedGroupDiscountId = groupDiscountId;

            // Retrieve the group discount based on the groupDiscountId
            var groupDiscount = await _context.GroupDiscounts.FindAsync(groupDiscountId);

            // Assign the group discount to the ViewBag
            ViewBag.GroupDiscount = groupDiscount;

            // Get the minimum requirements for adult and children tickets from the selected group discount
            var selectedGroupDiscount = await _context.GroupDiscounts.FindAsync(groupDiscountId);
            var minimumAdultTickets = selectedGroupDiscount?.MinimumAdults ?? 0;
            var minimumChildrenTickets = selectedGroupDiscount?.MinimumChildren ?? 0;

            // Check if there are enough tickets available for the required group size
            if (ticketsForEvent.Count >= (minimumAdultTickets + minimumChildrenTickets))
            {
                // Select the required number of tickets
                var selectedTickets = ticketsForEvent.Take(minimumAdultTickets + minimumChildrenTickets);

                // Update the PurchaseId of the selected tickets
                foreach (var ticket in selectedTickets)
                {
                    ticket.PurchaseId = purchaseId;
                }

                int adultTicketsCount = 0;
                int childTicketsCount = 0;

                foreach (var ticket in selectedTickets)
                {
                    if (adultTicketsCount < minimumAdultTickets)
                    {
                        ticket.DiscountId = 1;
                        adultTicketsCount++;
                    }
                    else if (childTicketsCount < minimumChildrenTickets)
                    {
                        ticket.DiscountId = 5;
                        childTicketsCount++;
                    }
                    // Here you should calculate the ticket price using ticket.BasePrice and the corresponding price multiplier from the discounts
                    // You should retrieve the discount by ticket.DiscountId and use its price multiplier
                    var discount = await _context.Discounts.FindAsync(ticket.DiscountId);
                    ticket.TicketPrice = ticket.BasePrice * discount.PriceMultiplier;
                }

                // Update the PurchaseId of the selected tickets
                foreach (var ticket in selectedTickets)
                {
                    ticket.PurchaseId = purchaseId;
                }

                // Decrease the number of available tickets
                ViewBag.TicketCount = ticketsForEvent.Count - selectedTickets.Count();

                ViewBag.AdultTickets = adultTicketsCount;
                ViewBag.ChildTickets = childTicketsCount;
                ViewBag.AdultSubtotal = selectedTickets.Where(t => t.DiscountId == 1).Sum(t => t.TicketPrice);
                ViewBag.ChildSubtotal = selectedTickets.Where(t => t.DiscountId == 5).Sum(t => t.TicketPrice);

                // Use the selected tickets as needed (e.g., store them in ViewBag or pass them to the view)
                ViewBag.SelectedTickets = selectedTickets;
            }
            else
            {
                // Display an error message indicating that there are not enough tickets available
                ViewBag.Error = "Not enough tickets available for the selected group discount.";

                // Reset the selected group discount to prevent the dropdown from retaining the invalid selection
                groupDiscountId = null;
            }

            if (ticketsForEvent.Count < (minimumAdultTickets + minimumChildrenTickets))
            {
                ModelState.AddModelError("", "Not enough tickets available for the selected group discount.");
                // Display an error message indicating that there are not enough tickets available
                ViewBag.Error = "Not enough tickets available for the selected group discount.";

                // Reset the selected group discount to prevent the dropdown from retaining the invalid selection
                groupDiscountId = null;
            }

            //
            // Individual TICKET purchases
            if (discountTicketCounts != null)
            {
                foreach (var entry in discountTicketCounts)
                {
                    var discount = await _context.Discounts.FindAsync(entry.Key);
                    var quantity = entry.Value;
                    var totalPrice = quantity * discount.PriceMultiplier;

                    // Handle totalPrice as needed
                }
            }

            if (discountTicketCounts != null)
            {
                foreach (var entry in discountTicketCounts)
                {
                    var discount = await _context.Discounts.FindAsync(entry.Key);
                    var quantity = entry.Value;
                    var totalPrice = quantity * discount.PriceMultiplier;

                    // Handle totalPrice as needed
                }
            }

            ViewBag.Discounts = await _context.Discounts.ToListAsync();

            return View("~/Views/EventSystem/Tickets/TicketsPage.cshtml", ticketsForEvent);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string FirstName, string LastName, string Email, string PhoneNumber, string StreetAddress, string Country, string PostalCode)
        {
            //Gets logged in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }


            // Process the form data here

            return RedirectToAction("Index"); // Redirect to the desired page after processing
        }

        [HttpPost]
        public async Task<IActionResult> Ticket2(IEnumerable<Ticket> updatedTickets)
        {

            //Gets logged in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("~/Areas/Identity/Pages/Account/Login.cshtml");
            }

            //Matches logged in user to the EventUser
            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return RedirectToAction("~/Areas/Identity/Pages/Account/Login.cshtml");
            }

            //Updates the Profile View with the logged in user's information
            ViewData["ProfilePicture"] = eventUser.Picture;
            ViewData["UserName"] = eventUser.UserName;
            ViewData["FirstName"] = eventUser.FirstName;
            ViewData["LastName"] = eventUser.LastName;
            ViewData["Email"] = user.Email;
            ViewData["Age"] = eventUser.Age;
            ViewData["Document"] = eventUser.Document;

            var purchaseId = HttpContext.Session.GetInt32("PurchaseId");
            ViewData["PurchaseId"] = purchaseId;


            foreach (var updatedTicket in updatedTickets)
            {
                var ticket = await _context.Tickets.FindAsync(updatedTicket.TicketId);
                if (ticket != null)
                {
                    // Only update the PurchaseId and DiscountId here because the other fields might be altered unintentionally
                    ticket.PurchaseId = updatedTicket.PurchaseId;
                    ticket.DiscountId = updatedTicket.DiscountId;

                    _context.Tickets.Update(ticket);
                }
            }

            await _context.SaveChangesAsync();

            // Redirect to another action or return a view
            return View("~/Views/Home/Ticket2.cshtml");
        }
    }
}