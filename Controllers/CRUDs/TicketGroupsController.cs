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
    public class TicketGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketGroupsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: TicketGroups
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.TicketGroups.Include(t => t.GroupDiscount).Include(t => t.Purchase);
                return View("~/Views/CRUDs/TicketGroups/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: TicketGroups/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketGroups == null)
            {
                return NotFound();
            }

            var ticketGroup = await _context.TicketGroups
                .Include(t => t.GroupDiscount)
                .Include(t => t.Purchase)
                .FirstOrDefaultAsync(m => m.TicketGroupId == id);
            if (ticketGroup == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/TicketGroups/Details.cshtml", ticketGroup);
        }

        // GET: TicketGroups/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["GroupDiscountId"] = new SelectList(_context.GroupDiscounts, "GroupDiscountId", "GroupDiscountId");
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId");
            return View("~/Views/CRUDs/TicketGroups/Create.cshtml");
        }

        // POST: TicketGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TicketGroupId,PurchaseId,GroupDiscountId")] TicketGroup ticketGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupDiscountId"] = new SelectList(_context.GroupDiscounts, "GroupDiscountId", "GroupDiscountId", ticketGroup.GroupDiscountId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", ticketGroup.PurchaseId);
            return View("~/Views/CRUDs/TicketGroups/Create.cshtml", ticketGroup);
        }

        // GET: TicketGroups/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketGroups == null)
            {
                return NotFound();
            }

            var ticketGroup = await _context.TicketGroups.FindAsync(id);
            if (ticketGroup == null)
            {
                return NotFound();
            }
            ViewData["GroupDiscountId"] = new SelectList(_context.GroupDiscounts, "GroupDiscountId", "GroupDiscountId", ticketGroup.GroupDiscountId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", ticketGroup.PurchaseId);
            return View("~/Views/CRUDs/TicketGroups/Edit.cshtml", ticketGroup);
        }

        // POST: TicketGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("TicketGroupId,PurchaseId,GroupDiscountId")] TicketGroup ticketGroup)
        {
            if (id != ticketGroup.TicketGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketGroupExists(ticketGroup.TicketGroupId))
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
            ViewData["GroupDiscountId"] = new SelectList(_context.GroupDiscounts, "GroupDiscountId", "GroupDiscountId", ticketGroup.GroupDiscountId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", ticketGroup.PurchaseId);
            return View("~/Views/CRUDs/TicketGroups/Edit.cshtml", ticketGroup);
        }

        // GET: TicketGroups/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketGroups == null)
            {
                return NotFound();
            }

            var ticketGroup = await _context.TicketGroups
                .Include(t => t.GroupDiscount)
                .Include(t => t.Purchase)
                .FirstOrDefaultAsync(m => m.TicketGroupId == id);
            if (ticketGroup == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/TicketGroups/Delete.cshtml", ticketGroup);
        }

        // POST: TicketGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TicketGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TicketGroups'  is null.");
            }
            var ticketGroup = await _context.TicketGroups.FindAsync(id);
            if (ticketGroup != null)
            {
                _context.TicketGroups.Remove(ticketGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool TicketGroupExists(int id)
        {
          return (_context.TicketGroups?.Any(e => e.TicketGroupId == id)).GetValueOrDefault();
        }
    }
}
