using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class TheaterAndArtsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TheaterAndArtsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subCategoryIds = new List<int> { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 }; // IDs of your 10 subcategories

            var events = _context.Events
                .Where(e => subCategoryIds.Contains(e.EventCategory.EventCategoryId)) // Filter by subcategory ID
                .Include(e => e.EventCategory)
                 .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/TheaterAndArts.cshtml", events);
        }
        public async Task<IActionResult> PlaysAndDrama()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 21)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/PlaysAndDrama.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> MusicalsAndBroadway()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 22)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/MusicalsAndBroadway.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> DancePerformances()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 23)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/DancePerformances.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> BalletAndContemporaryDance()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 24)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/BalletAndContemporaryDance.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> OperaAncClassicalPerformances()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 25)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/OperaAncClassicalPerformances.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ImprovAndSketchComedy()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 26)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/ImprovAndSketchComedy.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ArtExhibitionAndGalleries()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 27)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/ArtExhibitionAndGalleries.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> StreetPerformances()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 28)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/StreetPerformances.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> PuppetryAndMarionetteShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 29)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/PuppetryAndMarionetteShows.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> MultiMediaAndExperimentalArts()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 30)
                .ToListAsync();

            return View("~/Views/EventCategories/TheaterAndArts/MultiMediaAndExperimentalArts.cshtml", concertsAndGigs);
        }
    }
}