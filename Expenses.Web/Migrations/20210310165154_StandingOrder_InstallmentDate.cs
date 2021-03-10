using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Web.Migrations
{
    public partial class StandingOrder_InstallmentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Installment",
                table: "standingorders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Installment",
                table: "standingorders");
        }
    }
}
