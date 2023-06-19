using event_booking.Models;

namespace event_booking.Models.ViewModels
{    public class TicketPriceViewModel
    {
        public Event Event { get; set; }

        public Venue Venue { get; set; }

        public Seat Seat { get; set; }

        public Ticket Ticket { get; set; }

        public Dictionary<int, string> DiscountNames { get; set; }

        public Dictionary<int, decimal> Discounts { get; set; }

        public List<Ticket> Tickets { get; set; }
        public EventUser EventUser { get; set; }
    }
}
