namespace event_booking.Models.ViewModels
{
    public class MyTicketsViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public DateTime? SessionExpiryTime { get; set; }
    }

}
