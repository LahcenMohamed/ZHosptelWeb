using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZHosptel.Models.Migrations
{
    /// <inheritdoc />
    public partial class renamereser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfAppointment",
                table: "Reservations",
                newName: "TimeOfReservation");

            migrationBuilder.RenameColumn(
                name: "DateOfAppointment",
                table: "Reservations",
                newName: "DateOfReservation");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfReservation",
                table: "Reservations",
                newName: "TimeOfAppointment");

            migrationBuilder.RenameColumn(
                name: "DateOfReservation",
                table: "Reservations",
                newName: "DateOfAppointment");


        }
    }
}
