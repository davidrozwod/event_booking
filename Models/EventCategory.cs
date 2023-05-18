using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class EventCategory
    {
        [Key]
        public int EventCategoryId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CategoryName { get; set; } = null!;

        [InverseProperty("EventCategory")]
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }

}
