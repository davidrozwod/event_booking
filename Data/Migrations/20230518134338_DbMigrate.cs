using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Document",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Picture",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserEvent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserOrganizer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserOrganizer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserVenue",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserVenue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    DiscountName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PriceMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPricing", x => x.DiscountId);
                },
                comment: "Ticket Pricing Information");

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    EventCategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.EventCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "GroupDiscounts",
                columns: table => new
                {
                    GroupDiscountId = table.Column<int>(type: "int", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MinimumAdults = table.Column<int>(type: "int", nullable: true),
                    MinimumChildren = table.Column<int>(type: "int", nullable: true),
                    PriceMultiplier = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDiscounts", x => x.GroupDiscountId);
                },
                comment: "Discounts on groups");

            migrationBuilder.CreateTable(
                name: "Loyalties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketCount = table.Column<int>(type: "int", nullable: false),
                    PriceMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loyalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loyalty_ApplicationUser",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizerCategories",
                columns: table => new
                {
                    OrganizerCategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostOrganizerCategories", x => x.OrganizerCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    SectionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PriceMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    TicketTypeId = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PriceMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollows", x => x.TicketTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SeatCapacity = table.Column<int>(name: "Seat Capacity", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ContactInfo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    OrganizerCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostsOrganizers", x => x.OrganizerId);
                    table.ForeignKey(
                        name: "FK_Organizers_OrganizerCategories",
                        column: x => x.OrganizerCategoryId,
                        principalTable: "OrganizerCategories",
                        principalColumn: "OrganizerCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "date", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Application",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Purchase_1",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketGroups",
                columns: table => new
                {
                    TicketGroupId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    GroupDiscountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketGroups", x => x.TicketGroupId);
                    table.ForeignKey(
                        name: "FK_TicketGroup_GroupDiscounts_1",
                        column: x => x.GroupDiscountId,
                        principalTable: "GroupDiscounts",
                        principalColumn: "GroupDiscountId");
                    table.ForeignKey(
                        name: "FK_TicketGroup_Purchase",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false, comment: "Seats Information"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Section",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Venue",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Venue_Follow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Venue_Follow", x => new { x.Id, x.VenueID });
                    table.ForeignKey(
                        name: "FK_User_Venue_Follow_ApplicationUser",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Venue_Follow_Venue_1",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueId");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventCategoryId = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    EarlyBirdCutoff = table.Column<DateTime>(type: "date", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_EventCategories",
                        column: x => x.EventCategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "EventCategoryId");
                    table.ForeignKey(
                        name: "FK_Events_Organizers",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "OrganizerId");
                });

            migrationBuilder.CreateTable(
                name: "User_Organizer_Follow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Organizer_Follow", x => new { x.Id, x.OrganizerID });
                    table.ForeignKey(
                        name: "FK_User_Organizer_Follow_ApplicationUser",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Organizer_Follow_Organizers_1",
                        column: x => x.OrganizerID,
                        principalTable: "Organizers",
                        principalColumn: "OrganizerId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    TicketTypeId = table.Column<int>(type: "int", nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_ApplicationUser_3",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Discount_4",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId");
                    table.ForeignKey(
                        name: "FK_Tickets_Events",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK_Tickets_Purchase_6",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_2",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketType_5",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "TicketTypeId");
                    table.ForeignKey(
                        name: "FK_Tickets_Venue_1",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId");
                },
                comment: "Event Tickets");

            migrationBuilder.CreateTable(
                name: "User_Event_Follow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Event_Follow", x => new { x.Id, x.EventID });
                    table.ForeignKey(
                        name: "FK_User_Event_Follow_ApplicationUser",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Event_Follow_Events_1",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventId");
                });

            migrationBuilder.CreateTable(
                name: "VIP",
                columns: table => new
                {
                    VipId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    VipName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    VipPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIPAccess", x => x.VipId);
                    table.ForeignKey(
                        name: "FK_VIP_Events",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                },
                comment: "VIP Area");

            migrationBuilder.CreateTable(
                name: "JunctionTicketVips",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    VipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JunctionTicketVips", x => new { x.TicketId, x.VipId });
                    table.ForeignKey(
                        name: "FK_JunctionTicketVip_Tickets",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JunctionTicketVip_Vips",
                        column: x => x.VipId,
                        principalTable: "VIP",
                        principalColumn: "VipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketVip",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    VipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketVip", x => new { x.TicketId, x.VipId });
                    table.ForeignKey(
                        name: "FK_TicketVip_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketVip_VIP_VipId",
                        column: x => x.VipId,
                        principalTable: "VIP",
                        principalColumn: "VipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventCategoryId",
                table: "Events",
                column: "EventCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_JunctionTicketVips_VipId",
                table: "JunctionTicketVips",
                column: "VipId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_OrganizerCategoryId",
                table: "Organizers",
                column: "OrganizerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PurchaseId",
                table: "Sales",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectionId",
                table: "Seats",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_VenueId",
                table: "Seats",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroups_GroupDiscountId",
                table: "TicketGroups",
                column: "GroupDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroups_PurchaseId",
                table: "TicketGroups",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DiscountId",
                table: "Tickets",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Id",
                table: "Tickets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaseId",
                table: "Tickets",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VenueId",
                table: "Tickets",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "Ticket_Unique_Index",
                table: "Tickets",
                columns: new[] { "EventId", "SeatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketVip_VipId",
                table: "TicketVip",
                column: "VipId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Event_Follow_EventID",
                table: "User_Event_Follow",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Organizer_Follow_OrganizerID",
                table: "User_Organizer_Follow",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Venue_Follow_VenueID",
                table: "User_Venue_Follow",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_VIP_EventId",
                table: "VIP",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "VIP_Unique_Index",
                table: "VIP",
                columns: new[] { "VipId", "EventId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserEvent");

            migrationBuilder.DropTable(
                name: "ApplicationUserOrganizer");

            migrationBuilder.DropTable(
                name: "ApplicationUserVenue");

            migrationBuilder.DropTable(
                name: "JunctionTicketVips");

            migrationBuilder.DropTable(
                name: "Loyalties");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "TicketGroups");

            migrationBuilder.DropTable(
                name: "TicketVip");

            migrationBuilder.DropTable(
                name: "User_Event_Follow");

            migrationBuilder.DropTable(
                name: "User_Organizer_Follow");

            migrationBuilder.DropTable(
                name: "User_Venue_Follow");

            migrationBuilder.DropTable(
                name: "GroupDiscounts");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "VIP");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "Organizers");

            migrationBuilder.DropTable(
                name: "OrganizerCategories");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AspNetUsers");
        }
    }
}
