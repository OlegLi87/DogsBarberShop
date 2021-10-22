using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogsBarberShop.Persistence.Migrations
{
    public partial class OrderDateTimeOffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "Orders",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ArrivalDate",
                table: "Orders",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Orders",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalDate",
                table: "Orders",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
