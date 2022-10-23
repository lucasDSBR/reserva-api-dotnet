using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PathPhoto = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Fee = table.Column<bool>(nullable: false),
                    ValueFee = table.Column<double>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    NameClient = table.Column<string>(nullable: true),
                    PhoneNumberClient = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TipePayment = table.Column<string>(nullable: true),
                    FlagCardPayment = table.Column<int>(nullable: true),
                    DeliveryMethod = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AddressNumber = table.Column<int>(nullable: false),
                    Neighborhood = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Instituicao = table.Column<string>(nullable: true),
                    TipoUsuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    createAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PathPhoto = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Fee = table.Column<bool>(nullable: false),
                    ValueFee = table.Column<double>(nullable: false),
                    AmountDemanded = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ReservationEntityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReservations_Reservations_ReservationEntityId",
                        column: x => x.ReservationEntityId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemReservations_ReservationEntityId",
                table: "ItemReservations",
                column: "ReservationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemReservations");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
