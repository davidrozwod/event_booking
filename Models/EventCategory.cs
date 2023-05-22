using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("EventCategories", Schema = "evnt")]
public partial class EventCategory
{
    [Key]
    [Column("EventCategoryID")]
    public int EventCategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    [InverseProperty("EventCategory")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
