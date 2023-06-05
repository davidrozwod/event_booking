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
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SalesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Sales
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Sales.Include(s => s.EventUser).Include(s => s.Purchase);
                return View("~/Views/CRUDs/Sales/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: Sales/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.EventUser)
                .Include(s => s.Purchase)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Sales/Details.cshtml", sale);
        }

        // GET: Sales/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId");
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId");
            return View("~/Views/CRUDs/Sales/Create.cshtml");
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SaleId,EventUserId,PurchaseId,SaleDate,SalePrice")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", sale.EventUserId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", sale.PurchaseId);
            return View("~/Views/CRUDs/Sales/Create.cshtml", sale);
        }

        // GET: Sales/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", sale.EventUserId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", sale.PurchaseId);
            return View("~/Views/CRUDs/Sales/Edit.cshtml", sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,EventUserId,PurchaseId,SaleDate,SalePrice")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", sale.EventUserId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", sale.PurchaseId);
            return View("~/Views/CRUDs/Sales/Edit.cshtml", sale);
        }

        // GET: Sales/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.EventUser)
                .Include(s => s.Purchase)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Sales/Delete.cshtml", sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sales'  is null.");
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool SaleExists(int id)
        {
          return (_context.Sales?.Any(e => e.SaleId == id)).GetValueOrDefault();
        }
    }
}
