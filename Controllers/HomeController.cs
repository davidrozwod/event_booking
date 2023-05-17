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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AdminDashboard()
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

        public IActionResult Notification()
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