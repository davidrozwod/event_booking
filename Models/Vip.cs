using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models
{

    [Index("VipId", "EventId", Name = "VIP_Unique_Index", IsUnique = true)]
    public partial class Vip
    {
        public int VipId { get; set; }

        public int EventId { get; set; }

        [StringLength(50)]
        public string VipName { get; set; } = null!;

        [Unicode(false)]
        public string? Description { get; set; }

        public int VipPrice { get; set; }

        [ForeignKey("EventId")]
        [InverseProperty("Vips")]
        public virtual Event Event { get; set; } = null!;

        [ForeignKey("VipId")]
        [InverseProperty("Vips")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
