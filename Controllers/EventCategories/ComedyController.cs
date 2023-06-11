using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class ComedyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComedyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subCategoryIds = new List<int> { 72, 73, 74, 75, 76, 77, 78, 79, 80, 81 }; // IDs of your 10 subcategories

            var events = _context.Events
                .Where(e => subCategoryIds.Contains(e.EventCategory.EventCategoryId)) // Filter by subcategory ID
                .Include(e => e.EventCategory)
                 .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/Comedy.cshtml", events);
        }
        public async Task<IActionResult> StandUpComedyShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 72)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/StandUpComedyShows.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ImprovAndSketchComedyNights()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 73)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ImprovAndSketchComedyNights.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyFestivals()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 74)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyFestivals.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyRoastsAndSatireShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 75)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyRoastsAndSatireShows.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyWorkshopsAndClasses()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 76)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyWorkshopsAndClasses.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyCompetitions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 77)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyCompetitions.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyPodcastRecordings()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 78)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyPodcastRecordings.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyFilmScreenings()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 79)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyFilmScreenings.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyGameShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 80)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyGameShows.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ComedyToursAndLivePodcasts()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 81)
                .ToListAsync();

            return View("~/Views/EventCategories/Comedy/ComedyGameShows.cshtml", concertsAndGigs);
        }

    }
}