using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Events", Schema = "evnt")]
public partial class Event
{
    [Key]
    [Column("EventID")]
    public int EventId { get; set; }

    [Column("OrganizerID")]
    public int OrganizerId { get; set; }

    [Column("EventCategoryID")]
    public int EventCategoryId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDateTime { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDateTime { get; set; }

    [StringLength(100)]
    public string? Image { get; set; }

    [Column(TypeName = "date")]
    public DateTime? EarlyBirdCutoff { get; set; }

    //Relationships
    [ForeignKey("EventCategoryId")]
    [InverseProperty("Events")]
    public virtual EventCategory EventCategory { get; set; } = null!;

    [ForeignKey("OrganizerId")]
    [InverseProperty("Events")]
    public virtual Organizer Organizer { get; set; } = null!;

    [InverseProperty("Event")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [InverseProperty("Event")]
    public virtual ICollection<Vip> Vips { get; set; } = new List<Vip>();

    [ForeignKey("EventId")]
    [InverseProperty("Events")]
    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();
}
