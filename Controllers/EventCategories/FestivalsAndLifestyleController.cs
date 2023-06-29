using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class FestivalsAndLifestyleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FestivalsAndLifestyleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subCategoryIds = new List<int> { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 }; // IDs of your 10 subcategories

            var events = _context.Events
                .Where(e => subCategoryIds.Contains(e.EventCategory.EventCategoryId)) // Filter by subcategory ID
                .Include(e => e.EventCategory)
                 .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/FestivalsAndLifestyle.cshtml", events);
        }
        public async Task<IActionResult> FoodAndWineFestivals()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 41)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/FoodAndWineFestivals.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> FashionShowsAndRunways()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 42)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/FashionShowsAndRunways.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> ArtAndCultureFestivals()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 43)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/ArtAndCultureFestivals.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> FilmAndDocumentaryFestivals()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 44)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/FilmAndDocumentaryFestivals.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> MusicFestivalsAndOutdoorConcerts()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 45)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/MusicFestivalsAndOutdoorConcerts.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> WellnessRetreatsAndYogaFestivals()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 46)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/WellnessRetreatsAndYogaFestivals.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> TechnologyAndInnovationExpos()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 47)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/TechnologyAndInnovationExpos.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> TravelAndAdventureExpos()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 48)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/TravelAndAdventureExpos.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> LiteraryAndBookFairs()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 49)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/LiteraryAndBookFairs.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> HomeAndGardenShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 50)
                .ToListAsync();

            return View("~/Views/EventCategories/FestivalsAndLifestyle/HomeAndGardenShows.cshtml", concertsAndGigs);
        }
    }
}