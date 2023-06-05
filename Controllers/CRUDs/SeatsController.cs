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
    public class SeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seats
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Seats.Include(s => s.Section).Include(s => s.Venue);
            return View("~/Views/CRUDs/Seats/Index.cshtml", await applicationDbContext.ToListAsync());
        }

        // GET: Seats/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seats == null)
            {
                return NotFound();
            }

            var seat = await _context.Seats
                .Include(s => s.Section)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(m => m.SeatId == id);
            if (seat == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Seats/Details.cshtml", seat);
        }

        // GET: Seats/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId");
            return View("~/Views/CRUDs/Seats/Create.cshtml");
        }

        // POST: Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SeatId,VenueId,SectionId,SeatNumber")] Seat seat)
        {
            ModelState.Remove("Venue");
            if (ModelState.IsValid)
            {
                _context.Add(seat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", seat.SectionId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", seat.VenueId);
            return View("~/Views/CRUDs/Seats/Create.cshtml", seat);
        }

        [Authorize]
        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seats == null)
            {
                return NotFound();
            }

            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", seat.SectionId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", seat.VenueId);
            return View("~/Views/CRUDs/Seats/Edit.cshtml", seat);
        }


        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("SeatId,VenueId,SectionId,SeatNumber")] Seat seat)
        {
            if (id != seat.SeatId)
            {
                return NotFound();
            }

            ModelState.Remove("Venue");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatExists(seat.SeatId))
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

            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", seat.SectionId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", seat.VenueId);
            return View("~/Views/CRUDs/Seats/Edit.cshtml", seat);
        }

        // GET: Seats/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seats == null)
            {
                return NotFound();
            }

            var seat = await _context.Seats
                .Include(s => s.Section)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(m => m.SeatId == id);
            if (seat == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Seats/Delete.cshtml", seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Seats'  is null.");
            }
            var seat = await _context.Seats.FindAsync(id);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool SeatExists(int id)
        {
          return (_context.Seats?.Any(e => e.SeatId == id)).GetValueOrDefault();
        }
    }
}
