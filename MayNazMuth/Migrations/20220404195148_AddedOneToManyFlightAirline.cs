using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class AddedOneToManyFlightAirline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirlineId",
                table: "Flights",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airlines_AirlineId",
                table: "Flights",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airlines_AirlineId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "AirlineId",
                table: "Flights");
        }
    }
}
