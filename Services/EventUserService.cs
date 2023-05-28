using event_booking.Data;
using event_booking.Models;
using Microsoft.EntityFrameworkCore;

public interface IEventUserService
{
    Task<EventUser> GetEventUserAsync(string userId);
    Task UpdateEventUserPictureAsync(string userId, string pictureUrl);
    // ... add other methods as needed ...
}

public class EventUserService : IEventUserService
{
    private readonly ApplicationDbContext _context;

    public EventUserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EventUser> GetEventUserAsync(string userId)
    {
        return await _context.EventUsers.FindAsync(userId);
    }

    public async Task UpdateEventUserPictureAsync(string userId, string pictureUrl)
    {
        var eventUser = await GetEventUserAsync(userId);
        if (eventUser != null)
        {
            eventUser.Picture = pictureUrl;
            _context.Entry(eventUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    // ... add other methods as needed ...
}