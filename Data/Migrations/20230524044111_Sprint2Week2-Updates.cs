using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Sprint2Week2Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //VIP
            migrationBuilder.DropPrimaryKey(
                name: "PK_VIP",
                schema: "evnt",
                table: "VIP");

            migrationBuilder.DropColumn(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP");

            migrationBuilder.AddColumn<int>(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VIP",
                schema: "evnt",
                table: "VIP",
                column: "VIP_ID");

            //Venue
            migrationBuilder.DropPrimaryKey(
                name: "PK_Venue",
                schema: "evnt",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "VenueID",
                schema: "evnt",
                table: "Venue");

            migrationBuilder.AddColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venue",
                schema: "evnt",
                table: "Venue",
                column: "VenueID");

            //TicketType
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketType",
                schema: "evnt",
                table: "TicketType");

            migrationBuilder.DropColumn(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType");

            migrationBuilder.AddColumn<int>(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketType",
                schema: "evnt",
                table: "TicketType",
                column: "TicketTypeID");

            //Tickets
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                schema: "evnt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                schema: "evnt",
                table: "Tickets",
                column: "TicketID");

            //TicketGroup
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketGroup",
                schema: "evnt",
                table: "TicketGroup");

            migrationBuilder.DropColumn(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup");

            migrationBuilder.AddColumn<int>(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketGroup",
                schema: "evnt",
                table: "TicketGroup",
                column: "TicketGroupID");

            //Section
            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                schema: "evnt",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "SectionID",
                schema: "evnt",
                table: "Section");

            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                schema: "evnt",
                table: "Section",
                column: "SectionID");

            //Seats
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                schema: "evnt",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatID",
                schema: "evnt",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "SeatID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                schema: "evnt",
                table: "Seats",
                column: "SeatID");

            //Sales
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                schema: "evnt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleID",
                schema: "evnt",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                schema: "evnt",
                table: "Sales",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                schema: "evnt",
                table: "Sales",
                column: "SaleID");

            //Purchase
            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                schema: "evnt",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                schema: "evnt",
                table: "Purchase",
                column: "PurchaseID");

            //Organizers
            migrationBuilder.DropPrimaryKey(
                name: "PK_HostsOrganizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers",
                column: "OrganizerID");

            /*migrationBuilder.DropPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.DropColumn(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers",
                column: "OrganizerID");*/

            //OrganizerCategories
            migrationBuilder.DropPrimaryKey(
                name: "PK_HostOrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories",
                column: "OrganizerCategoryID");
            /*migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.DropColumn(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories",
                column: "OrganizerCategoryID");*/

            //GroupDiscounts
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupDiscounts",
                schema: "evnt",
                table: "GroupDiscounts");

            migrationBuilder.DropColumn(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts");

            migrationBuilder.AddColumn<int>(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupDiscounts",
                schema: "evnt",
                table: "GroupDiscounts",
                column: "GroupDiscountID");

            //??No change needed??//
            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            //??No change needed??// END

            //Events
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                schema: "evnt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventID",
                schema: "evnt",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                schema: "evnt",
                table: "Events",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                schema: "evnt",
                table: "Events",
                column: "EventID");

            //EventCategories
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCategories",
                schema: "evnt",
                table: "EventCategories");

            migrationBuilder.DropColumn(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories");

            migrationBuilder.AddColumn<int>(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCategories",
                schema: "evnt",
                table: "EventCategories",
                column: "EventCategoryID");

            //Discount
            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                schema: "evnt",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount");

            migrationBuilder.AddColumn<int>(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                schema: "evnt",
                table: "Discount",
                column: "DiscountID");

            //------------------------------------------
            /*migrationBuilder.DropPrimaryKey(
                name: "PK_HostsOrganizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HostOrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers",
                column: "OrganizerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories",
                column: "OrganizerCategoryID");*/

            /*
            migrationBuilder.DropPrimaryKey(
                name: "PK_HostsOrganizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HostOrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");
            //------------------------------
            //
            migrationBuilder.AlterColumn<int>(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            //
            migrationBuilder.AlterColumn<int>(
                name: "SeatID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                schema: "evnt",
                table: "Sales",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            ///
            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            ///
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            ///
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            ///
            migrationBuilder.AlterColumn<string>(
                name: "Document",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            //
            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                schema: "evnt",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            //
            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers",
                column: "OrganizerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories",
                column: "OrganizerCategoryID");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //VIP
            migrationBuilder.DropPrimaryKey(
                name: "PK_VIP",
                schema: "evnt",
                table: "VIP");

            migrationBuilder.DropColumn(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP");

            migrationBuilder.AddColumn<int>(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VIP",
                schema: "evnt",
                table: "VIP",
                column: "VIP_ID");

            //Venue
            migrationBuilder.DropPrimaryKey(
                name: "PK_Venue",
                schema: "evnt",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "VenueID",
                schema: "evnt",
                table: "Venue");

            migrationBuilder.AddColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venue",
                schema: "evnt",
                table: "Venue",
                column: "VenueID");

            //TicketType
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketType",
                schema: "evnt",
                table: "TicketType");

            migrationBuilder.DropColumn(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType");

            migrationBuilder.AddColumn<int>(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketType",
                schema: "evnt",
                table: "TicketType",
                column: "TicketTypeID");

            //Tickets
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                schema: "evnt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                schema: "evnt",
                table: "Tickets",
                column: "TicketID");

            //TicketGroup
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketGroup",
                schema: "evnt",
                table: "TicketGroup");

            migrationBuilder.DropColumn(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup");

            migrationBuilder.AddColumn<int>(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketGroup",
                schema: "evnt",
                table: "TicketGroup",
                column: "TicketGroupID");

            //Section
            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                schema: "evnt",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "SectionID",
                schema: "evnt",
                table: "Section");

            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                schema: "evnt",
                table: "Section",
                column: "SectionID");

            //Seats
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                schema: "evnt",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatID",
                schema: "evnt",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "SeatID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                schema: "evnt",
                table: "Seats",
                column: "SeatID");

            //Sales
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                schema: "evnt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleID",
                schema: "evnt",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                schema: "evnt",
                table: "Sales",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                schema: "evnt",
                table: "Sales",
                column: "SaleID");

            //Purchase
            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                schema: "evnt",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                schema: "evnt",
                table: "Purchase",
                column: "PurchaseID");

            //Organizers
            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HostsOrganizers",
                schema: "evnt",
                table: "Organizers",
                columns: new[] { "HostID", "OrganizerID" });

            //OrganizerCategories
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HostOrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories",
                columns: new[] { "HostID", "OrganizerCategoryID" });

            //GroupDiscounts
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupDiscounts",
                schema: "evnt",
                table: "GroupDiscounts");

            migrationBuilder.DropColumn(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts");

            migrationBuilder.AddColumn<int>(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupDiscounts",
                schema: "evnt",
                table: "GroupDiscounts",
                column: "GroupDiscountID");

            //??No change needed??//
            migrationBuilder.AlterColumn<int>(
                name: "Picture",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldUnicode: false,
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastName",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "FirstName",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Document",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldUnicode: false,
                oldMaxLength: 450,
                oldNullable: true);
            //??No change needed??// END

            //Events
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                schema: "evnt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventID",
                schema: "evnt",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                schema: "evnt",
                table: "Events",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                schema: "evnt",
                table: "Events",
                column: "EventID");

            //EventCategories
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCategories",
                schema: "evnt",
                table: "EventCategories");

            migrationBuilder.DropColumn(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories");

            migrationBuilder.AddColumn<int>(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCategories",
                schema: "evnt",
                table: "EventCategories",
                column: "EventCategoryID");

            //Discount
            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                schema: "evnt",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount");

            migrationBuilder.AddColumn<int>(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                schema: "evnt",
                table: "Discount",
                column: "DiscountID");
        }
        /*
        migrationBuilder.DropPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AlterColumn<int>(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SeatID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                schema: "evnt",
                table: "Sales",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Picture",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldUnicode: false,
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastName",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "FirstName",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Document",
                schema: "evnt",
                table: "EventUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldUnicode: false,
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                schema: "evnt",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HostsOrganizers",
                schema: "evnt",
                table: "Organizers",
                column: "OrganizerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HostOrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories",
                column: "OrganizerCategoryID");
        }*/
    }
}
