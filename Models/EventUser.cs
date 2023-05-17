using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("EventUser", Schema = "evnt")]
public partial class EventUser
{
    [Key]
    [Column("EventUserID")]
    public string EventUserId { get; set; } = null!;

    public int? FirstName { get; set; }

    public int? LastName { get; set; }

    public int? Age { get; set; }

    public int? Picture { get; set; }

    public int? Document { get; set; }

    [InverseProperty("EventUser")]
    public virtual Loyalty? Loyalty { get; set; }

    [InverseProperty("EventUser")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("EventUser")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("EventUserId")]
    [InverseProperty("EventUsers")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [ForeignKey("EventUserId")]
    [InverseProperty("EventUsers")]
    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();

    [ForeignKey("EventUserId")]
    [InverseProperty("EventUsers")]
    public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
}
