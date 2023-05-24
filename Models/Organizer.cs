using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Organizers", Schema = "evnt")]
public partial class Organizer
{
    [Key]
    [Column("OrganizerID")]
    public int OrganizerId { get; set; }

    [Column("OrganizerCategoryID")]
    public int OrganizerCategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
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

    [ForeignKey("OrganizerId")]
    [InverseProperty("Organizers")]
    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();
}