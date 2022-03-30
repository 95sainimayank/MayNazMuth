using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class AddedOnetoManyTwoFKRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirportId",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AirportId1",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationAirportAirlineId",
                table: "Flights",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceAirportAirlineId",
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
                name: "IX_Flights_AirportId",
                table: "Flights",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirportId1",
                table: "Flights",
                column: "AirportId1");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportAirlineId",
                table: "Flights",
                column: "DestinationAirportAirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_SourceAirportAirlineId",
                table: "Flights",
                column: "SourceAirportAirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_AirportId",
                table: "Flights",
                column: "AirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_AirportId1",
                table: "Flights",
                column: "AirportId1",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airlines_DestinationAirportAirlineId",
                table: "Flights",
                column: "DestinationAirportAirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airlines_SourceAirportAirlineId",
                table: "Flights",
                column: "SourceAirportAirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_AirportId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_AirportId1",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airlines_DestinationAirportAirlineId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airlines_SourceAirportAirlineId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_Flights_AirportId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_AirportId1",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DestinationAirportAirlineId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_SourceAirportAirlineId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "AirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "AirportId1",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DestinationAirportAirlineId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "SourceAirportAirlineId",
                table: "Flights");
        }
    }
}
