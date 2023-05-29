using event_booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> UpdateProfileProfilePicture(string pictureUrl)
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

        public async Task<IActionResult> ProfilePicture()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                // Handle case where user is not logged in
                return NotFound();
            }

            var pictureUrl = await _eventUserService.GetEventUserPictureUrlAsync(userId);

            // Create a view model with the necessary data
            var model = new EventUser
            {
                Picture = pictureUrl
            };

            return View(model);
        }
    }
}
