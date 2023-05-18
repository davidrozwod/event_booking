namespace event_booking.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Image { get; set; }
        public DateTime EarlyBirdCutoff { get; set; }

        // Foreign key
        public int EventCategoryId { get; set; }
        public int VenueId { get; set; }
        public int OrganizerId { get; set; }

        // Navigation properties
        public virtual EventCategory? EventCategory { get; set; }
        public virtual Venue? Venue { get; set; }
        public virtual Organizer? Organizer { get; set; }
    }
}
