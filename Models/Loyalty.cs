using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Loyalty
    {
        public string? Id { get; set; } = null!;
        public int TicketCount { get; set; }
        public decimal PriceMultiplier { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Loyalty")]
        public virtual ApplicationUser? ApplicationUser { get; set; } = null!;
    }

}
