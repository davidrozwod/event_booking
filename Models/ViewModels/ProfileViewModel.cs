using Microsoft.AspNetCore.Identity;

namespace event_booking.Models.ViewModels
{
    public class ProfileViewModel
    {
        public EventUser EventUser { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}