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

    }
}