using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        [InverseProperty("Purchase")]
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        [InverseProperty("Purchase")]
        public virtual ICollection<TicketGroup> TicketGroups { get; set; } = new List<TicketGroup>();

        [InverseProperty("Purchase")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

}
