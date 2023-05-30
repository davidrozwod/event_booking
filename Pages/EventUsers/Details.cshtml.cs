using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;

namespace event_booking.Pages.EventUsers
{
    public class DetailsModel : PageModel
    {
        private readonly event_booking.Data.ApplicationDbContext _context;

        public DetailsModel(event_booking.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public EventUser EventUser { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventuser = await _context.EventUsers.FirstOrDefaultAsync(m => m.EventUserId == id);
            if (eventuser == null)
            {
                return NotFound();
            }
            else 
            {
                EventUser = eventuser;
            }
            return Page();
        }
    }
}
