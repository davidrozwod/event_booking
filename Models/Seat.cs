using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Seats", Schema = "evnt")]
[Index("SectionId", Name = "IX_Seats_SectionID")]
[Index("VenueId", Name = "IX_Seats_VenueID")]
public partial class Seat
{
    [Key]
    [Column("SeatID")]
    public int SeatId { get; set; }

    /// <summary>
    /// Seats Information
    /// </summary>
    [Column("VenueID")]
    public int VenueId { get; set; }

    [Column("SectionID")]
    public int? SectionId { get; set; }

    public int SeatNumber { get; set; }

    //Relationships
    [ForeignKey("SectionId")]
    [InverseProperty("Seats")]
    public virtual Section? Section { get; set; }

    [InverseProperty("Seat")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("VenueId")]
    [InverseProperty("Seats")]
    public virtual Venue Venue { get; set; } = null!;
}
