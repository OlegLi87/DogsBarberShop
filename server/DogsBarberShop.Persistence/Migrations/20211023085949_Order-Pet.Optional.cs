using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogsBarberShop.Persistence.Migrations
{
    public partial class OrderPetOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PetId_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "PetId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PetId_UserId",
                table: "Orders",
                columns: new[] { "PetId", "UserId" },
                unique: true,
                filter: "[PetId] IS NOT NULL AND [UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PetId_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "PetId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PetId_UserId",
                table: "Orders",
                columns: new[] { "PetId", "UserId" },
                unique: true);
        }
    }
}
