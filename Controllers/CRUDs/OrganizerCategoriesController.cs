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
    public class OrganizerCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizerCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrganizerCategories
        public async Task<IActionResult> Index()
        {
              return _context.OrganizerCategories != null ? 
                          View("~/Views/CRUDs/OrganizerCategories/Index.cshtml", await _context.OrganizerCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.OrganizerCategories'  is null.");
        }

        // GET: OrganizerCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrganizerCategories == null)
            {
                return NotFound();
            }

            var organizerCategory = await _context.OrganizerCategories
                .FirstOrDefaultAsync(m => m.OrganizerCategoryId == id);
            if (organizerCategory == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/OrganizerCategories/Details.cshtml", organizerCategory);
        }

        // GET: OrganizerCategories/Create
        public IActionResult Create()
        {
            return View("~/Views/CRUDs/OrganizerCategories/Create.cshtml");
        }

        // POST: OrganizerCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizerCategoryId,CategoryName")] OrganizerCategory organizerCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizerCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CRUDs/OrganizerCategories/Details.cshtml", organizerCategory);
        }

        // GET: OrganizerCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrganizerCategories == null)
            {
                return NotFound();
            }

            var organizerCategory = await _context.OrganizerCategories.FindAsync(id);
            if (organizerCategory == null)
            {
                return NotFound();
            }
            return View("~/Views/CRUDs/OrganizerCategories/Edit.cshtml", organizerCategory);
        }

        // POST: OrganizerCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizerCategoryId,CategoryName")] OrganizerCategory organizerCategory)
        {
            if (id != organizerCategory.OrganizerCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizerCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerCategoryExists(organizerCategory.OrganizerCategoryId))
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
            return View("~/Views/CRUDs/OrganizerCategories/Edit.cshtml", organizerCategory);
        }

        // GET: OrganizerCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrganizerCategories == null)
            {
                return NotFound();
            }

            var organizerCategory = await _context.OrganizerCategories
                .FirstOrDefaultAsync(m => m.OrganizerCategoryId == id);
            if (organizerCategory == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/OrganizerCategories/Delete.cshtml", organizerCategory);
        }

        // POST: OrganizerCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrganizerCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrganizerCategories'  is null.");
            }
            var organizerCategory = await _context.OrganizerCategories.FindAsync(id);
            if (organizerCategory != null)
            {
                _context.OrganizerCategories.Remove(organizerCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizerCategoryExists(int id)
        {
          return (_context.OrganizerCategories?.Any(e => e.OrganizerCategoryId == id)).GetValueOrDefault();
        }
    }
}
