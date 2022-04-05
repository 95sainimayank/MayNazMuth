using Microsoft.EntityFrameworkCore.Migrations;

namespace MayNazMuth.Migrations
{
    public partial class paymentUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Payments");
        }
    }
}
