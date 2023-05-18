using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Sale
    {
        public int SaleId { get; set; }
        public int PurchaseId { get; set; }
        public string? Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Sales")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("PurchaseId")]
        [InverseProperty("Sales")]
        public virtual Purchase? Purchase { get; set; }
    }

}
