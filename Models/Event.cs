using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_booking.Models
{
    public partial class Event
    {
        public int EventId { get; set; }

        public int EventCategoryId { get; set; }

        public int OrganizerId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; } = null!;

        [StringLength(250)]
        public string? Description { get; set; }
        public string? Location { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDateTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDateTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime EarlyBirdCutoff { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }




        [ForeignKey("EventCategoryId")]
        [InverseProperty("Events")]
        public virtual EventCategory EventCategory { get; set; } = null!;

        [ForeignKey("OrganizerId")]
        [InverseProperty("Events")]
        public virtual Organizer? Organizer { get; set; } = null!;

        [InverseProperty("Event")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [InverseProperty("Event")]
        public virtual ICollection<Vip> Vips { get; set; } = new List<Vip>();

        [ForeignKey("EventId")]
        [InverseProperty("Events")]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
