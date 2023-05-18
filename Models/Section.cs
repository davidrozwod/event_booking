using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Section
    {
        
        public int SectionId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? SectionName { get; set; }
        public decimal PriceMultiplier { get; set; }

        [InverseProperty("Section")]
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }

}
