using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Data;

public partial class EventfulDbContext : DbContext
{
    public EventfulDbContext()
    {
    }

    public EventfulDbContext(DbContextOptions<EventfulDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<GroupDiscount> GroupDiscounts { get; set; }

    public virtual DbSet<Loyalty> Loyalties { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<OrganizerCategory> OrganizerCategories { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketGroup> TicketGroups { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    public virtual DbSet<Vip> Vips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:eventdatabase.database.windows.net,1433;Initial Catalog=eventdata;Persist Security Info=False;User ID=devninjas;Password=Scum777*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasMany(d => d.Events).WithMany(p => p.Ids)
                .UsingEntity<Dictionary<string, object>>(
                    "UserEventFollow",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Event_Follow_Events_1"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Event_Follow_AspNetUsers"),
                    j =>
                    {
                        j.HasKey("Id", "EventId");
                        j.ToTable("User_Event_Follow", "evnt");
                        j.IndexerProperty<int>("EventId").HasColumnName("EventID");
                    });

            entity.HasMany(d => d.Organizers).WithMany(p => p.Ids)
                .UsingEntity<Dictionary<string, object>>(
                    "UserOrganizerFollow",
                    r => r.HasOne<Organizer>().WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Organizer_Follow_Organizers_1"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Organizer_Follow_AspNetUsers"),
                    j =>
                    {
                        j.HasKey("Id", "OrganizerId");
                        j.ToTable("User_Organizer_Follow", "evnt");
                        j.IndexerProperty<int>("OrganizerId").HasColumnName("OrganizerID");
                    });

            entity.HasMany(d => d.Venues).WithMany(p => p.Ids)
                .UsingEntity<Dictionary<string, object>>(
                    "UserVenueFollow",
                    r => r.HasOne<Venue>().WithMany()
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Venue_Follow_Venue_1"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Venue_Follow_AspNetUsers"),
                    j =>
                    {
                        j.HasKey("Id", "VenueId");
                        j.ToTable("User_Venue_Follow", "evnt");
                        j.IndexerProperty<int>("VenueId").HasColumnName("VenueID");
                    });
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK_TicketPricing");

            entity.ToTable("Discount", "evnt", tb => tb.HasComment("Ticket Pricing Information"));

            entity.Property(e => e.DiscountId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.EventId).ValueGeneratedNever();

            entity.HasOne(d => d.EventCategory).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_EventCategories");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Organizers");
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.Property(e => e.EventCategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<GroupDiscount>(entity =>
        {
            entity.ToTable("GroupDiscounts", "evnt", tb => tb.HasComment("Discounts on groups"));

            entity.Property(e => e.GroupDiscountId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Loyalty>(entity =>
        {
            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Loyalty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loyalty_AspNetUsers");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.OrganizerId).HasName("PK_HostsOrganizers");

            entity.Property(e => e.OrganizerId).ValueGeneratedNever();

            entity.HasOne(d => d.OrganizerCategory).WithMany(p => p.Organizers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organizers_OrganizerCategories");
        });

        modelBuilder.Entity<OrganizerCategory>(entity =>
        {
            entity.HasKey(e => e.OrganizerCategoryId).HasName("PK_HostOrganizerCategories");

            entity.Property(e => e.OrganizerCategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.Property(e => e.PurchaseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.SaleId).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Sales).HasConstraintName("FK_Sales_AspNetUsers");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Sales).HasConstraintName("FK_Sales_Purchase_1");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.Property(e => e.SeatId).ValueGeneratedNever();
            entity.Property(e => e.VenueId).HasComment("Seats Information");

            entity.HasOne(d => d.Section).WithMany(p => p.Seats).HasConstraintName("FK_Seats_Section");

            entity.HasOne(d => d.Venue).WithMany(p => p.Seats).HasConstraintName("FK_Seats_Venue");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.Property(e => e.SectionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Tickets", "evnt", tb => tb.HasComment("Event Tickets"));

            entity.Property(e => e.TicketId).ValueGeneratedNever();

            entity.HasOne(d => d.Discount).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Discount_4");

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Events");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_AspNetUsers_3");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Purchase_6");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Seats_2");

            entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_TicketType_5");

            entity.HasOne(d => d.Venue).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Venue_1");

            entity.HasMany(d => d.Vips).WithMany(p => p.Tickets)
                .UsingEntity<Dictionary<string, object>>(
                    "JunctionTicketVip",
                    r => r.HasOne<Vip>().WithMany()
                        .HasForeignKey("VipId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Junction_Ticket_VIP_VIP"),
                    l => l.HasOne<Ticket>().WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Junction_Ticket_VIP_Tickets"),
                    j =>
                    {
                        j.HasKey("TicketId", "VipId");
                        j.ToTable("Junction_Ticket_VIP", "evnt");
                        j.IndexerProperty<int>("TicketId").HasColumnName("TicketID");
                        j.IndexerProperty<int>("VipId").HasColumnName("VIP_ID");
                    });
        });

        modelBuilder.Entity<TicketGroup>(entity =>
        {
            entity.Property(e => e.TicketGroupId).ValueGeneratedNever();

            entity.HasOne(d => d.GroupDiscount).WithMany(p => p.TicketGroups).HasConstraintName("FK_TicketGroup_GroupDiscounts_1");

            entity.HasOne(d => d.Purchase).WithMany(p => p.TicketGroups).HasConstraintName("FK_TicketGroup_Purchase");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.TicketTypeId).HasName("PK_UserFollows");

            entity.Property(e => e.TicketTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.Property(e => e.VenueId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Vip>(entity =>
        {
            entity.HasKey(e => e.VipId).HasName("PK_VIPAccess");

            entity.ToTable("VIP", "evnt", tb => tb.HasComment("VIP Area"));

            entity.Property(e => e.VipId).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithMany(p => p.Vips)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VIP_Events");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
