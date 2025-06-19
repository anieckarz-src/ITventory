using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Software_Department_DepartmentId",
                table: "Software");

            migrationBuilder.DropIndex(
                name: "IX_Software_DepartmentId",
                table: "Software");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Software");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Software",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Software_DepartmentId",
                table: "Software",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Department_DepartmentId",
                table: "Software",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
