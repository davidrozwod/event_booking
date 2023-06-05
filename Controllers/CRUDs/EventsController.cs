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
using System.Security.Claims;

namespace event_booking.Controllers.CRUDs
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EventsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Events
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Promoter"))
            {
                var applicationDbContext = _context.Events.Include(e => e.EventCategory).Include(f => f.Organizer).Where(e => e.CreatedByUserId == currentUser.Id);
                return View("~/Views/CRUDs/Events/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else if (currentUser != null && User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Events.Include(e => e.EventCategory).Include(f => f.Organizer);
                return View("~/Views/CRUDs/Events/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else 
            {
                TempData["ErrorMessage"] = "Access restricted to admin and promoter accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: Events/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.EventCategory)
                .Include(f => f.Organizer)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Events/Details.cshtml", @event);
        }

        // GET: Events/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Organizers = _context.Organizers.ToList();
            ViewBag.EventCategories = _context.EventCategories.ToList();
            return View("~/Views/CRUDs/Events/Create.cshtml");
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("EventId,OrganizerId,EventCategoryId,Name,Description,StartDateTime,EndDateTime,Image,EarlyBirdCutoff")] Event @event)
        {
            ModelState.Remove("Organizer");
            ModelState.Remove("EventCategory");
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null)
                {
                    @event.CreatedByUserId = currentUser.Id;
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Organizers = _context.Organizers.ToList();
            ViewBag.EventCategories = _context.EventCategories.ToList();
            return View("~/Views/CRUDs/Events/Create.cshtml", @event);
        }

        // GET: Events/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewBag.Organizers = _context.Organizers.ToList();
            ViewBag.EventCategories = _context.EventCategories.ToList();
            return View("~/Views/CRUDs/Events/Edit.cshtml", @event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,OrganizerId,EventCategoryId,Name,Description,StartDateTime,EndDateTime,Image,EarlyBirdCutoff")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            ModelState.Remove("Organizer");
            ModelState.Remove("EventCategory");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryId", @event.EventCategoryId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "OrganizerId", "OrganizerId", @event.OrganizerId);
            return View("~/Views/CRUDs/Events/Edit.cshtml", @event);
        }

        // GET: Events/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.EventCategory)
                .Include(f => f.Organizer)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Events/Delete.cshtml", @event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
