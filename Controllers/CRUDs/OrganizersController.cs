using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;

namespace event_booking.Controllers.CRUDs
{
    public class OrganizersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organizers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Organizers.Include(o => o.OrganizerCategory);
            return View("~/Views/CRUDs/Organizers/Index.cshtml", await applicationDbContext.ToListAsync());
        }

        // GET: Organizers/Details/5
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

        private bool OrganizerExists(int id)
        {
          return (_context.Organizers?.Any(e => e.OrganizerId == id)).GetValueOrDefault();
        }
    }
}
