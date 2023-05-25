using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("TicketType", Schema = "evnt")]
public partial class TicketType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TicketTypeID")]
    public int TicketTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TypeName { get; set; } = null!;

    public int PriceMultiplier { get; set; }

    //Relationships
    [InverseProperty("TicketType")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
