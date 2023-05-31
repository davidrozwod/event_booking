using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using event_booking.Data;
using event_booking.Models;

namespace event_booking.Pages.EventUsers
{
    public class CreateModel : PageModel
    {
        private readonly event_booking.Data.ApplicationDbContext _context;

        public CreateModel(event_booking.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EventUserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public EventUser EventUser { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.EventUsers == null || EventUser == null)
            {
                return Page();
            }

            _context.EventUsers.Add(EventUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
