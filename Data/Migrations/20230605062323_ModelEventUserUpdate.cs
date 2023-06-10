using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelEventUserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DocumentTmp",
                schema: "evnt",
                table: "EventUser",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.Sql("Update evnt.EventUser SET DocumentTmp = Convert(varbinary, Document)");

            migrationBuilder.DropColumn(
                name: "Document",
                schema: "evnt",
                table: "EventUser");

            migrationBuilder.RenameColumn(
                name: "DocumentTmp",
                schema: "evnt",
                table: "EventUser",
                newName: "Document");

            /*migrationBuilder.AlterColumn<byte[]>(
                name: "Document",
                schema: "evnt",
                table: "EventUser",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldUnicode: false,
                oldMaxLength: 450,
                oldNullable: true);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentTmp",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true);

            migrationBuilder.Sql("Update evnt.EventUser SET DocumentTmp = Convert(varchar(450), Document)");

            migrationBuilder.DropColumn(
                name: "Document",
                schema: "evnt",
                table: "EventUser");

            migrationBuilder.RenameColumn(
                name: "DocumentTmp",
                schema: "evnt",
                table: "EventUser",
                newName: "Document");

            /*migrationBuilder.AlterColumn<string>(
                name: "Document",
                schema: "evnt",
                table: "EventUser",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);*/
        }
    }
}
