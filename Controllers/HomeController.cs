using event_booking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace event_booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
            return View();
        }
        public IActionResult SportsAndOutdoors()
        {
            return View();
        }
        public IActionResult TheaterAndArts()
        {
            return View();
        }
        public IActionResult FamilyAndEducation()
        {
            return View();
        }

        public IActionResult PromoterArea()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Booking()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Review()
        {
            return View();
        }

        public IActionResult CRUDs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}