using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("EventUser", Schema = "evnt")]
public partial class EventUser
{
    [Key]
    [Column("EventUserID")]
    [ForeignKey("IdentityUser")]//changed from aspnetuser table to reference the ASP.NET Core Identity <tkey> [IdentityUser]
    public string EventUserId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    [StringLength(450)]
    [Unicode(false)]
    public string? Picture { get; set; }

    [StringLength(450)]
    [Unicode(false)]
    public string? Document { get; set; }

    //IdentityUser Relationship
    public virtual IdentityUser? IdentityUser { get; set; } // is the navigation property that points to the associated IdentityUser

    //Relationships
    [InverseProperty("EventUser")]
    public virtual Loyalty? Loyalty { get; set; }

    [InverseProperty("EventUser")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("EventUser")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();

    public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
}