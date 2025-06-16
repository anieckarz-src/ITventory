using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixHistoryOfLogon4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HardwareId1",
                table: "Logon",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logon_HardwareId1",
                table: "Logon",
                column: "HardwareId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Logon_Hardware_HardwareId1",
                table: "Logon",
                column: "HardwareId1",
                principalTable: "Hardware",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logon_Hardware_HardwareId1",
                table: "Logon");

            migrationBuilder.DropIndex(
                name: "IX_Logon_HardwareId1",
                table: "Logon");

            migrationBuilder.DropColumn(
                name: "HardwareId1",
                table: "Logon");
        }
    }
}
