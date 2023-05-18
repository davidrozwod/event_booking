using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Organizer
    {
        [Key]
        public int OrganizerId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; } = null!;


        [StringLength(50)]
        [Unicode(false)]
        public string? Description { get; set; } = null!;


        [StringLength(100)]
        [Unicode(false)]
        public string? Image { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? ContactInfo { get; set; }

        [InverseProperty("Organizer")]
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

        [ForeignKey("OrganizerCategoryId")]
        [InverseProperty("Organizers")]
        public virtual OrganizerCategory OrganizerCategory { get; set; } = null!;

        [ForeignKey("OrganizerId")]
        [InverseProperty("Organizers")]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }

}
