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
    public class GroupDiscountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GroupDiscountsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: GroupDiscounts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && User.IsInRole("Admin"))
            {
                return _context.GroupDiscounts != null ?
                          View("~/Views/CRUDs/GroupDiscounts/Index.cshtml", await _context.GroupDiscounts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GroupDiscounts'  is null.");
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: GroupDiscounts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GroupDiscounts == null)
            {
                return NotFound();
            }

            var groupDiscount = await _context.GroupDiscounts
                .FirstOrDefaultAsync(m => m.GroupDiscountId == id);
            if (groupDiscount == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/GroupDiscounts/Details.cshtml", groupDiscount);
        }

        // GET: GroupDiscounts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View("~/Views/CRUDs/GroupDiscounts/Create.cshtml");
        }

        // POST: GroupDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("GroupDiscountId,GroupName,MinimumAdults,MinimumChildren,PriceMultiplier")] GroupDiscount groupDiscount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CRUDs/GroupDiscounts/Create.cshtml", groupDiscount);
        }

        // GET: GroupDiscounts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GroupDiscounts == null)
            {
                return NotFound();
            }

            var groupDiscount = await _context.GroupDiscounts.FindAsync(id);
            if (groupDiscount == null)
            {
                return NotFound();
            }
            return View("~/Views/CRUDs/GroupDiscounts/Edit.cshtml", groupDiscount);
        }

        // POST: GroupDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("GroupDiscountId,GroupName,MinimumAdults,MinimumChildren,PriceMultiplier")] GroupDiscount groupDiscount)
        {
            if (id != groupDiscount.GroupDiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupDiscount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupDiscountExists(groupDiscount.GroupDiscountId))
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
            return View("~/Views/CRUDs/GroupDiscounts/Edit.cshtml", groupDiscount);
        }

        // GET: GroupDiscounts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GroupDiscounts == null)
            {
                return NotFound();
            }

            var groupDiscount = await _context.GroupDiscounts
                .FirstOrDefaultAsync(m => m.GroupDiscountId == id);
            if (groupDiscount == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/GroupDiscounts/Delete.cshtml", groupDiscount);
        }

        // POST: GroupDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GroupDiscounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GroupDiscounts'  is null.");
            }
            var groupDiscount = await _context.GroupDiscounts.FindAsync(id);
            if (groupDiscount != null)
            {
                _context.GroupDiscounts.Remove(groupDiscount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool GroupDiscountExists(int id)
        {
          return (_context.GroupDiscounts?.Any(e => e.GroupDiscountId == id)).GetValueOrDefault();
        }
    }
}
