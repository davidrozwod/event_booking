using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;

namespace event_booking.Pages.EventUsers
{
    public class EditModel : PageModel
    {
        private readonly event_booking.Data.ApplicationDbContext _context;

        public EditModel(event_booking.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventUser EventUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventuser =  await _context.EventUsers.FirstOrDefaultAsync(m => m.EventUserId == id);
            if (eventuser == null)
            {
                return NotFound();
            }
            EventUser = eventuser;
           ViewData["EventUserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EventUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventUserExists(EventUser.EventUserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventUserExists(string id)
        {
          return (_context.EventUsers?.Any(e => e.EventUserId == id)).GetValueOrDefault();
        }
    }
}
