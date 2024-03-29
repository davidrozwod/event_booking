﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Organizers", Schema = "evnt")]
[Index("OrganizerCategoryId", Name = "IX_Organizers_OrganizerCategoryID")]
public partial class Organizer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("OrganizerID")]
    public int OrganizerId { get; set; }

    [Column("OrganizerCategoryID")]
    public int OrganizerCategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(450)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Image { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactInfo { get; set; }

    //Relationships
    [InverseProperty("Organizer")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [ForeignKey("OrganizerCategoryId")]
    [InverseProperty("Organizers")]
    public virtual OrganizerCategory OrganizerCategory { get; set; } = null!;

    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();
}
