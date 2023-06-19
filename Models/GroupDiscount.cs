using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

/// <summary>
/// Discounts on groups
/// </summary>
[Table("GroupDiscounts", Schema = "evnt")]
public partial class GroupDiscount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("GroupDiscountID")]
    public int GroupDiscountId { get; set; }

    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    public int MinimumAdults { get; set; }

    public int MinimumChildren { get; set; }

    public decimal PriceMultiplier { get; set; }

    //Relationships
    [InverseProperty("GroupDiscount")]
    public virtual ICollection<TicketGroup> TicketGroups { get; set; } = new List<TicketGroup>();

    [InverseProperty("GroupDiscount")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
