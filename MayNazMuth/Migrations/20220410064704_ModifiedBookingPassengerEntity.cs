using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class ModifiedBookingPassengerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingPassengers",
                table: "BookingPassengers");

            migrationBuilder.AddColumn<int>(
                name: "BookingPassengerId",
                table: "BookingPassengers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingPassengers",
                table: "BookingPassengers",
                column: "BookingPassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPassengers_BookingId",
                table: "BookingPassengers",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingPassengers",
                table: "BookingPassengers");

            migrationBuilder.DropIndex(
                name: "IX_BookingPassengers_BookingId",
                table: "BookingPassengers");

            migrationBuilder.DropColumn(
                name: "BookingPassengerId",
                table: "BookingPassengers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingPassengers",
                table: "BookingPassengers",
                columns: new[] { "BookingId", "PassengerId" });
        }
    }
}
