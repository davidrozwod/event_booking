using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Collections.Specialized.BitVector32;

namespace event_booking.Models
{
    public partial class Seat
    {
        public int SeatId { get; set; }
        public int VenueId { get; set; }
        public int SectionId { get; set; }
        public string? SeatNumber { get; set; }

        [ForeignKey("SectionId")]
        [InverseProperty("Seats")]
        public virtual Section? Section { get; set; }

        [InverseProperty("Seat")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [ForeignKey("VenueId")]
        [InverseProperty("Seats")]
        public virtual Venue? Venue { get; set; }
    }

}
