using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Order",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
