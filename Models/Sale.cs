namespace event_booking.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int PurchaseId { get; set; } // Foreign key to Purchase model
        public string? Id { get; set; } // Foreign key to ApplicationUser
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }

        // Navigation properties
        public virtual Purchase? Purchase { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }

}
