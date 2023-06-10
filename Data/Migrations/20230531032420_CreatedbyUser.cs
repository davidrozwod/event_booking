using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedbyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                schema: "evnt",
                table: "Events",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "evnt",
                table: "Events");
        }
    }
}
