//
//For tables that are not included in the models see below within
//"protected override void OnModelCreating(ModelBuilder modelBuilder)"
//
//Tables here that are not included in models are
//Junction table = User > Events [User_Event_Follow]
//Junction table = User > Organizers [User_Organizer_Follow]
//Junction table = User > Venues [User_Venue_Follow]
//Junction table = Ticket > VIP [Junction_Ticket_VIP]

using event_booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
     
        public virtual DbSet<Discount> Discounts { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventCategory> EventCategories { get; set; }

        public virtual DbSet<EventUser> EventUsers { get; set; }

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
            => optionsBuilder.UseSqlServer("Server=tcp:eventdatabase.database.windows.net,1433;Initial Catalog=eventdata;Persist Security Info=False;User ID=devninjas;Password=Scum777*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This ensures the base identity mappings are added

            //Discount table
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId).HasName("PK_TicketPricing");

                entity.ToTable("Discount", "evnt", tb => tb.HasComment("Ticket Pricing Information"));

                entity.Property(e => e.DiscountId).ValueGeneratedNever();
            });

            //Event table
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

            //EventCategory table
            modelBuilder.Entity<EventCategory>(entity =>
            {
                entity.Property(e => e.EventCategoryId).ValueGeneratedNever();
            });

            //
            //EventUser table
            //
            modelBuilder.Entity<EventUser>(entity =>
            {
                modelBuilder.Entity<EventUser>()    //User primary key > EventUser
                    .HasOne(eu => eu.IdentityUser) //IdentityUser (UserID)
                    .WithOne()
                    .HasForeignKey<EventUser>(eu => eu.EventUserId);

            //Junction table = User > Events
                entity.HasMany(d => d.Events).WithMany(p => p.EventUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserEventFollow",
                        r => r.HasOne<Event>().WithMany()
                            .HasForeignKey("EventId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Event_Follow_Events_1"),
                        l => l.HasOne<EventUser>().WithMany()
                            .HasForeignKey("EventUserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Event_Follow_EventUser"),
                        j =>
                        {
                            j.HasKey("EventUserId", "EventId");
                            j.ToTable("User_Event_Follow", "evnt");
                            j.IndexerProperty<string>("EventUserId").HasColumnName("EventUserID");
                            j.IndexerProperty<int>("EventId").HasColumnName("EventID");
                        });

            //Junction table = User > Organizers
                entity.HasMany(d => d.Organizers).WithMany(p => p.EventUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserOrganizerFollow",
                        r => r.HasOne<Organizer>().WithMany()
                            .HasForeignKey("OrganizerId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Organizer_Follow_Organizers_1"),
                        l => l.HasOne<EventUser>().WithMany()
                            .HasForeignKey("EventUserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Organizer_Follow_EventUser"),
                        j =>
                        {
                            j.HasKey("EventUserId", "OrganizerId");
                            j.ToTable("User_Organizer_Follow", "evnt");
                            j.IndexerProperty<string>("EventUserId").HasColumnName("EventUserID");
                            j.IndexerProperty<int>("OrganizerId").HasColumnName("OrganizerID");
                        });

            //Junction table = User > Venues
                entity.HasMany(d => d.Venues).WithMany(p => p.EventUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserVenueFollow",
                        r => r.HasOne<Venue>().WithMany()
                            .HasForeignKey("VenueId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Venue_Follow_Venue_1"),
                        l => l.HasOne<EventUser>().WithMany()
                            .HasForeignKey("EventUserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Venue_Follow_EventUser"),
                        j =>
                        {
                            j.HasKey("EventUserId", "VenueId");
                            j.ToTable("User_Venue_Follow", "evnt");
                            j.IndexerProperty<string>("EventUserId").HasColumnName("EventUserID");
                            j.IndexerProperty<int>("VenueId").HasColumnName("VenueID");
                        });
            });

            //GroupDiscount table
            modelBuilder.Entity<GroupDiscount>(entity =>
            {
                entity.ToTable("GroupDiscounts", "evnt", tb => tb.HasComment("Discounts on groups"));

                entity.Property(e => e.GroupDiscountId).ValueGeneratedNever();
            });

            //Loyalty table
            modelBuilder.Entity<Loyalty>(entity =>
            {
                entity.HasOne(d => d.EventUser).WithOne(p => p.Loyalty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loyalty_EventUser");
            });

            //Organizer table
            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.HasKey(e => e.OrganizerId).HasName("PK_HostsOrganizers");

                entity.Property(e => e.OrganizerId).ValueGeneratedNever();

                entity.HasOne(d => d.OrganizerCategory).WithMany(p => p.Organizers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organizers_OrganizerCategories");
            });

            //OrganizerCategory table
            modelBuilder.Entity<OrganizerCategory>(entity =>
            {
                entity.HasKey(e => e.OrganizerCategoryId).HasName("PK_HostOrganizerCategories");

                entity.Property(e => e.OrganizerCategoryId).ValueGeneratedNever();
            });

            //Purchase table
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.PurchaseId).ValueGeneratedNever();
            });

            //Sale table
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.SaleId).ValueGeneratedNever();

                entity.HasOne(d => d.EventUser).WithMany(p => p.Sales).HasConstraintName("FK_Sales_EventUser");

                entity.HasOne(d => d.Purchase).WithMany(p => p.Sales).HasConstraintName("FK_Sales_Purchase_1");
            });

            //Seat table
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.SeatId).ValueGeneratedNever();
                entity.Property(e => e.VenueId).HasComment("Seats Information");

                entity.HasOne(d => d.Section).WithMany(p => p.Seats).HasConstraintName("FK_Seats_Section");

                entity.HasOne(d => d.Venue).WithMany(p => p.Seats).HasConstraintName("FK_Seats_Venue");
            });

            //Section table
            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionId).ValueGeneratedNever();
            });

            //Ticket table
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Tickets", "evnt", tb => tb.HasComment("Event Tickets"));

                entity.Property(e => e.TicketId).ValueGeneratedNever();

                entity.HasOne(d => d.Discount).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Discount_4");

                entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Events");

                entity.HasOne(d => d.EventUser).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_EventUser_3");

                entity.HasOne(d => d.Purchase).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Purchase_6");

                entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Seats_2");

                entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_TicketType_5");

                entity.HasOne(d => d.Venue).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Venue_1");

            //Junction table = Ticket > VIP
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

            //TicketGroup table
            modelBuilder.Entity<TicketGroup>(entity =>
            {
                entity.Property(e => e.TicketGroupId).ValueGeneratedNever();

                entity.HasOne(d => d.GroupDiscount).WithMany(p => p.TicketGroups).HasConstraintName("FK_TicketGroup_GroupDiscounts_1");

                entity.HasOne(d => d.Purchase).WithMany(p => p.TicketGroups).HasConstraintName("FK_TicketGroup_Purchase");
            });

            //TicketType table
            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.HasKey(e => e.TicketTypeId).HasName("PK_UserFollows");

                entity.Property(e => e.TicketTypeId).ValueGeneratedNever();
            });

            //Venue table
            modelBuilder.Entity<Venue>(entity =>
            {
                entity.Property(e => e.VenueId).ValueGeneratedNever();
            });

            //Vip table
            modelBuilder.Entity<Vip>(entity =>
            {
                entity.HasKey(e => e.VipId).HasName("PK_VIPAccess");

                entity.ToTable("VIP", "evnt", tb => tb.HasComment("VIP Area"));

                entity.Property(e => e.VipId).ValueGeneratedNever();

                entity.HasOne(d => d.Event).WithMany(p => p.Vips)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VIP_Events");
            });

        }
    }
}