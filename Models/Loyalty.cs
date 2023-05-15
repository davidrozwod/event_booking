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
    public string Id { get; set; } = null!;

    public int? TicketCount { get; set; }

    public int? PriceMultiplier { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Loyalty")]
    public virtual AspNetUser IdNavigation { get; set; } = null!;
}
