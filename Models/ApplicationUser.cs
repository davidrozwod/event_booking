using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace event_booking.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }

        public int? Picture { get; set; }

        public int? Document { get; set; }

        [InverseProperty("ApplicationUser")]
        public virtual Loyalty? Loyalty { get; set; }

        [InverseProperty("ApplicationUser")]
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        [InverseProperty("ApplicationUser")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [ForeignKey("Id")]
        [InverseProperty("ApplicationUsers")]
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

        [ForeignKey("Id")]
        [InverseProperty("ApplicationUsers")]
        public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();

        [ForeignKey("Id")]
        [InverseProperty("ApplicationUsers")]
        public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
    }
}

