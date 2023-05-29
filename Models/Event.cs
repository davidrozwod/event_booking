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
    public int EventId { get; set; }

    [Required]
    public int OrganizerId { get; set; }

    [Required]
    public int EventCategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(250)]
    public string? Description { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    [Required]
    public DateTime EndDateTime { get; set; }

    [StringLength(100)]
    public string? Image { get; set; }

    public DateTime? EarlyBirdCutoff { get; set; }

    //Relationships
    [ForeignKey("EventCategoryId")]
    [InverseProperty("Events")]
    public virtual EventCategory? EventCategory { get; set; }

    [ForeignKey("OrganizerId")]
    [InverseProperty("Events")]
    public virtual Organizer? Organizer { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [InverseProperty("Event")]
    public virtual ICollection<Vip> Vips { get; set; } = new List<Vip>();

    [ForeignKey("EventId")]
    [InverseProperty("Events")]
    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();
}