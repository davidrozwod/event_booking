using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventAddVenueFKColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VenueID",
                schema: "evnt",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenueID",
                schema: "evnt",
                table: "Events");
        }
    }
}
