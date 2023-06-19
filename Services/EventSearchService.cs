using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using event_booking.Data;
using event_booking.Models;
using Microsoft.EntityFrameworkCore;

public class EventSearchService
{
    private readonly ApplicationDbContext _context;

    public EventSearchService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> SearchEvents(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return new List<Event>(); // Return an empty list if the search term is null or empty
        }

        var events = await _context.Events.ToListAsync();

        var filteredEvents = events
            .Where(e => e.Name != null && e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return filteredEvents;
    }




}
