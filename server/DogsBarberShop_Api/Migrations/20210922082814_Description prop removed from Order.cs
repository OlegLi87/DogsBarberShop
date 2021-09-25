using Microsoft.EntityFrameworkCore.Migrations;

namespace DogsBarberShop_Api.Migrations
{
    public partial class DescriptionpropremovedfromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "text",
                nullable: true);
        }
    }
}
