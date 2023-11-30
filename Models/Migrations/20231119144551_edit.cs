using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZHosptel.Models.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_DocterId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "DocterId",
                table: "Reservations",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DocterId",
                table: "Reservations",
                newName: "IX_Reservations_RoomId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reservations",
                newName: "DocterId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                newName: "IX_Reservations_DocterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_DocterId",
                table: "Reservations",
                column: "DocterId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
