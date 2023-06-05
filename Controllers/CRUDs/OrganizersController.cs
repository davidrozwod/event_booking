using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace event_booking.Controllers.CRUDs
{
    public class OrganizersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrganizersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Organizers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Organizers.Include(o => o.OrganizerCategory);
                return View("~/Views/CRUDs/Organizers/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: Organizers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Organizers == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers
                .Include(o => o.OrganizerCategory)
                .FirstOrDefaultAsync(m => m.OrganizerId == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Organizers/Details.cshtml", organizer);
        }

        // GET: Organizers/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["OrganizerCategoryId"] = new SelectList(_context.OrganizerCategories, "OrganizerCategoryId", "CategoryName");
            return View("~/Views/CRUDs/Organizers/Create.cshtml");
        }

        // POST: Organizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("OrganizerId,OrganizerCategoryId,Name,Description,Image,ContactInfo")] Organizer organizer)
        {
            ModelState.Remove("OrganizerCategory");
            if (ModelState.IsValid)
            {
                _context.Add(organizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["OrganizerCategoryId"] = new SelectList(_context.OrganizerCategories, "OrganizerCategoryId", "CategoryName", organizer.OrganizerCategoryId);
            return View("~/Views/CRUDs/Organizers/Create.cshtml", organizer);
        }

        // GET: Organizers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Organizers == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }
            ViewData["OrganizerCategoryId"] = new SelectList(_context.OrganizerCategories, "OrganizerCategoryId", "CategoryName", organizer.OrganizerCategoryId);
            return View("~/Views/CRUDs/Organizers/Edit.cshtml", organizer);
        }

        // POST: Organizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizerId,OrganizerCategoryId,Name,Description,Image,ContactInfo")] Organizer organizer)
        {
            if (id != organizer.OrganizerId)
            {
                return NotFound();
            }

            ModelState.Remove("OrganizerCategory");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerExists(organizer.OrganizerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizerCategoryId"] = new SelectList(_context.OrganizerCategories, "OrganizerCategoryId", "CategoryName", organizer.OrganizerCategoryId);
            return View("~/Views/CRUDs/Organizers/Edit.cshtml", organizer);
        }

        // GET: Organizers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Organizers == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers
                .Include(o => o.OrganizerCategory)
                .FirstOrDefaultAsync(m => m.OrganizerId == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Organizers/Delete.cshtml", organizer);
        }

        // POST: Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Organizers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Organizers'  is null.");
            }
            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool OrganizerExists(int id)
        {
          return (_context.Organizers?.Any(e => e.OrganizerId == id)).GetValueOrDefault();
        }
    }
}
