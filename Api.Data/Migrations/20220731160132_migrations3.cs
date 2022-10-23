using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class migrations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdMaterialOrig",
                table: "ItemReservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMaterialOrig",
                table: "ItemReservations");
        }
    }
}
