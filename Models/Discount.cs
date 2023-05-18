using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? DiscountName { get; set; }
        public decimal PriceMultiplier { get; set; }

        [InverseProperty("Discount")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

}
