using event_booking.Models;
using System.Threading.Tasks;

namespace event_booking.Services.Interfaces
{
    public interface IEventUserService
    {
        Task<EventUser> GetEventUserByIdAsync(string userId);
        Task UpdateEventUserPictureAsync(string userId, string pictureUrl);
        Task<string> GetEventUserPictureUrlAsync(string userId);
    }
}
