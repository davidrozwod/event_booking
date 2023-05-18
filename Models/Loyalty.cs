using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace event_booking.Models
{
    public class Loyalty
    {
        public string? LoyaltyID { get; set; }
        public string? Id { get; set; }
        public int TicketCount { get; set; }
        public decimal PriceMultiplier { get; set; }

        // Navigation property
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }

}
