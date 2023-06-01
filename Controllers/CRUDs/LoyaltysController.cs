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
    public class LoyaltysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoyaltysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loyaltys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Loyalties.Include(l => l.EventUser);
            return View("~/Views/CRUDs/Loyaltys/Index.cshtml", await applicationDbContext.ToListAsync());
        }

        // GET: Loyaltys/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Loyalties == null)
            {
                return NotFound();
            }

            var loyalty = await _context.Loyalties
                .Include(l => l.EventUser)
                .FirstOrDefaultAsync(m => m.EventUserId == id);
            if (loyalty == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Loyaltys/Details.cshtml", loyalty);
        }

        // GET: Loyaltys/Create
        public IActionResult Create()
        {
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId");
            return View("~/Views/CRUDs/Loyaltys/Create.cshtml");
        }

        // POST: Loyaltys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventUserId,TicketCount,PriceMultiplier")] Loyalty loyalty)
        {
            ModelState.Remove("EventUser");
            if (ModelState.IsValid)
            {
                _context.Add(loyalty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", loyalty.EventUserId);
            return View("~/Views/CRUDs/Loyaltys/Create.cshtml", loyalty);
        }

        // GET: Loyaltys/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Loyalties == null)
            {
                return NotFound();
            }

            var loyalty = await _context.Loyalties.FindAsync(id);
            if (loyalty == null)
            {
                return NotFound();
            }
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", loyalty.EventUserId);
            return View("~/Views/CRUDs/Loyaltys/Edit.cshtml", loyalty);
        }

        // POST: Loyaltys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EventUserId,TicketCount,PriceMultiplier")] Loyalty loyalty)
        {
            if (id != loyalty.EventUserId)
            {
                return NotFound();
            }

            ModelState.Remove("EventUser");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loyalty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoyaltyExists(loyalty.EventUserId))
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
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", loyalty.EventUserId);
            return View("~/Views/CRUDs/Loyaltys/Edit.cshtml", loyalty);
        }

        // GET: Loyaltys/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Loyalties == null)
            {
                return NotFound();
            }

            var loyalty = await _context.Loyalties
                .Include(l => l.EventUser)
                .FirstOrDefaultAsync(m => m.EventUserId == id);
            if (loyalty == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Loyaltys/Delete.cshtml", loyalty);
        }

        // POST: Loyaltys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Loyalties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Loyalties'  is null.");
            }
            var loyalty = await _context.Loyalties.FindAsync(id);
            if (loyalty != null)
            {
                _context.Loyalties.Remove(loyalty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoyaltyExists(string id)
        {
          return (_context.Loyalties?.Any(e => e.EventUserId == id)).GetValueOrDefault();
        }
    }
}
