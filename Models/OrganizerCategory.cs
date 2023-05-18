using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{

    public partial class OrganizerCategory
    {

        public int OrganizerCategoryId { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; } = null!;

        [InverseProperty("OrganizerCategory")]
        public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();
    }
}
