using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("Sales", Schema = "evnt")]
[Index("EventUserId", Name = "IX_Sales_EventUserID")]
[Index("PurchaseId", Name = "IX_Sales_PurchaseID")]
public partial class Sale
{
    [Key]
    [Column("SaleID")]
    public int SaleId { get; set; }

    [Column("EventUserID")]
    public string? EventUserId { get; set; }

    [Column("PurchaseID")]
    public int? PurchaseId { get; set; }

    [Column(TypeName = "date")]
    public DateTime? SaleDate { get; set; }

    public int? SalePrice { get; set; }

    //Relationships
    [ForeignKey("EventUserId")]
    [InverseProperty("Sales")]
    public virtual EventUser? EventUser { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("Sales")]
    public virtual Purchase? Purchase { get; set; }
}
