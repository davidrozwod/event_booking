using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

/// <summary>
/// Ticket Pricing Information
/// </summary>
[Table("Discount", Schema = "evnt")]
public partial class Discount
{
    [Key]
    [Column("DiscountID")]
    public int DiscountId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DiscountName { get; set; }

    public int? PriceMultiplier { get; set; }

    [InverseProperty("Discount")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
