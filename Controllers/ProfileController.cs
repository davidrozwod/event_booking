using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;
using Microsoft.AspNetCore.Identity;

namespace event_booking.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EventUser
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View(eventUser);
        }

        // GET: EventUser/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers.FirstOrDefaultAsync(e => e.EventUserId == user.Id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View(eventUser);
        }

        // POST: EventUser/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return View(eventUser);
        }

        private bool EventUserExists(string id)
        {
            return _context.EventUsers.Any(e => e.EventUserId == id);
        }
    }
}