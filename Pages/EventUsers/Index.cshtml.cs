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
    public class IndexModel : PageModel
    {
        private readonly event_booking.Data.ApplicationDbContext _context;

        public IndexModel(event_booking.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EventUser> EventUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.EventUsers != null)
            {
                EventUser = await _context.EventUsers
                .Include(e => e.IdentityUser).ToListAsync();
            }
        }
    }
}
