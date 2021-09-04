using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wonder.Infra.Migrations
{
    public partial class Company2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "PriceStock",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "PriceStock",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");
        }
    }
}
