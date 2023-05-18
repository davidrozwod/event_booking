namespace event_booking.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
    }
}
