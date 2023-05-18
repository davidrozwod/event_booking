using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace event_booking.Models
{
    [Index("EventId", "SeatId", Name = "Ticket_Unique_Index", IsUnique = true)]
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public int SeatId { get; set; }
        public string? Id { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? LastName { get; set; }
        public int? DiscountId { get; set; }
        public int? TicketTypeId { get; set; }
        public int? PurchaseId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? TicketPrice { get; set; }

        [ForeignKey("DiscountId")]
        [InverseProperty("Tickets")]
        public virtual Discount? Discount { get; set; }

        [ForeignKey("EventId")]
        [InverseProperty("Tickets")]
        public virtual Event Event { get; set; } = null!;

        [ForeignKey("Id")]
        [InverseProperty("Tickets")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("PurchaseId")]
        [InverseProperty("Tickets")]
        public virtual Purchase? Purchase { get; set; }

        [ForeignKey("SeatId")]
        [InverseProperty("Tickets")]
        public virtual Seat Seat { get; set; } = null!;

        [ForeignKey("TicketTypeId")]
        [InverseProperty("Tickets")]
        public virtual TicketType? TicketType { get; set; }

        [ForeignKey("VenueId")]
        [InverseProperty("Tickets")]
        public virtual Venue Venue { get; set; } = null!;

        [ForeignKey("TicketId")]
        [InverseProperty("Tickets")]
        public virtual ICollection<Vip> Vips { get; set; } = new List<Vip>();


    }

}
