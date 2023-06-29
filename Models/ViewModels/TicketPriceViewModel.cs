using event_booking.Models;
using System.Collections.Generic;

namespace event_booking.Models.ViewModels
{    public class TicketPriceViewModel
    {
        public Event Event { get; set; }

        public Venue Venue { get; set; }

        public Seat Seat { get; set; }

        public Ticket Ticket { get; set; }

        public Discount Discount { get; set; }

        public TicketType TicketType { get; set; }

        public Purchase Purchase { get; set; }

        public Dictionary<int, string> DiscountNames { get; set; }

        public Dictionary<int, decimal> Discounts { get; set; }

        public List<Section> Section { get; set; }

        public List<GroupDiscount> GroupDiscounts { get; set; }
    }
}
