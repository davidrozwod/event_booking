using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class SportsAndOutdoorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportsAndOutdoorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events
                .Include(e => e.EventCategory)
                .Include(e => e.Organizer)
                .Where(e => e.EventCategoryId == 5)
                .ToList();
            return View("~/Views/EventCategories/SportsAndOutdoors.cshtml", events);
        }

        public async Task<IActionResult> ProfessionalSportsGames()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 31)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/ProfessionalSportsGames.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> AmateurSportsTournaments()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 32)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/AmateurSportsTournaments.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> FitnessChallengesAndMarathons()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 33)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/FitnessChallengesAndMarathons.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> AdventureRacesAndObstacleCourses()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 34)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/AdventureRacesAndObstacleCourses.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> OutdoorAdventuresAndExcursions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 35)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/OutdoorAdventuresAndExcursions.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> CyclingRacesAndTours()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 36)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/CyclingRacesAndTours.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> WaterSportsCompetitions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 37)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/WaterSportsCompetitions.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> TeamBuildingAndCorporateSports()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 38)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/WaterSportsCompetitions.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ExtremeSportsExhibitions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 39)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/ExtremeSportsExhibitions.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> YogaAndFitnessWorkshops()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 40)
                .ToListAsync();

            return View("~/Views/EventCategories/SportsAndOutdoors/YogaAndFitnessWorkshops.cshtml", concertsAndGigs);
        }
    }
}