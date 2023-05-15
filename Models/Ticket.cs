﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

/// <summary>
/// Event Tickets
/// </summary>
[Table("Tickets", Schema = "evnt")]
[Index("EventId", "SeatId", Name = "Ticket_Unique_Index", IsUnique = true)]
public partial class Ticket
{
    [Key]
    [Column("TicketID")]
    public int TicketId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [Column("VenueID")]
    public int VenueId { get; set; }

    [Column("SeatID")]
    public int SeatId { get; set; }

    [StringLength(450)]
    public string? Id { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("DiscountID")]
    public int? DiscountId { get; set; }

    [Column("TicketTypeID")]
    public int? TicketTypeId { get; set; }

    [Column("PurchaseID")]
    public int? PurchaseId { get; set; }

    public int BasePrice { get; set; }

    public int? TicketPrice { get; set; }

    [ForeignKey("DiscountId")]
    [InverseProperty("Tickets")]
    public virtual Discount? Discount { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Tickets")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("Tickets")]
    public virtual AspNetUser? IdNavigation { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("Tickets")]
    public virtual Purchase? Purchase { get; set; }

    [ForeignKey("SeatId")]
    [InverseProperty("Tickets")]
    public virtual Seat Seat { get; set; } = null!;

    [ForeignKey("TicketTypeId")]
    [InverseProperty("Tickets")]
    public virtual TicketType? TicketType { get; set; }

    [ForeignKey("VenueId")]
    [InverseProperty("Tickets")]
    public virtual Venue Venue { get; set; } = null!;

    [ForeignKey("TicketId")]
    [InverseProperty("Tickets")]
    public virtual ICollection<Vip> Vips { get; set; } = new List<Vip>();
}