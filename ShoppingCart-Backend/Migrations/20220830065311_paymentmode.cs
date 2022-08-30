using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart_Backend.Migrations
{
    public partial class paymentmode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "paymentMode",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymentMode",
                table: "orders");
        }
    }
}
