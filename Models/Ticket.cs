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
        public string? Id { get; set; } // Foreign key to ApplicationUser (nullable)
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DiscountId { get; set; } // Foreign key (nullable)
        public int? TicketTypeId { get; set; } // Foreign key (nullable)
        public int? PurchaseId { get; set; } // Foreign key (nullable)
        public decimal BasePrice { get; set; }
        public decimal? TicketPrice { get; set; }

        // Navigation properties
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual Event? Event { get; set; }
        public virtual Venue? Venue { get; set; }
        public virtual Seat? Seat { get; set; }
        public virtual Discount? Discount { get; set; } // Nullable relationship
        public virtual TicketType? TicketType { get; set; } // Nullable relationship
        public virtual Purchase? Purchase { get; set; } // Nullable relationship


    }

}
