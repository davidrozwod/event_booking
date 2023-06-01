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
    public class EventCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventCategories
        public async Task<IActionResult> Index()
        {
              return _context.EventCategories != null ? 
                          View("~/Views/CRUDs/EventCategories/Index.cshtml", await _context.EventCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.EventCategories'  is null.");
        }

        // GET: EventCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventCategories == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategories
                .FirstOrDefaultAsync(m => m.EventCategoryId == id);
            if (eventCategory == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/EventCategories/Details.cshtml", eventCategory);
        }

        // GET: EventCategories/Create
        public IActionResult Create()
        {
            return View("~/Views/CRUDs/EventCategories/Create.cshtml");
        }

        // POST: EventCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventCategoryId,CategoryName")] EventCategory eventCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CRUDs/EventCategories/Create.cshtml", eventCategory);
        }

        // GET: EventCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventCategories == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategories.FindAsync(id);
            if (eventCategory == null)
            {
                return NotFound();
            }
            return View("~/Views/CRUDs/EventCategories/Edit.cshtml", eventCategory);
        }

        // POST: EventCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventCategoryId,CategoryName")] EventCategory eventCategory)
        {
            if (id != eventCategory.EventCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCategoryExists(eventCategory.EventCategoryId))
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
            return View("~/Views/CRUDs/EventCategories/Edit.cshtml", eventCategory);
        }

        // GET: EventCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventCategories == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategories
                .FirstOrDefaultAsync(m => m.EventCategoryId == id);
            if (eventCategory == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/EventCategories/Delete.cshtml", eventCategory);
        }

        // POST: EventCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventCategories'  is null.");
            }
            var eventCategory = await _context.EventCategories.FindAsync(id);
            if (eventCategory != null)
            {
                _context.EventCategories.Remove(eventCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventCategoryExists(int id)
        {
          return (_context.EventCategories?.Any(e => e.EventCategoryId == id)).GetValueOrDefault();
        }
    }
}
