using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Models;

[Table("OrganizerCategories", Schema = "evnt")]
public partial class OrganizerCategory
{
    [Key]
    [Column("OrganizerCategoryID")]
    public int OrganizerCategoryId { get; set; }

    [StringLength(50)]
    public string CategoryName { get; set; } = null!;

    [InverseProperty("OrganizerCategory")]
    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();
}
