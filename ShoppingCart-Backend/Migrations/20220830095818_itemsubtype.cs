using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart_Backend.Migrations
{
    public partial class itemsubtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "itemsubType",
                table: "items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itemsubType",
                table: "items");
        }
    }
}
