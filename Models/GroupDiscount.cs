using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace event_booking.Models
{
    public partial class GroupDiscount
    {
        public int GroupDiscountId { get; set; }

        [StringLength(50)]
        public string? GroupName { get; set; }

        public int? MinimumAdults { get; set; }

        public int? MinimumChildren { get; set; }

        public int? PriceMultiplier { get; set; }

        [InverseProperty("GroupDiscount")]
        public virtual ICollection<TicketGroup> TicketGroups { get; set; } = new List<TicketGroup>();
    }
}
