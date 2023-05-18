using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class TicketType
    {
        public int TicketTypeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TypeName { get; set; }
        public decimal PriceMultiplier { get; set; }

        [InverseProperty("TicketType")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

}
