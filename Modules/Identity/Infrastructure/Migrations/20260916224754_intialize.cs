using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniECommerce.Modules.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCredentials_Users_UserId1",
                table: "UserCredentials");

            migrationBuilder.DropIndex(
                name: "IX_UserCredentials_UserId1",
                table: "UserCredentials");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserCredentials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserCredentials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCredentials_UserId1",
                table: "UserCredentials",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCredentials_Users_UserId1",
                table: "UserCredentials",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
