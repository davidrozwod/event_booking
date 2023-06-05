using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace event_booking.Controllers.CRUDs
{
    public class EventUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EventUsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EventUsers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventUsers.Include(e => e.IdentityUser);
            return View("~/Views/CRUDs/EventUsers/Index.cshtml", await applicationDbContext.ToListAsync());
        }

        // GET: EventUsers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.EventUserId == id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/EventUsers/Details.cshtml", eventUser);
        }

        // GET: EventUsers/Create
        [Authorize]
        public IActionResult Create()
        {
            /*ViewData["EventUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View("~/Views/CRUDs/EventUsers/Create.cshtml");*/
            var users = _userManager.Users.ToList();

            ViewData["EventUserId"] = users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.UserName
            }).ToList();

            return View("~/Views/CRUDs/EventUsers/Create.cshtml");
        }

        // POST: EventUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("EventUserId,FirstName,LastName,Age,Picture,Document")] EventUser eventUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventUserId"] = new SelectList(_context.Users, "Id", "Id", eventUser.EventUserId);
            return View("~/Views/CRUDs/EventUsers/Create.cshtml", eventUser);
        }

        // GET: EventUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers.FindAsync(id);
            if (eventUser == null)
            {
                return NotFound();
            }
            ViewData["EventUserId"] = new SelectList(_context.Users, "Id", "Id", eventUser.EventUserId);
            return View("~/Views/CRUDs/EventUsers/Edit.cshtml", eventUser);
        }

        // POST: EventUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string id, [Bind("EventUserId,FirstName,LastName,Age,Picture,Document")] EventUser eventUser)
        {
            if (id != eventUser.EventUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventUserExists(eventUser.EventUserId))
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
            ViewData["EventUserId"] = new SelectList(_context.Users, "Id", "Id", eventUser.EventUserId);
            return View("~/Views/CRUDs/EventUsers/Edit.cshtml", eventUser);
        }

        // GET: EventUsers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.EventUserId == id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/EventUsers/Delete.cshtml", eventUser);
        }

        // POST: EventUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EventUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventUsers'  is null.");
            }
            var eventUser = await _context.EventUsers.FindAsync(id);
            if (eventUser != null)
            {
                _context.EventUsers.Remove(eventUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool EventUserExists(string id)
        {
          return (_context.EventUsers?.Any(e => e.EventUserId == id)).GetValueOrDefault();
        }
    }
}
