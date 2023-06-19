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
            var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

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

            /*if()


                     var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

                     // Retrieve all tickets that are associated with this purchase
                     var ticketsForEvent = await _context.Tickets
                         .Where(t => t.PurchaseId == purchaseId)
                         .ToListAsync();

                     // Save changes to the database
                     _context.UpdateRange(ticketsForEvent);
                     await _context.SaveChangesAsync();

                     // Redirect to another action or return a view
                     return View("~/Views/Home/Ticket2.cshtml");
                 }*/


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
                return NotFound();
            }

            //Matches logged in user to the EventUser
            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return NotFound();
            }

            //Updates the Profile View with the logged in user's information
            ViewData["ProfilePicture"] = eventUser.Picture;
            ViewData["UserName"] = eventUser.UserName;
            ViewData["FirstName"] = eventUser.FirstName;
            ViewData["LastName"] = eventUser.LastName;
            ViewData["Age"] = eventUser.Age;
            ViewData["Document"] = eventUser.Document;


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

    /*//
    //Selects a single ticket for viewing
    [Authorize]
    public async Task<IActionResult> Detail(int id)
    {
        //Get the ticket and include the related entities
        var ticket = await _context.Tickets
            .Include(t => t.Event)
            .Include(t => t.Venue)
            .Include(t => t.Seat).ThenInclude(s => s.Section)
            .Include(t => t.Discount)
            .Include(t => t.EventUser)
            .Include(t => t.TicketType)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (ticket == null)
        {
            return NotFound();
        }

        // Get the discounts and convert them to a dictionary
        var discounts = await _context.Discounts
                .ToDictionaryAsync(d => d.DiscountId, d => d.PriceMultiplier);
        var discountNames = await _context.Discounts
                .ToDictionaryAsync(d => d.DiscountId, d => d.DiscountName);

        // get the current logged in user
        var user = await _userManager.GetUserAsync(User);

        // logged in user is an event user, get the first and last name
        var eventUser = await _context.EventUsers.FirstOrDefaultAsync(eu => eu.EventUserId == user.Id);

        if (eventUser != null)
        {
            ViewBag.FirstName = eventUser.FirstName;
            ViewBag.LastName = eventUser.LastName;
        }

        ViewBag.CurrentUser = user;

        var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

        // check if the ticket is already associated with a purchase
        if (ticket.PurchaseId.HasValue)
        {
            // if the same user reloads the page, use the existing purchase
            if (purchaseId == ticket.PurchaseId.Value)
            {
                ViewBag.Message = "This ticket is already reserved by you.";
            }
            else
            {
                // if another user has already reserved the ticket, display a message
                ViewBag.Message = "This ticket is already reserved by another user.";
                return View("~/Views/EventSystem/Tickets/TicketView.cshtml");
            }
        }
        else
        {
            Purchase purchase;

            if (purchaseId.HasValue)
            {
                purchase = await _context.Purchases.FindAsync(purchaseId.Value);
            }
            else
            {
                purchase = new Purchase();
                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("PurchaseId", purchase.PurchaseId);
            }

            ticket.PurchaseId = purchase.PurchaseId;
            await _context.SaveChangesAsync();
        }

        ticket.TicketPrice = ticket.BasePrice;

        var model = new TicketPriceViewModel
        {
            Event = ticket.Event,
            Venue = ticket.Venue,
            Seat = ticket.Seat,
            Ticket = ticket,
            Discounts = discounts,
            DiscountNames = discountNames
        };

        return View("~/Views/EventSystem/Tickets/TicketView.cshtml", model);
    }
    */

    /*
    public IActionResult ConfirmPurchase(int ticketId)
    {
        var ticket = _context.Tickets.Find(ticketId);

        // ...other code...

        var discounts = _context.Discounts
            .ToDictionary(d => d.DiscountId, d => d.PriceMultiplier);

        var model = new TicketPriceViewModel
        {
            Ticket = ticket,
            Discounts = discounts
        };

        return View(model);
    }*/
/*
﻿using event_booking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using event_booking.Models;
using event_booking.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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

        //
        //Index
        public async Task<IActionResult> Index(int id)
        {
            var currentUserId = _userManager.GetUserId(User);

            var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

            var ticketsForEvent = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Venue)
                .Include(t => t.Seat)
                .Include(t => t.Seat.Section) // Includes the Section related to the Seat
                .Include(t => t.Discount)
                .Include(t => t.TicketType)
                .Where(t => t.EventId == id && (t.PurchaseId == null || t.PurchaseId == purchaseId))
                .ToListAsync();

            // Calculating ticket price here, you can replace this with your actual calculation logic
            foreach (var ticket in ticketsForEvent)
            {
                // Using base price as ticket price
                ticket.TicketPrice = ticket.BasePrice * ticket.Seat.Section.PriceMultiplier;

                // Loyalty and early bird discounts
                if (ticket.TicketType != null)
                {
                    ticket.TicketPrice *= ticket.TicketType.PriceMultiplier;
                }

                ticket.EventUserId = currentUserId;
            }

            //Using the viewmodel
            var ticketsForEventViewModel = ticketsForEvent.Select(ticket => new TicketPriceViewModel
            {
                Event = ticket.Event,
                Venue = ticket.Venue,
                Seat = ticket.Seat,
                Ticket = ticket,
                Discount = ticket.Discount,
                TicketType = ticket.TicketType,
                Purchase = ticket.Purchase,
                Discounts = ticket.Discount != null ? new Dictionary<int, decimal> { { ticket.Discount.DiscountId, ticket.Discount.PriceMultiplier } } : new Dictionary<int, decimal>(),
                DiscountNames = ticket.Discount != null ? new Dictionary<int, string> { { ticket.Discount.DiscountId, ticket.Discount.DiscountName } } : new Dictionary<int, string>(),

            }).ToList();

            // Pass the list of tickets to the view
            return View("~/Views/EventSystem/Tickets/TicketsPage.cshtml", ticketsForEventViewModel);
        }

        //
        //Selects a single ticket for viewing
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            //Get the ticket and include the related entities
            var ticket = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Venue)
                .Include(t => t.Seat).ThenInclude(s => s.Section)
                .Include(t => t.Discount)
                .Include(t => t.EventUser)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            // Get the discounts and convert them to a dictionary
            var discounts = await _context.Discounts
                    .ToDictionaryAsync(d => d.DiscountId, d => d.PriceMultiplier);
            var discountNames = await _context.Discounts
                    .ToDictionaryAsync(d => d.DiscountId, d => d.DiscountName);

            // get the current logged in user
            var user = await _userManager.GetUserAsync(User);

            // logged in user is an event user, get the first and last name
            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(eu => eu.EventUserId == user.Id);

            if (eventUser != null)
            {
                ViewBag.FirstName = eventUser.FirstName;
                ViewBag.LastName = eventUser.LastName;
            }

            ViewBag.CurrentUser = user;

            var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

            // check if the ticket is already associated with a purchase
            if (ticket.PurchaseId.HasValue)
            {
                // if the same user reloads the page, use the existing purchase
                if (purchaseId == ticket.PurchaseId.Value)
                {
                    ViewBag.Message = "This ticket is already reserved by you.";
                }
                else
                {
                    // if another user has already reserved the ticket, display a message
                    ViewBag.Message = "This ticket is already reserved by another user.";
                    return View("~/Views/EventSystem/Tickets/TicketView.cshtml");
                }
            }
            else
            {
                Purchase purchase;

                if (purchaseId.HasValue)
                {
                    purchase = await _context.Purchases.FindAsync(purchaseId.Value);
                }
                else
                {
                    purchase = new Purchase();
                    // Set the session expiry time to the current time plus 5 minutes
                    purchase.SessionExpiryTime = DateTime.UtcNow.AddMinutes(5);
                    _context.Purchases.Add(purchase);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetInt32("PurchaseId", purchase.PurchaseId);
                }

                ticket.PurchaseId = purchase.PurchaseId;
                await _context.SaveChangesAsync();
            }

            ticket.TicketPrice = ticket.BasePrice;

            var model = new TicketPriceViewModel
            {
                Event = ticket.Event,
                Venue = ticket.Venue,
                Seat = ticket.Seat,
                Ticket = ticket,
                Discounts = discounts,
                DiscountNames = discountNames
            };

            return View("~/Views/EventSystem/Tickets/TicketView.cshtml", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTicket(int eventId, int purchaseId)
        {
            // Get the purchase
            var purchase = await _context.Purchases.FindAsync(purchaseId);

            if (purchase == null)
            {
                return NotFound();
            }

            // Update the session expiry time
            purchase.SessionExpiryTime = DateTime.UtcNow.AddMinutes(5);
            await _context.SaveChangesAsync();

            // Get an available ticket for the event
            var ticket = await _context.Tickets
                .Where(t => t.EventId == eventId && t.PurchaseId == null)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return NotFound();
            }

            // Assign the purchaseId to the ticket
            ticket.PurchaseId = purchaseId;
            await _context.SaveChangesAsync();

            // Return the ticket object as JSON
            return Json(new { TicketId = ticket.TicketId, SessionExpiryTime = purchase.SessionExpiryTime });
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CancelPurchase(int ticketId)
        {
            // Get the ticket
            var ticket = await _context.Tickets.FindAsync(ticketId);

            // If ticket doesn't exist, redirect to not found page
            if (ticket == null)
            {
                return NotFound();
            }

            // If the ticket isn't associated with a purchase, redirect to a suitable page
            if (!ticket.PurchaseId.HasValue)
            {
                return RedirectToAction("Index");
            }

            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            // Check if the current user is the one who reserved the ticket
            if (HttpContext.Session.GetInt32("PurchaseId") != ticket.PurchaseId.Value)
            {
                return Unauthorized(); // Or redirect to a suitable page
            }

            // Nullify the PurchaseId of the ticket
            ticket.PurchaseId = null;
            await _context.SaveChangesAsync();

            // Clear the PurchaseId from the user's session
            HttpContext.Session.Remove("PurchaseId");

            // Redirect back to the list of available tickets
            return RedirectToAction("Index");
        }

        /*
        // Selects all tickets for the current user
        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            // Check if the user is logged in
            if (user == null)
            {
                // User is not logged in, redirect to the login page
                return RedirectToAction("Home", "Index");
            }

            // Get the PurchaseId from the user's session
            var purchaseId = HttpContext.Session.GetInt32("PurchaseId");

            // If there's no PurchaseId in the session, redirect to a suitable page
            if (!purchaseId.HasValue)
            {
                return RedirectToAction("Index"); // Or any other suitable page
            }

            // Get all tickets associated with the PurchaseId
            var tickets = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Venue)
                .Include(t => t.Seat).ThenInclude(s => s.Section)
                .Include(t => t.Discount)
                .Include(t => t.EventUser)
                .Include(t => t.TicketType)
                .Where(t => t.PurchaseId == purchaseId.Value)
                .ToListAsync();

            // Get the session expiry time
            var purchase = await _context.Purchases.FindAsync(purchaseId.Value);
            var expiryTime = purchase?.SessionExpiryTime;

            // Pass the tickets and expiry time to the view
            var model = new Ticket
            {
                Tickets = tickets,
                SessionExpiryTime = expiryTime
            };

            return View(model);
        }*/