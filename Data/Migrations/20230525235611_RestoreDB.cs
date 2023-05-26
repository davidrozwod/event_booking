using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestoreDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "evnt");

            migrationBuilder.CreateTable(
                name: "Discount",
                schema: "evnt",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PriceMultiplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPricing", x => x.DiscountID);
                },
                comment: "Ticket Pricing Information");

            migrationBuilder.CreateTable(
                name: "EventCategories",
                schema: "evnt",
                columns: table => new
                {
                    EventCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.EventCategoryID);
                });

            /*migrationBuilder.CreateTable(
                name: "EventEventUser",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEventUser", x => new { x.EventId, x.EventUserId });
                });*/

            migrationBuilder.CreateTable(
                name: "EventUser",
                schema: "evnt",
                columns: table => new
                {
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Picture = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 450, nullable: true),
                    Document = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => x.EventUserID);
                    table.ForeignKey(
                        name: "FK_EventUser_AspNetUsers_EventUserID",
                        column: x => x.EventUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /*migrationBuilder.CreateTable(
                name: "EventUserOrganizer",
                columns: table => new
                {
                    EventUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUserOrganizer", x => new { x.EventUserId, x.OrganizerId });
                });

            migrationBuilder.CreateTable(
                name: "EventUserVenue",
                columns: table => new
                {
                    EventUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUserVenue", x => new { x.EventUserId, x.VenueId });
                });*/

            migrationBuilder.CreateTable(
                name: "GroupDiscounts",
                schema: "evnt",
                columns: table => new
                {
                    GroupDiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinimumAdults = table.Column<int>(type: "int", nullable: false),
                    MinimumChildren = table.Column<int>(type: "int", nullable: false),
                    PriceMultiplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDiscounts", x => x.GroupDiscountID);
                },
                comment: "Discounts on groups");

            migrationBuilder.CreateTable(
                name: "OrganizerCategories",
                schema: "evnt",
                columns: table => new
                {
                    OrganizerCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizerCategories", x => x.OrganizerCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                schema: "evnt",
                columns: table => new
                {
                    PurchaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseID);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                schema: "evnt",
                columns: table => new
                {
                    SectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PriceMultiplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionID);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                schema: "evnt",
                columns: table => new
                {
                    TicketTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PriceMultiplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollows", x => x.TicketTypeID);
                });

            /*migrationBuilder.CreateTable(
                name: "TicketVip",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    VipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketVip", x => new { x.TicketId, x.VipId });
                });*/

            migrationBuilder.CreateTable(
                name: "Venue",
                schema: "evnt",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    SeatCapacity = table.Column<int>(name: "Seat Capacity", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "Loyalty",
                schema: "evnt",
                columns: table => new
                {
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketCount = table.Column<int>(type: "int", nullable: true),
                    PriceMultiplier = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loyalty", x => x.EventUserID);
                    table.ForeignKey(
                        name: "FK_Loyalty_EventUser",
                        column: x => x.EventUserID,
                        principalSchema: "evnt",
                        principalTable: "EventUser",
                        principalColumn: "EventUserID");
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                schema: "evnt",
                columns: table => new
                {
                    OrganizerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerCategoryID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ContactInfo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.OrganizerID);
                    table.ForeignKey(
                        name: "FK_Organizers_OrganizerCategories",
                        column: x => x.OrganizerCategoryID,
                        principalSchema: "evnt",
                        principalTable: "OrganizerCategories",
                        principalColumn: "OrganizerCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                schema: "evnt",
                columns: table => new
                {
                    SaleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseID = table.Column<int>(type: "int", nullable: true),
                    SaleDate = table.Column<DateTime>(type: "date", nullable: true),
                    SalePrice = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sales_EventUser",
                        column: x => x.EventUserID,
                        principalSchema: "evnt",
                        principalTable: "EventUser",
                        principalColumn: "EventUserID");
                    table.ForeignKey(
                        name: "FK_Sales_Purchase_1",
                        column: x => x.PurchaseID,
                        principalSchema: "evnt",
                        principalTable: "Purchase",
                        principalColumn: "PurchaseID");
                });

            migrationBuilder.CreateTable(
                name: "TicketGroup",
                schema: "evnt",
                columns: table => new
                {
                    TicketGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseID = table.Column<int>(type: "int", nullable: true),
                    GroupDiscountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketGroup", x => x.TicketGroupID);
                    table.ForeignKey(
                        name: "FK_TicketGroup_GroupDiscounts_1",
                        column: x => x.GroupDiscountID,
                        principalSchema: "evnt",
                        principalTable: "GroupDiscounts",
                        principalColumn: "GroupDiscountID");
                    table.ForeignKey(
                        name: "FK_TicketGroup_Purchase",
                        column: x => x.PurchaseID,
                        principalSchema: "evnt",
                        principalTable: "Purchase",
                        principalColumn: "PurchaseID");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                schema: "evnt",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueID = table.Column<int>(type: "int", nullable: false, comment: "Seats Information"),
                    SectionID = table.Column<int>(type: "int", nullable: true),
                    SeatNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_Seats_Section",
                        column: x => x.SectionID,
                        principalSchema: "evnt",
                        principalTable: "Section",
                        principalColumn: "SectionID");
                    table.ForeignKey(
                        name: "FK_Seats_Venue",
                        column: x => x.VenueID,
                        principalSchema: "evnt",
                        principalTable: "Venue",
                        principalColumn: "VenueID");
                });

            migrationBuilder.CreateTable(
                name: "User_Venue_Follow",
                schema: "evnt",
                columns: table => new
                {
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Venue_Follow", x => new { x.EventUserID, x.VenueID });
                    table.ForeignKey(
                        name: "FK_User_Venue_Follow_EventUser",
                        column: x => x.EventUserID,
                        principalSchema: "evnt",
                        principalTable: "EventUser",
                        principalColumn: "EventUserID");
                    table.ForeignKey(
                        name: "FK_User_Venue_Follow_Venue_1",
                        column: x => x.VenueID,
                        principalSchema: "evnt",
                        principalTable: "Venue",
                        principalColumn: "VenueID");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "evnt",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerID = table.Column<int>(type: "int", nullable: false),
                    EventCategoryID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EarlyBirdCutoff = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_EventCategories",
                        column: x => x.EventCategoryID,
                        principalSchema: "evnt",
                        principalTable: "EventCategories",
                        principalColumn: "EventCategoryID");
                    table.ForeignKey(
                        name: "FK_Events_Organizers",
                        column: x => x.OrganizerID,
                        principalSchema: "evnt",
                        principalTable: "Organizers",
                        principalColumn: "OrganizerID");
                });

            migrationBuilder.CreateTable(
                name: "User_Organizer_Follow",
                schema: "evnt",
                columns: table => new
                {
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Organizer_Follow", x => new { x.EventUserID, x.OrganizerID });
                    table.ForeignKey(
                        name: "FK_User_Organizer_Follow_EventUser",
                        column: x => x.EventUserID,
                        principalSchema: "evnt",
                        principalTable: "EventUser",
                        principalColumn: "EventUserID");
                    table.ForeignKey(
                        name: "FK_User_Organizer_Follow_Organizers_1",
                        column: x => x.OrganizerID,
                        principalSchema: "evnt",
                        principalTable: "Organizers",
                        principalColumn: "OrganizerID");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "evnt",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DiscountID = table.Column<int>(type: "int", nullable: true),
                    TicketTypeID = table.Column<int>(type: "int", nullable: true),
                    PurchaseID = table.Column<int>(type: "int", nullable: true),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Tickets_Discount_4",
                        column: x => x.DiscountID,
                        principalSchema: "evnt",
                        principalTable: "Discount",
                        principalColumn: "DiscountID");
                    table.ForeignKey(
                        name: "FK_Tickets_EventUser_3",
                        column: x => x.EventUserID,
                        principalSchema: "evnt",
                        principalTable: "EventUser",
                        principalColumn: "EventUserID");
                    table.ForeignKey(
                        name: "FK_Tickets_Events",
                        column: x => x.EventID,
                        principalSchema: "evnt",
                        principalTable: "Events",
                        principalColumn: "EventID");
                    table.ForeignKey(
                        name: "FK_Tickets_Purchase_6",
                        column: x => x.PurchaseID,
                        principalSchema: "evnt",
                        principalTable: "Purchase",
                        principalColumn: "PurchaseID");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_2",
                        column: x => x.SeatID,
                        principalSchema: "evnt",
                        principalTable: "Seats",
                        principalColumn: "SeatID");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketType_5",
                        column: x => x.TicketTypeID,
                        principalSchema: "evnt",
                        principalTable: "TicketType",
                        principalColumn: "TicketTypeID");
                    table.ForeignKey(
                        name: "FK_Tickets_Venue_1",
                        column: x => x.VenueID,
                        principalSchema: "evnt",
                        principalTable: "Venue",
                        principalColumn: "VenueID");
                },
                comment: "Event Tickets");

            migrationBuilder.CreateTable(
                name: "User_Event_Follow",
                schema: "evnt",
                columns: table => new
                {
                    EventUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Event_Follow", x => new { x.EventUserID, x.EventID });
                    table.ForeignKey(
                        name: "FK_User_Event_Follow_EventUser",
                        column: x => x.EventUserID,
                        principalSchema: "evnt",
                        principalTable: "EventUser",
                        principalColumn: "EventUserID");
                    table.ForeignKey(
                        name: "FK_User_Event_Follow_Events_1",
                        column: x => x.EventID,
                        principalSchema: "evnt",
                        principalTable: "Events",
                        principalColumn: "EventID");
                });

            migrationBuilder.CreateTable(
                name: "VIP",
                schema: "evnt",
                columns: table => new
                {
                    VIP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    VIP_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    VIP_Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIPAccess", x => x.VIP_ID);
                    table.ForeignKey(
                        name: "FK_VIP_Events",
                        column: x => x.EventID,
                        principalSchema: "evnt",
                        principalTable: "Events",
                        principalColumn: "EventID");
                },
                comment: "VIP Area");

            migrationBuilder.CreateTable(
                name: "Junction_Ticket_VIP",
                schema: "evnt",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false),
                    VIP_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Junction_Ticket_VIP", x => new { x.TicketID, x.VIP_ID });
                    table.ForeignKey(
                        name: "FK_Junction_Ticket_VIP_Tickets",
                        column: x => x.TicketID,
                        principalSchema: "evnt",
                        principalTable: "Tickets",
                        principalColumn: "TicketID");
                    table.ForeignKey(
                        name: "FK_Junction_Ticket_VIP_VIP",
                        column: x => x.VIP_ID,
                        principalSchema: "evnt",
                        principalTable: "VIP",
                        principalColumn: "VIP_ID");
                });

            //Indexes
            migrationBuilder.CreateIndex(
                name: "IX_Events_EventCategoryID",
                schema: "evnt",
                table: "Events",
                column: "EventCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerID",
                schema: "evnt",
                table: "Events",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_Junction_Ticket_VIP_VIP_ID",
                schema: "evnt",
                table: "Junction_Ticket_VIP",
                column: "VIP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_OrganizerCategoryID",
                schema: "evnt",
                table: "Organizers",
                column: "OrganizerCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EventUserID",
                schema: "evnt",
                table: "Sales",
                column: "EventUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PurchaseID",
                schema: "evnt",
                table: "Sales",
                column: "PurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectionID",
                schema: "evnt",
                table: "Seats",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_VenueID",
                schema: "evnt",
                table: "Seats",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroup_GroupDiscountID",
                schema: "evnt",
                table: "TicketGroup",
                column: "GroupDiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroup_PurchaseID",
                schema: "evnt",
                table: "TicketGroup",
                column: "PurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DiscountID",
                schema: "evnt",
                table: "Tickets",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventUserID",
                schema: "evnt",
                table: "Tickets",
                column: "EventUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaseID",
                schema: "evnt",
                table: "Tickets",
                column: "PurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                schema: "evnt",
                table: "Tickets",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeID",
                schema: "evnt",
                table: "Tickets",
                column: "TicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VenueID",
                schema: "evnt",
                table: "Tickets",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "Ticket_Unique_Index",
                schema: "evnt",
                table: "Tickets",
                columns: new[] { "EventID", "SeatID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Event_Follow_EventID",
                schema: "evnt",
                table: "User_Event_Follow",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Organizer_Follow_OrganizerID",
                schema: "evnt",
                table: "User_Organizer_Follow",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Venue_Follow_VenueID",
                schema: "evnt",
                table: "User_Venue_Follow",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_VIP_EventID",
                schema: "evnt",
                table: "VIP",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "VIP_Unique_Index",
                schema: "evnt",
                table: "VIP",
                columns: new[] { "VIP_ID", "EventID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEventUser");

            migrationBuilder.DropTable(
                name: "EventUserOrganizer");

            migrationBuilder.DropTable(
                name: "EventUserVenue");

            migrationBuilder.DropTable(
                name: "Junction_Ticket_VIP",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Loyalty",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Sales",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "TicketGroup",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "TicketVip");

            migrationBuilder.DropTable(
                name: "User_Event_Follow",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "User_Organizer_Follow",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "User_Venue_Follow",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "VIP",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "GroupDiscounts",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Discount",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "EventUser",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Purchase",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Seats",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "TicketType",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Section",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Venue",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "EventCategories",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "Organizers",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "OrganizerCategories",
                schema: "evnt");
        }
    }
}
