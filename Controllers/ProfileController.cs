using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace event_booking.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IEventUserService _eventUserService;

        public ProfileController(IEventUserService eventUserService)
        {
            _eventUserService = eventUserService;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Picture
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string pictureUrl)
        {
            // Get the logged in user's id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                // Handle case where user is not logged in
                return NotFound();
            }

            // Update the user's picture URL using the provided service
            await _eventUserService.UpdateEventUserPictureAsync(userId, pictureUrl);

            // If the update was successful, redirect to the profile page
            return RedirectToAction("Profile", "Home");
        }

        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pictureUrl = await _eventUserService.GetEventUserPictureUrlAsync(userId);

            // Pass the picture URL to the view, e.g. using ViewBag
            ViewBag.PictureUrl = pictureUrl;

            return View();
        }
    }
}
