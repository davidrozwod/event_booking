using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("GroupDiscounts", Schema = "evnt")]
public partial class GroupDiscount
{
    [Key]
    [Column("GroupDiscountID")]
    public int GroupDiscountId { get; set; }

    [StringLength(50)]
    public string? GroupName { get; set; }

    public int? MinimumAdults { get; set; }

    public int? MinimumChildren { get; set; }

    public int? PriceMultiplier { get; set; }

    //Relationships
    [InverseProperty("GroupDiscount")]
    public virtual ICollection<TicketGroup> TicketGroups { get; set; } = new List<TicketGroup>();
}
