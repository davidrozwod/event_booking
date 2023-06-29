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
    [Authorize(Roles = "Admin")]
    public class TicketTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketTypesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TicketTypes
        
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Admin"))
            {
                return _context.TicketTypes != null ?
                          View("~/Views/CRUDs/TicketTypes/Index.cshtml", await _context.TicketTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TicketTypes'  is null.");
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: TicketTypes/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketTypes == null)
            {
                return NotFound();
            }

            var ticketType = await _context.TicketTypes
                .FirstOrDefaultAsync(m => m.TicketTypeId == id);
            if (ticketType == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/TicketTypes/Details.cshtml", ticketType);
        }

        // GET: TicketTypes/Create
        
        public IActionResult Create()
        {
            return View("~/Views/CRUDs/TicketTypes/Create.cshtml");
        }

        // POST: TicketTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("TicketTypeId,TypeName,PriceMultiplier")] TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CRUDs/TicketTypes/Create.cshtml", ticketType);
        }

        // GET: TicketTypes/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketTypes == null)
            {
                return NotFound();
            }

            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType == null)
            {
                return NotFound();
            }
            return View("~/Views/CRUDs/TicketTypes/Edit.cshtml", ticketType);
        }

        // POST: TicketTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("TicketTypeId,TypeName,PriceMultiplier")] TicketType ticketType)
        {
            if (id != ticketType.TicketTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketTypeExists(ticketType.TicketTypeId))
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
            return View("~/Views/CRUDs/TicketTypes/Edit.cshtml", ticketType);
        }

        // GET: TicketTypes/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketTypes == null)
            {
                return NotFound();
            }

            var ticketType = await _context.TicketTypes
                .FirstOrDefaultAsync(m => m.TicketTypeId == id);
            if (ticketType == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/TicketTypes/Delete.cshtml", ticketType);
        }

        // POST: TicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TicketTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TicketTypes'  is null.");
            }
            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType != null)
            {
                _context.TicketTypes.Remove(ticketType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool TicketTypeExists(int id)
        {
          return (_context.TicketTypes?.Any(e => e.TicketTypeId == id)).GetValueOrDefault();
        }
    }
}
