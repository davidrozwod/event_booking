using static System.Collections.Specialized.BitVector32;

namespace event_booking.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int VenueId { get; set; }
        public int SectionId { get; set; }
        public string? SeatNumber { get; set; }

        // Navigation properties
        public virtual Venue? Venue { get; set; }
        public virtual Section? Section { get; set; }
    }

}
