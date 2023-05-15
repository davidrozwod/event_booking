using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Index("NormalizedEmail", Name = "EmailIndex")]
public partial class AspNetUser
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? NormalizedUserName { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [StringLength(256)]
    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FisrtName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    public int? Age { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Picture { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Document { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Loyalty? Loyalty { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("IdNavigation")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("Id")]
    [InverseProperty("Ids")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [ForeignKey("Id")]
    [InverseProperty("Ids")]
    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();

    [ForeignKey("Id")]
    [InverseProperty("Ids")]
    public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
}
