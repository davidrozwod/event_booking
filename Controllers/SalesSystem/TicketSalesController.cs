using Microsoft.AspNetCore.Mvc;
using event_booking.Models;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using event_booking.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Mail;

namespace event_booking.Controllers.SalesSytem
{
    public class TicketSalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketSalesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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

            /*// Calculating ticket price here, you can replace this with your actual calculation logic
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
            }*/

            var viewModel = new TicketPriceViewModel();

            if (ticketsForEvent.Count > 0)
            {
                viewModel.Event = ticketsForEvent[0].Event;
                viewModel.Venue = ticketsForEvent[0].Venue;
            }

            /*//Using the viewmodel
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

            }).ToList();*/

            // Pass the list of tickets to the view
            return View("~/Views/EventSystem/Tickets/TicketsPage.cshtml", viewModel);
        }

        /*public IActionResult Index(int eventId)
        {
            // Retrieve event details using the eventId
            var eventDetails = RetrieveEventDetails(eventId);

            // Pass the event details to the view
            return View("~/Views/Home/Ticket.cshtml", eventDetails);
        }*/

        // Helper method to retrieve event details
        private Event RetrieveEventDetails(int eventId)
        {
            var eventDetails = _context.Events
                .Include(e => e.EventCategory)
                .Include(e => e.Organizer)
                .Include(e => e.Venue)
                .FirstOrDefault(e => e.EventId == eventId);

            return eventDetails;
        }
    }
}
