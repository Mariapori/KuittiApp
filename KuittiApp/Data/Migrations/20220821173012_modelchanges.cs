using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuittiApp.Data.Migrations
{
    public partial class modelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PVM",
                table: "Kuitit",
                newName: "OstoPVM");

            migrationBuilder.AddColumn<int>(
                name: "TakuuKK",
                table: "Kuitit",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakuuKK",
                table: "Kuitit");

            migrationBuilder.RenameColumn(
                name: "OstoPVM",
                table: "Kuitit",
                newName: "PVM");
        }
    }
}
