using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace event_booking.Models
{
    public partial class Venue
    {
        public int VenueId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Location { get; set; }

        [Unicode(false)]
        public string? Description { get; set; }

        [Column("Seat Capacity")]
        public int? SeatCapacity { get; set; }

        [InverseProperty("Venue")]
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

        [InverseProperty("Venue")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [ForeignKey("VenueId")]
        [InverseProperty("Venues")]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
