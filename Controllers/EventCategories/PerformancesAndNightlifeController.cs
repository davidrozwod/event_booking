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

        public IActionResult ConcertsAndGigs()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/ConcertsAndGigs.cshtml");
        }

        public IActionResult MusicFestivals()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/MusicFestivals.cshtml");
        }
        public IActionResult DJSetsAndElectronicMusic()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/DJSetsAndElectronicMusic.cshtml");
        }

        public IActionResult LiveBandsAndMusicians()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/LiveBandsAndMusicians.cshtml");
        }
        public IActionResult ClubNightsAndDanceParties()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/ClubNightsAndDanceParties.cshtml");
        }
        public IActionResult JazzAndBluesPerformances()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/JazzAndBluesPerformances.cshtml");
        }
        public IActionResult AcousticSessions()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/AcousticSessions.cshtml");
        }
        public IActionResult OpenMicNights()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/OpenMicNights.cshtml");
        }
        public IActionResult KaraokeNights()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/KaraokeNights.cshtml");
        }
        public IActionResult LiveComedyShows()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife/LiveComedyShows.cshtml");
        }
    }
}