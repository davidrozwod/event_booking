using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventUserModelAddColumn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentFileName",
                schema: "evnt",
                table: "EventUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentFileName",
                schema: "evnt",
                table: "EventUser");
        }
    }
}
