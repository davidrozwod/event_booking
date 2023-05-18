namespace event_booking.Models
{
    public class TicketType
    {
        public int TicketTypeId { get; set; }
        public string? TypeName { get; set; }
        public decimal PriceMultiplier { get; set; }
    }

}
