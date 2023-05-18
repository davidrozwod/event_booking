namespace event_booking.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string? DiscountName { get; set; }
        public decimal PriceMultiplier { get; set; }
    }

}
