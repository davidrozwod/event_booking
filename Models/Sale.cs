using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Sales", Schema = "evnt")]
public partial class Sale
{
    [Key]
    [Column("SaleID")]
    public int SaleId { get; set; }

    [StringLength(450)]
    public string? Id { get; set; }

    [Column("PurchaseID")]
    public int? PurchaseId { get; set; }

    [Column(TypeName = "date")]
    public DateTime? SaleDate { get; set; }

    public int? SalePrice { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Sales")]
    public virtual AspNetUser? IdNavigation { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("Sales")]
    public virtual Purchase? Purchase { get; set; }
}
