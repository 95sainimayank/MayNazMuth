using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class AddedBookingPassengerDbSetToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPassenger_Bookings_BookingId",
                table: "BookingPassenger");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPassenger_Passengers_PassengerId",
                table: "BookingPassenger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingPassenger",
                table: "BookingPassenger");

            migrationBuilder.RenameTable(
                name: "BookingPassenger",
                newName: "BookingPassengers");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPassenger_PassengerId",
                table: "BookingPassengers",
                newName: "IX_BookingPassengers_PassengerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingPassengers",
                table: "BookingPassengers",
                columns: new[] { "BookingId", "PassengerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPassengers_Bookings_BookingId",
                table: "BookingPassengers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPassengers_Passengers_PassengerId",
                table: "BookingPassengers",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "PassengerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPassengers_Bookings_BookingId",
                table: "BookingPassengers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPassengers_Passengers_PassengerId",
                table: "BookingPassengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingPassengers",
                table: "BookingPassengers");

            migrationBuilder.RenameTable(
                name: "BookingPassengers",
                newName: "BookingPassenger");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPassengers_PassengerId",
                table: "BookingPassenger",
                newName: "IX_BookingPassenger_PassengerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingPassenger",
                table: "BookingPassenger",
                columns: new[] { "BookingId", "PassengerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPassenger_Bookings_BookingId",
                table: "BookingPassenger",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPassenger_Passengers_PassengerId",
                table: "BookingPassenger",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "PassengerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
