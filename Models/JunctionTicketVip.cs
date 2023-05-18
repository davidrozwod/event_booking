using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models { 
public partial class JunctionTicketVip
{
        public int TicketId { get; set; }

        public int VipId { get; set; }


        [ForeignKey("TicketId")]
        [InverseProperty("JunctionTicketVips")]
        public virtual Ticket Ticket { get; set; } = null!;

        [ForeignKey("VipId")]
        [InverseProperty("JunctionTicketVips")]
        public virtual Vip Vip { get; set; } = null!;
    }

}
