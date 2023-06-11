using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class PerformancesAndNightlifeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformancesAndNightlifeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events
                .Include(e => e.EventCategory)
                .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/PerformancesAndNightlife.cshtml", events);
        }

        public async Task<IActionResult> ConcertsAndGigs()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 11)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/ConcertsAndGigs.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> MusicFestivals()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 12)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/MusicFestivals.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> DJSetsAndElectronicMusic()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 13)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/DJSetsAndElectronicMusic.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> LiveBandsAndMusicians()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 14)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/LiveBandsAndMusicians.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ClubNightsAndDanceParties()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 15)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/ClubNightsAndDanceParties.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> JazzAndBluesPerformances()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 16)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/JazzAndBluesPerformances.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> AcousticSessions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 17)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/AcousticSessions.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> OpenMicNights()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 18)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/OpenMicNights.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> KaraokeNights()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 19)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/KaraokeNights.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> LiveComedyShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 20)
                .ToListAsync();

            return View("~/Views/EventCategories/PerformancesAndNightlife/LiveComedyShows.cshtml", concertsAndGigs);
        }
    }
}