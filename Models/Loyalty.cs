using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Loyalty", Schema = "evnt")]
public partial class Loyalty
{
    [Key]
    [Column("EventUserID")]
    public string EventUserId { get; set; } = null!;

    public int? TicketCount { get; set; }

    public decimal? PriceMultiplier { get; set; }

    //Relationships
    [ForeignKey("EventUserId")]
    [InverseProperty("Loyalty")]
    public virtual EventUser EventUser { get; set; } = null!;
}
