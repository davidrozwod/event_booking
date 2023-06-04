using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;
using event_booking.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace event_booking.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EventUser
        public async Task<IActionResult> Index()
        {
            //Gets logged in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            //Matches logged in user to the EventUser
            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return NotFound();
            }

            //Updates the Profile View with the logged in user's information
            ViewData["ProfilePicture"] = eventUser.Picture;
            ViewData["UserId"] = user.Id;
            ViewData["Email"] = user.Email;
            ViewData["UserName"] = user.UserName;
            ViewData["FirstName"] = eventUser.FirstName;
            ViewData["LastName"] = eventUser.LastName;
            ViewData["Age"] = eventUser.Age;
            ViewData["Document"] = eventUser.Document;

            //
            var profileViewModel = new ProfileViewModel
            {
                EventUser = eventUser,
                IdentityUser = user
            };

            return View("~/Views/Home/Profile.cshtml", profileViewModel);
        }

        // POST: EventUser/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProfileViewModel profileViewModel)
        {
            // Get the EventUser from the view model
            var eventUser = profileViewModel.EventUser;

            //Updates the event users information
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventUserExists(eventUser.EventUserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Update the IdentityUser properties separately
            var identityUser = await _userManager.FindByIdAsync(eventUser.EventUserId);
            if (identityUser != null)
            {
                identityUser.Email = profileViewModel.IdentityUser.Email;
                identityUser.UserName = profileViewModel.IdentityUser.UserName;
                await _userManager.UpdateAsync(identityUser);
            }


            return RedirectToAction("Index");
        }

        // POST: EventUser/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return NotFound();
            }

            _context.EventUsers.Remove(eventUser);
            await _context.SaveChangesAsync();

            await _userManager.DeleteAsync(user);

            // Account deletion successful
            // You can perform additional actions or redirect to another page
            return RedirectToAction("Index", "Home");
        }


        private bool EventUserExists(string id)
        {
            return _context.EventUsers.Any(e => e.EventUserId == id);
        }
    }
}
