using Microsoft.EntityFrameworkCore.Migrations;

namespace DogsBarberShop.Persistence.Migrations
{
    public partial class PetImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Pets",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Pets",
                newName: "ImageUrl");
        }
    }
}
