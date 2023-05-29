using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

/// <summary>
/// VIP Area
/// </summary>
[Table("VIP", Schema = "evnt")]
[Index("EventId", Name = "IX_VIP_EventID")]
[Index("VipId", "EventId", Name = "VIP_Unique_Index", IsUnique = true)]
public partial class Vip
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("VIP_ID")]
    public int VipId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [Column("VIP_Name")]
    [StringLength(50)]
    public string VipName { get; set; } = null!;

    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("VIP_Price")]
    public decimal VipPrice { get; set; }

    //Relationships
    [ForeignKey("EventId")]
    [InverseProperty("Vips")]
    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
