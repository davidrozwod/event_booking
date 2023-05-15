﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Venue", Schema = "evnt")]
public partial class Venue
{
    [Key]
    [Column("VenueID")]
    public int VenueId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Location { get; set; }

    [Unicode(false)]
    public string? Description { get; set; }

    [Column("Seat Capacity")]
    public int? SeatCapacity { get; set; }

    [InverseProperty("Venue")]
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [InverseProperty("Venue")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("VenueId")]
    [InverseProperty("Venues")]
    public virtual ICollection<AspNetUser> Ids { get; set; } = new List<AspNetUser>();
}