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
    public class VipsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VipsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Vips
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Vips.Include(v => v.Event);
                return View("~/Views/CRUDs/Vips/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }


        // GET: Vips/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vips == null)
            {
                return NotFound();
            }

            var vip = await _context.Vips
                .Include(v => v.Event)
                .FirstOrDefaultAsync(m => m.VipId == id);
            if (vip == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Vips/Details.cshtml", vip);
        }

        // GET: Vips/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            return View("~/Views/CRUDs/Vips/Create.cshtml");
        }

        // POST: Vips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("VipId,EventId,VipName,Description,VipPrice")] Vip vip)
        {
            ModelState.Remove("Event");
            if (ModelState.IsValid)
            {
                _context.Add(vip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", vip.EventId);
            return View("~/Views/CRUDs/Vips/Create.cshtml", vip);
        }

        // GET: Vips/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vips == null)
            {
                return NotFound();
            }

            var vip = await _context.Vips.FindAsync(id);
            if (vip == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", vip.EventId);
            return View("~/Views/CRUDs/Vips/Edit.cshtml", vip);
        }

        // POST: Vips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("VipId,EventId,VipName,Description,VipPrice")] Vip vip)
        {
            if (id != vip.VipId)
            {
                return NotFound();
            }

            ModelState.Remove("Event");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VipExists(vip.VipId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", vip.EventId);
            return View("~/Views/CRUDs/Vips/Edit.cshtml", vip);
        }

        // GET: Vips/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vips == null)
            {
                return NotFound();
            }

            var vip = await _context.Vips
                .Include(v => v.Event)
                .FirstOrDefaultAsync(m => m.VipId == id);
            if (vip == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Vips/Delete.cshtml", vip);
        }

        // POST: Vips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vips == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vips'  is null.");
            }
            var vip = await _context.Vips.FindAsync(id);
            if (vip != null)
            {
                _context.Vips.Remove(vip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool VipExists(int id)
        {
          return (_context.Vips?.Any(e => e.VipId == id)).GetValueOrDefault();
        }
    }
}
