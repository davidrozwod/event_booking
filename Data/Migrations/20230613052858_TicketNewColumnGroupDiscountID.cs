using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class TicketNewColumnGroupDiscountID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_GroupDiscountID",
                schema: "evnt",
                table: "Tickets",
                column: "GroupDiscountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_GroupDiscounts_GroupDiscountID",
                schema: "evnt",
                table: "Tickets",
                column: "GroupDiscountID",
                principalSchema: "evnt",
                principalTable: "GroupDiscounts",
                principalColumn: "GroupDiscountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_GroupDiscounts_GroupDiscountID",
                schema: "evnt",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_GroupDiscountID",
                schema: "evnt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "GroupDiscountID",
                schema: "evnt",
                table: "Tickets");
        }
    }
}
