﻿using System.Linq;
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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Advanced;

namespace event_booking.Controllers
{
    [Authorize]
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
            ViewData["DocumentFileName"] = eventUser.DocumentFileName;


            //
            var profileViewModel = new ProfileViewModel
            {
                EventUser = eventUser,
                IdentityUser = user
            };

            return View("~/Areas/Identity/Pages/Account/Profile.cshtml", profileViewModel);
        }

        // POST: EventUser/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Index(ProfileViewModel profileViewModel, IFormFile document)
        {
            // Get the EventUser from the view model
            var eventUserViewModel = profileViewModel.EventUser;

            // Fetch the existing EventUser from the database
            var eventUser = await _context.EventUsers.FindAsync(eventUserViewModel.EventUserId);

            // If the EventUser doesn't exist, return NotFound
            if (eventUser == null)
            {
                return NotFound();
            }

            // Update the non-document properties of EventUser
            eventUser.Picture = eventUserViewModel.Picture;
            eventUser.UserName = eventUserViewModel.UserName;
            eventUser.FirstName = eventUserViewModel.FirstName;
            eventUser.LastName = eventUserViewModel.LastName;
            eventUser.Age = eventUserViewModel.Age;

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
            {
                ModelState.Remove("Document");
            }



            //Updates the event users information
            //Im pretty sure this can be removed, update is already done above
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
                await _userManager.UpdateAsync(identityUser);
            }


            return RedirectToAction("Index");
        }

        // GET: EventUser/DisplayImage
        public async Task<IActionResult> DisplayImage()
        {
            // Gets logged in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Retrieves the event user for the logged-in user
            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return NotFound();
            }

            // Retrieve the image data from the event user
            byte[] imageData = eventUser.Document;

            // Determine the file extension based on the image data
            string fileExtension = ".jpg"; // Default file extension

            using (var imageStream = new MemoryStream(imageData))
            {
                using (var image = SixLabors.ImageSharp.Image.Load(imageStream))
                {
                    var format = image.Metadata.DecodedImageFormat;
                    fileExtension = format.FileExtensions.First(); // Default to first file extension if multiple are available
                }
            }

            // Return the image with appropriate content type
            return File(imageData, $"image/{fileExtension}");
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

            // Log out the user
            await _signInManager.SignOutAsync();

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
