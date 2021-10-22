using Microsoft.EntityFrameworkCore.Migrations;

namespace DogsBarberShop.Persistence.Migrations
{
    public partial class PetCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Pets_PetId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PetId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PetId_UserId",
                table: "Orders",
                columns: new[] { "PetId", "UserId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Pets_PetId_UserId",
                table: "Orders",
                columns: new[] { "PetId", "UserId" },
                principalTable: "Pets",
                principalColumns: new[] { "Id", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Pets_PetId_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PetId_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PetId",
                table: "Orders",
                column: "PetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Pets_PetId",
                table: "Orders",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
