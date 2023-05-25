using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Repair1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HostsOrganizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HostOrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "evnt",
                table: "VIP",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VIP_ID",
                schema: "evnt",
                table: "VIP",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "Seat Capacity",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "evnt",
                table: "Venue",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                schema: "evnt",
                table: "Venue",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "evnt",
                table: "Venue",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "TicketTypeID",
                schema: "evnt",
                table: "TicketType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "evnt",
                table: "Tickets",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "evnt",
                table: "Tickets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TicketID",
                schema: "evnt",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TicketGroupID",
                schema: "evnt",
                table: "TicketGroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<string>(
                name: "SectionName",
                schema: "evnt",
                table: "Section",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriceMultiplier",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Seats Information",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Seats Information");

            migrationBuilder.AlterColumn<int>(
                name: "SeatNumber",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SeatID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                schema: "evnt",
                table: "Sales",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseID",
                schema: "evnt",
                table: "Purchase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerID",
                schema: "evnt",
                table: "Organizers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerCategoryID",
                schema: "evnt",
                table: "OrganizerCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "PriceMultiplier",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinimumChildren",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinimumAdults",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "evnt",
                table: "Events",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                schema: "evnt",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "EventCategoryID",
                schema: "evnt",
                table: "EventCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")

            migrationBuilder.AlterColumn<int>(
                name: "PriceMultiplier",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiscountName",
                schema: "evnt",
                table: "Discount",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizers",
                schema: "evnt",
                table: "Organizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizerCategories",
                schema: "evnt",
                table: "OrganizerCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "evnt",
                table: "VIP",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

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
                name: "Seat Capacity",
                schema: "evnt",
                table: "Venue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "evnt",
                table: "Venue",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                schema: "evnt",
                table: "Venue",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "evnt",
                table: "Venue",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

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

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "evnt",
                table: "Tickets",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "evnt",
                table: "Tickets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

            migrationBuilder.AlterColumn<string>(
                name: "SectionName",
                schema: "evnt",
                table: "Section",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "PriceMultiplier",
                schema: "evnt",
                table: "Section",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "VenueID",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: true,
                comment: "Seats Information",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Seats Information");

            migrationBuilder.AlterColumn<int>(
                name: "SeatNumber",
                schema: "evnt",
                table: "Seats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "PriceMultiplier",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MinimumChildren",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MinimumAdults",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                schema: "evnt",
                table: "GroupDiscounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "evnt",
                table: "Events",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

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
                name: "PriceMultiplier",
                schema: "evnt",
                table: "Discount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountName",
                schema: "evnt",
                table: "Discount",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

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
        }
    }
}
