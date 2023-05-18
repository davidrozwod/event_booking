using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models
{
    public partial class TicketGroup
    {
        [Key]
        [Column("TicketGroupID")]
        public int TicketGroupId { get; set; }

        [Column("PurchaseID")]
        public int? PurchaseId { get; set; }

        [Column("GroupDiscountID")]
        public int? GroupDiscountId { get; set; }

        [ForeignKey("GroupDiscountId")]
        [InverseProperty("TicketGroups")]
        public virtual GroupDiscount? GroupDiscount { get; set; }

        [ForeignKey("PurchaseId")]
        [InverseProperty("TicketGroups")]
        public virtual Purchase? Purchase { get; set; }
    }
}
