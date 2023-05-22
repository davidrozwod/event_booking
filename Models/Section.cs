using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Section", Schema = "evnt")]
public partial class Section
{
    [Key]
    [Column("SectionID")]
    public int SectionId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SectionName { get; set; }

    public int? PriceMultiplier { get; set; }

    //Relationships
    [InverseProperty("Section")]
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
