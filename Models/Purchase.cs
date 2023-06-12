using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Purchase", Schema = "evnt")]
public partial class Purchase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PurchaseID")]
    public int PurchaseId { get; set; }

    public DateTime SessionExpiryTime { get; set; }

    //Relationships
    [InverseProperty("Purchase")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("Purchase")]
    public virtual ICollection<TicketGroup> TicketGroups { get; set; } = new List<TicketGroup>();

    [InverseProperty("Purchase")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
