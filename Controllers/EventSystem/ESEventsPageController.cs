using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers.EventSystem
{
    public class ESEventsPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ESEventsPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Other methods...

        public async Task<IActionResult> EventsPage(int id)
        {
            var eventDetail = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .FirstOrDefaultAsync(e => e.EventId == id);

            return View("~/Views/Home/EventsPage.cshtml", eventDetail);
        }
    }
}
