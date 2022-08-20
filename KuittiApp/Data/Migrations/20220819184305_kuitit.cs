using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuittiApp.Data.Migrations
{
    public partial class kuitit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kuitit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PVM = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Kuvaus = table.Column<string>(type: "TEXT", nullable: false),
                    Kuva = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Kayttaja = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuitit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kuitit");
        }
    }
}
