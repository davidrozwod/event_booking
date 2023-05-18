using event_booking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Loyalty> Loyalties { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .ToTable("AspNetUsers");

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => new { t.EventId, t.SeatId })
                .IsUnique()
                .HasDatabaseName("IX_EventId_SeatId");

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Purchase)
                .WithMany()
                .HasForeignKey(s => s.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.ApplicationUser)
                .WithMany()
                .HasForeignKey(s => s.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seat>()
            .HasOne(s => s.Venue)
            .WithMany()
            .HasForeignKey(s => s.VenueId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Section)
                .WithMany()
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
            .HasOne(e => e.EventCategory)
            .WithMany()
            .HasForeignKey(e => e.EventCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany()
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany()
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Loyalty>()
                .HasOne(l => l.ApplicationUser)
                .WithOne()
                .HasForeignKey<Loyalty>(l => l.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.ApplicationUser)
                .WithMany()
                .HasForeignKey(t => t.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany()
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Venue)
                .WithMany()
                .HasForeignKey(t => t.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Discount)
                .WithMany()
                .HasForeignKey(t => t.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TicketType)
                .WithMany()
                .HasForeignKey(t => t.TicketTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Purchase)
                .WithMany()
                .HasForeignKey(t => t.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Discount>()
        .Property(d => d.PriceMultiplier)
        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Loyalty>()
                .Property(l => l.PriceMultiplier)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Purchase>()
                .Property(p => p.SalePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .Property(s => s.SalePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Section>()
                .Property(s => s.PriceMultiplier)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.BasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.TicketPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TicketType>()
                .Property(tt => tt.PriceMultiplier)
                .HasColumnType("decimal(18,2)");

        }
    }
}