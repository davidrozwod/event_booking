using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace event_booking.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public int SeatId { get; set; }
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DiscountId { get; set; }
        public int? TicketTypeId { get; set; }
        public int? PurchaseId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? TicketPrice { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual Event? Event { get; set; }
        public virtual Venue? Venue { get; set; }
        public virtual Seat? Seat { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual TicketType? TicketType { get; set; }
        public virtual Purchase? Purchase { get; set; }


    }

}
