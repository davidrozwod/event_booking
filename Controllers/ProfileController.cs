using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;
using event_booking.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace event_booking.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: EventUser
        [Authorize]
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
            ViewData["UserName"] = eventUser.UserName;
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
        [Authorize]
        public async Task<IActionResult> Index(ProfileViewModel profileViewModel, IFormFile document)
        {
            // Get the EventUser from the view model
            var eventUser = profileViewModel.EventUser;

            // Upload the document
            if (document != null && document.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await document.CopyToAsync(memoryStream);
                    eventUser.Document = memoryStream.ToArray();
                    eventUser.DocumentFileName = document.FileName; // Save the document name
                }
            }
            else
                ModelState.Remove("Document");


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
                identityUser.UserName = profileViewModel.IdentityUser.Email;
                await _userManager.UpdateAsync(identityUser);
            }


            return RedirectToAction("Index");
        }

        public IActionResult DisplayImage()
        {
            // Retrieve the image data from the database
            byte[] imageData = _context.EventUsers.FirstOrDefault()?.Document;

            // Determine the image format based on the image data
            ImageFormat imageFormat = ImageFormat.Jpeg; // Default format
            using (var imageStream = new MemoryStream(imageData))
            {
                using (var image = Image.FromStream(imageStream))
                {
                    ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
                    foreach (ImageCodecInfo encoder in encoders)
                    {
                        if (encoder.FormatID.Equals(image.RawFormat.Guid))
                        {
                            imageFormat = encoder.Format;
                            break;
                        }
                    }
                }
            }

            // Return the image with appropriate content type
            return File(imageData, imageFormat.MimeType);
        }

        // POST: EventUser/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

            // Log out the user
            await _signInManager.SignOutAsync();

            // Account deletion successful
            // You can perform additional actions or redirect to another page
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        private bool EventUserExists(string id)
        {
            return _context.EventUsers.Any(e => e.EventUserId == id);
        }
    }
}
