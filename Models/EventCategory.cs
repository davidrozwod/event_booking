﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("EventCategories", Schema = "evnt")]
public partial class EventCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EventCategoryID")]
    public int EventCategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    //Relationships
    [InverseProperty("EventCategory")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
