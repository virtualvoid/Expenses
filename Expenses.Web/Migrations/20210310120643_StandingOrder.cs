using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Web.Migrations
{
    public partial class StandingOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "standingorders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Description = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_standingorders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_standingorders_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_standingorders_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_standingorders_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_standingorders_AccountId",
                table: "standingorders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_standingorders_CategoryId",
                table: "standingorders",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_standingorders_UserId",
                table: "standingorders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "standingorders");
        }
    }
}
