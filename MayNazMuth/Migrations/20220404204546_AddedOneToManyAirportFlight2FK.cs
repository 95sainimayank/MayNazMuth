using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class AddedOneToManyAirportFlight2FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationAirportId",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceAirportId",
                table: "Flights",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirportName = table.Column<string>(nullable: true),
                    AirportAddress = table.Column<string>(nullable: true),
                    AirportCity = table.Column<string>(nullable: true),
                    AirportCountry = table.Column<string>(nullable: true),
                    AirportAbbreviation = table.Column<string>(nullable: true),
                    AirportEmail = table.Column<string>(nullable: true),
                    AirportWebsite = table.Column<string>(nullable: true),
                    AirportPhoneno = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_SourceAirportId",
                table: "Flights",
                column: "SourceAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_SourceAirportId",
                table: "Flights",
                column: "SourceAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DestinationAirportId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_SourceAirportId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_SourceAirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DestinationAirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "SourceAirportId",
                table: "Flights");
        }
    }
}
