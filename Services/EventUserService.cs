using event_booking.Data;
using event_booking.Models;
using Microsoft.EntityFrameworkCore;

public interface IEventUserService
{
    Task<EventUser> GetEventUserAsync(string userId);

    //Pictrure
    Task UpdateEventUserPictureAsync(string userId, string pictureUrl);
    Task<string> GetEventUserPictureUrlAsync(string userId);

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

    //Picture
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

    public async Task<string> GetEventUserPictureUrlAsync(string userId)
    {
        var eventUser = await GetEventUserAsync(userId);
        return eventUser?.Picture;
    }

    // ... add other methods as needed ...
}