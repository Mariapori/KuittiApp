using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuittiApp.Data.Migrations
{
    public partial class cntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Kuitit",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Kuitit");
        }
    }
}
