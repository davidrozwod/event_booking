using event_booking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using event_booking.Models;
using event_booking.Models.ViewModels;
using Microsoft.AspNetCore.Http;

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
                .Include(t => t.Seat.Section) // Include the Section related to the Seat
                .Include(t => t.Discount)
                .Include(t => t.TicketType)
                .Where(t => t.EventId == id && (t.PurchaseId == null || t.PurchaseId == purchaseId))
                .ToListAsync();

            // Calculating ticket price here, you can replace this with your actual calculation logic
            foreach (var ticket in ticketsForEvent)
            {
                // Using base price as ticket price
                ticket.TicketPrice = ticket.BasePrice * ticket.Seat.Section.PriceMultiplier;

                // Applying discounts and ticket types if they exist
                if (ticket.Discount != null)
                {
                    ticket.TicketPrice *= ticket.Discount.PriceMultiplier;
                }

                if (ticket.TicketType != null)
                {
                    ticket.TicketPrice *= ticket.TicketType.PriceMultiplier;
                }

                ticket.EventUserId = currentUserId;
            }

            // Pass the list of tickets to the view
            return View("~/Views/EventSystem/Tickets/TicketsPage.cshtml", ticketsForEvent);
        }

        //
        //Selects a single ticket for viewing
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

            // check if the user is logged in
            if (user == null)
            {
                // user is not logged in, redirect to the login page
                return RedirectToAction("Home", "Index");
            }

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
        }

    }
}