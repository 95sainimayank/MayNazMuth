using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class UpdateFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AirlineName",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationAirportName",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceAirportName",
                table: "Flights",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirlineName",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DestinationAirportName",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "SourceAirportName",
                table: "Flights");
        }
    }
}
