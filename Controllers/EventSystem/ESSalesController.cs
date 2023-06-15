using event_booking.Data;
using event_booking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace event_booking.Controllers.EventSystem
{
    [Authorize(Roles = "Admin, Promoter")]
    public class ESSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ESSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sales = await _context.Sales
                .Include(s => s.EventUser)
                .Include(s => s.Purchase)
                .Where(s => s.EventUserId == userId && s.PurchaseId == null)
                .ToListAsync();

            return View("~/Views/EventSystem/Sales/Index.cshtml", sales);
        }

        // POST: Sales/AddToCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            if (ticket != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var sale = new Sale
                {
                    EventUserId = userId,
                    SaleDate = DateTime.Today,
                    SalePrice = ticket.TicketPrice
                };

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                var purchase = new Purchase();

                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();

                ticket.PurchaseId = purchase.PurchaseId;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Sales/RemoveFromCart/5
        public async Task<IActionResult> RemoveFromCart(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);

            if (sale != null)
            {
                var tickets = await _context.Tickets
                    .Where(t => t.PurchaseId == sale.PurchaseId)
                    .ToListAsync();

                foreach (var ticket in tickets)
                {
                    ticket.PurchaseId = null;
                }

                _context.Sales.Remove(sale);
                _context.Tickets.UpdateRange(tickets);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Sales/Checkout
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sales = await _context.Sales
                .Include(s => s.EventUser)
                .Include(s => s.Purchase)
                .Where(s => s.EventUserId == userId && s.PurchaseId == null)
                .ToListAsync();

            if (sales.Any())
            {
                var purchase = new Purchase();

                _context.Purchases.Add(purchase);

                foreach (var sale in sales)
                {
                    sale.PurchaseId = purchase.PurchaseId;
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
