using event_booking.Data;
using event_booking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace event_booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Events = _context.Events.ToList();
            ViewBag.Organizers = _context.Organizers.ToList();
            ViewBag.EventCategories = _context.EventCategories.ToList();
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Policy()
        {
            return View();
        }

        public IActionResult UserExclusiveFeatures()
        {
            return View();
        }

        public IActionResult Ticket()
        {
            return View();
        }

        public IActionResult Ticket2()
        {
            return View();
        }

        public IActionResult Ticket3()
        {
            return View();
        }

        public IActionResult Ticket4()
        {
            return View();
        }

        public IActionResult Event_Category()
        {
            return View();
        }

        public IActionResult PerformancesAndNightlife()
        {
            return View("~/Views/EventCategories/PerformancesAndNightlife.cshtml");
        }
        public IActionResult SportsAndOutdoors()
        {
            return View("~/Views/EventCategories/SportsAndOutdoors.cshtml");
        }
        public IActionResult TheaterAndArts()
        {
            return View("~/Views/EventCategories/TheaterAndArts.cshtml");
        }
        public IActionResult FamilyAndEducation()
        {
            return View("~/Views/EventCategories/FamilyAndEducation.cshtml");
        }

        [Authorize]
        public IActionResult PromoterArea()
        {
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public IActionResult Booking()
        {
            return View();
        }

        [Authorize]
        public IActionResult Events()
        {
            return View();
        }

        [Authorize]
        public IActionResult Review()
        {
            return View();
        }

        [Authorize]
        public IActionResult CRUDs()
        {
            return View();
        }

        [Authorize]
        public IActionResult RegisterAccount()
        {
            return View();
        }

        public IActionResult ConcertsAndGigs()
        {
            return View("~/Views/EventCategories/ConcertsAndGigs.cshtml");
        }
        public IActionResult RapAndHipHop()
        {
            return View("~/Views/EventCategories/RapAndHipHop.cshtml");
        }
        public IActionResult RBandSoul()
        {
            return View("~/Views/EventCategories/RBandSoul.cshtml");
        }
        public IActionResult JazzAndBlues()
        {
            return View("~/Views/EventCategories/JazzAndBlues.cshtml");
        }

        public IActionResult HardRockMetal()
        {
            return View("~/Views/EventCategories/HardRockMetal.cshtml");
        }

        public IActionResult DanceElectronic()
        {
            return View("~/Views/EventCategories/DanceElectronic.cshtml");
        }

        public IActionResult CombatSports()
        {
            return View("~/Views/EventCategories/CombatSports.cshtml");
        }

        public IActionResult Baseball()
        {
            return View("~/Views/EventCategories/Baseball.cshtml");
        }

        public IActionResult Basketball()
        {
            return View("~/Views/EventCategories/Basketball.cshtml");
        }

        public IActionResult RugbyUnion()
        {
            return View("~/Views/EventCategories/Basketball.cshtml");
        }

        public IActionResult Football()
        {
            return View("~/Views/EventCategories/Football.cshtml");
        }

        public IActionResult Cricket()
        {
            return View("~/Views/EventCategories/Cricket.cshtml");
        }

        public IActionResult Netball()
        {
            return View("~/Views/EventCategories/Netball.cshtml");
        }

        public IActionResult Golf()
        {
            return View("~/Views/EventCategories/Golf.cshtml");
        }

        public IActionResult BalletAndDance()
        {
            return View("~/Views/EventCategories/BalletAndDance.cshtml");
        }

        public IActionResult Classical()
        {
            return View("~/Views/EventCategories/Classical.cshtml");
        }

        public IActionResult Fashion()
        {
            return View("~/Views/EventCategories/Fashion.cshtml");
        }

        public IActionResult MuseumAndExhibits()
        {
            return View("~/Views/EventCategories/MuseumAndExhibits.cshtml");
        }

        public IActionResult Musicals()
        {
            return View("~/Views/EventCategories/Musicals.cshtml");
        }

        public IActionResult Opera()
        {
            return View("~/Views/EventCategories/Opera.cshtml");
        }

        public IActionResult Plays()
        {
            return View("~/Views/EventCategories/Plays.cshtml");
        }

        public IActionResult ChildrensMusicAndTheater()
        {
            return View("~/Views/EventCategories/ChildrensMusicAndTheater.cshtml");
        }

        public IActionResult Circus()
        {
            return View("~/Views/EventCategories/Circus.cshtml");
        }

        public IActionResult FairsAndFestivals()
        {
            return View("~/Views/EventCategories/FairsAndFestivals.cshtml");
        }

        public IActionResult FamilyAttractions ()
        {
            return View("~/Views/EventCategories/FamilyAttractions.cshtml");
        }

        public IActionResult IceShows()
        {
            return View("~/Views/EventCategories/IceShows.cshtml");
        }

        public IActionResult MagicShows()
        {
            return View("~/Views/EventCategories/MagicShows.cshtml");
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult EventSystemPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult RedirectAccount()
        {

            return Redirect("/Identity/Account/Manage");
        }
    }
}