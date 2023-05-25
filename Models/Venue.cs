using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Venue", Schema = "evnt")]
public partial class Venue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("VenueID")]
    public int VenueId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Location { get; set; } = null!;

    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("Seat Capacity")]
    public int SeatCapacity { get; set; }

    //Relationships
    [InverseProperty("Venue")]
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [InverseProperty("Venue")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("VenueId")]
    [InverseProperty("Venues")]
    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();
}
