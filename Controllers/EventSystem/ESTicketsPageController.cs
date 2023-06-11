using event_booking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
            var ticketsForEvent = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Venue)
                .Include(t => t.Seat)
                .Include(t => t.Seat.Section) // Include the Section related to the Seat
                .Include(t => t.Discount)
                .Include(t => t.TicketType)
                .Where(t => t.EventId == id && t.PurchaseId == null)
                .ToListAsync();

            // Getting the id of the currently logged in user
            var currentUserId = _userManager.GetUserId(User);

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

            ViewBag.Discounts = await _context.Discounts.ToListAsync();

            // get the current logged in user
            var user = await _userManager.GetUserAsync(User);

            // check if the user is logged in
            if (user == null)
            {
                // user is not logged in, redirect to the login page
                return RedirectToAction("Login", "Account");
            }

            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(eu => eu.EventUserId == user.Id);

            if (eventUser != null)
            {
                ViewBag.FirstName = eventUser.FirstName;
                ViewBag.LastName = eventUser.LastName;
            }

            ViewBag.CurrentUser = user;

            return View("~/Views/EventSystem/Tickets/TicketView.cshtml", ticket);
        }

        public IActionResult ConfirmPurchase(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            // Set the base price.
            ViewBag.BasePrice = ticket.BasePrice;

            // Load all discounts and ticket types.
            ViewBag.Discounts = _context.Discounts.ToList();
            ViewBag.TicketTypes = _context.TicketTypes.ToList();

            // Calculate the initial ticket price.
            var discountMultiplier = ticket.DiscountId.HasValue ? _context.Discounts.Find(ticket.DiscountId).PriceMultiplier : 1;
            var ticketTypeMultiplier = _context.TicketTypes.Find(ticket.TicketTypeId).PriceMultiplier;
            var ticketPrice = ticket.BasePrice * discountMultiplier * ticketTypeMultiplier;

            ViewBag.TicketPrice = ticketPrice;

            return View(ticket);
        }

    }
}