using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventAddVenueFKColumn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                schema: "evnt",
                table: "Events",
                column: "VenueID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venue_VenueID",
                schema: "evnt",
                table: "Events",
                column: "VenueID",
                principalSchema: "evnt",
                principalTable: "Venue",
                principalColumn: "VenueID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venue_VenueID",
                schema: "evnt",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_VenueID",
                schema: "evnt",
                table: "Events");
        }
    }
}
