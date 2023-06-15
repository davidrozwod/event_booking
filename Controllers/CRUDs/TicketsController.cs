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
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets
        
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Tickets.Include(t => t.Discount).Include(t => t.Event).Include(t => t.EventUser).Include(t => t.Purchase).Include(t => t.Seat).Include(t => t.TicketType).Include(t => t.Venue);
                return View("~/Views/CRUDs/Tickets/Index.cshtml", await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["ErrorMessage"] = "Access restricted to admin accounts.";
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: Tickets/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Discount)
                .Include(t => t.Event)
                .Include(t => t.EventUser)
                .Include(t => t.Purchase)
                .Include(t => t.Seat)
                .Include(t => t.TicketType)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Tickets/Details.cshtml", ticket);
        }

        // GET: Tickets/Create
        
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.Discounts.OrderBy(d => d.DiscountName), "DiscountId", "DiscountName");
            ViewData["EventId"] = new SelectList(_context.Events.OrderBy(e => e.Name), "EventId", "Name");
            ViewData["EventUserId"] = new SelectList(_context.EventUsers.OrderBy(u => u.FirstName).ThenBy(u => u.LastName), "EventUserId", "FullName");
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId");
            ViewData["SeatId"] = new SelectList(_context.Seats.OrderBy(s => s.SeatNumber), "SeatId", "SeatNumber");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes.OrderBy(t => t.TypeName), "TicketTypeId", "TypeName");
            ViewData["VenueId"] = new SelectList(_context.Venues.OrderBy(v => v.Name), "VenueId", "Name");

            /*            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId");
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId");
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId");
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "TicketTypeId", "TicketTypeId");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId");*/
            return View("~/Views/CRUDs/Tickets/Create.cshtml");
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("TicketId,EventId,VenueId,SeatId,FirstName,LastName,DiscountId,TicketTypeId,PurchaseId,BasePrice,TicketPrice")] Ticket ticket)
        {
            ModelState.Remove("Seat");
            ModelState.Remove("Event");
            ModelState.Remove("Venue");
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId", ticket.DiscountId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", ticket.EventId);
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", ticket.EventUserId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", ticket.PurchaseId);
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId", ticket.SeatId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "TicketTypeId", "TicketTypeId", ticket.TicketTypeId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", ticket.VenueId);
            return View("~/Views/CRUDs/Tickets/Create.cshtml", ticket);
        }

        // GET: Tickets/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId", ticket.DiscountId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", ticket.EventId);
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", ticket.EventUserId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", ticket.PurchaseId);
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId", ticket.SeatId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "TicketTypeId", "TicketTypeId", ticket.TicketTypeId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", ticket.VenueId);
            return View("~/Views/CRUDs/Tickets/Edit.cshtml", ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,EventId,VenueId,SeatId,EventUserId,FirstName,LastName,DiscountId,TicketTypeId,PurchaseId,BasePrice,TicketPrice")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            ModelState.Remove("Seat");
            ModelState.Remove("Event");
            ModelState.Remove("Venue");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId", ticket.DiscountId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", ticket.EventId);
            ViewData["EventUserId"] = new SelectList(_context.EventUsers, "EventUserId", "EventUserId", ticket.EventUserId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "PurchaseId", "PurchaseId", ticket.PurchaseId);
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId", ticket.SeatId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "TicketTypeId", "TicketTypeId", ticket.TicketTypeId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", ticket.VenueId);
            return View("~/Views/CRUDs/Tickets/Edit.cshtml", ticket);
        }

        // GET: Tickets/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Discount)
                .Include(t => t.Event)
                .Include(t => t.EventUser)
                .Include(t => t.Purchase)
                .Include(t => t.Seat)
                .Include(t => t.TicketType)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View("~/Views/CRUDs/Tickets/Delete.cshtml", ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool TicketExists(int id)
        {
          return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
