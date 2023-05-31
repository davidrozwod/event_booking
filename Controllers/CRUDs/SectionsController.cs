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
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sections
        public async Task<IActionResult> Index()
        {
              return _context.Sections != null ? 
                          View("~/Views/CRUDs/Sections/Index.cshtml", await _context.Sections.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Sections'  is null.");
        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (section == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Sections/Details.cshtml", section);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            return View("~/Views/CRUDs/Sections/Create.cshtml");
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionId,SectionName,PriceMultiplier")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CRUDs/Sections/Create.cshtml", section);
        }

        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View("~/Views/CRUDs/Sections/Edit.cshtml", section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionId,SectionName,PriceMultiplier")] Section section)
        {
            if (id != section.SectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.SectionId))
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
            return View("~/Views/CRUDs/Sections/Edit.cshtml", section);
        }

        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (section == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Sections/Delete.cshtml", section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sections'  is null.");
            }
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
          return (_context.Sections?.Any(e => e.SectionId == id)).GetValueOrDefault();
        }
    }
}
