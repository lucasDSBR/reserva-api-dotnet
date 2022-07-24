using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _20221003213200_CarMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.CreateTable(
                        name: "Car",
                        columns: table => new
                        {
                            Id = table.Column<Guid>(nullable: false),
                            CarModel = table.Column<string>(nullable: true),
                            TotalPeople = table.Column<int>(nullable: true),
                            IdSupplier = table.Column<int>(nullable: true),
                            CarYear = table.Column<string>(nullable: true),
                            City = table.Column<string>(nullable: true),
                            State = table.Column<string>(nullable: true),
                            Photos = table.Column<string>(nullable: true),
                            AvailableQuantity = table.Column<string>(nullable: true)
                        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                            name: "Car");
        }
    }
}
