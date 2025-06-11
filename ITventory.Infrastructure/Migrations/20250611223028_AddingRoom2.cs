using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingRoom2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Area",
                table: "Rooms",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Area", "Capacity", "Floor", "OfficeId", "PersonResponsibleId" },
                values: new object[] { new Guid("8f5f3d88-cb83-4748-9d93-191b99b903cc"), 1149.0, 100, 3, new Guid("f1a2b3c4-0001-0000-0000-000000000001"), new Guid("7ebc5231-ae71-4c64-8154-ffe53c88cd0c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("8f5f3d88-cb83-4748-9d93-191b99b903cc"));

            migrationBuilder.AlterColumn<float>(
                name: "Area",
                table: "Rooms",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
