using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("VIP", Schema = "evnt")]
[Index("VipId", "EventId", Name = "VIP_Unique_Index", IsUnique = true)]
public partial class Vip
{
    [Key]
    [Column("VIP_ID")]
    public int VipId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [Column("VIP_Name")]
    [StringLength(50)]
    public string VipName { get; set; } = null!;

    [Unicode(false)]
    public string? Description { get; set; }

    [Column("VIP_Price")]
    public int VipPrice { get; set; }

    //Relationships
    [ForeignKey("EventId")]
    [InverseProperty("Vips")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("VipId")]
    [InverseProperty("Vips")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
