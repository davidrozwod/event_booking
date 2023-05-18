namespace event_booking.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int PurchaseId { get; set; }
        public string? Id { get; set; } 
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }

        public virtual Purchase? Purchase { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }

}
