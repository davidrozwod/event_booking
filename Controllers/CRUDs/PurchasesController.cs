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

namespace event_booking.Controllers.CRUDs
{
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Purchases != null ? 
                          View("~/Views/CRUDs/Purchases/Index.cshtml", await _context.Purchases.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Purchases'  is null.");
        }

        // GET: Purchases/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Purchases/Details.cshtml", purchase);
        }

        // GET: Purchases/Create
        [Authorize]
        public IActionResult Create()
        {
            return View("~/Views/CRUDs/Purchases/Create.cshtml");
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PurchaseId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CRUDs/Purchases/Create.cshtml", purchase);
        }

        // GET: Purchases/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View("~/Views/CRUDs/Purchases/Edit.cshtml", purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId")] Purchase purchase)
        {
            if (id != purchase.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseId))
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
            return View("~/Views/CRUDs/Purchases/Edit.cshtml", purchase);
        }

        // GET: Purchases/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Purchases/Delete.cshtml", purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Purchases'  is null.");
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool PurchaseExists(int id)
        {
          return (_context.Purchases?.Any(e => e.PurchaseId == id)).GetValueOrDefault();
        }
    }
}
