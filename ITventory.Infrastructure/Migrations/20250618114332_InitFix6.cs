using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitFix6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicenseType",
                table: "SoftwareLicense",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LicenseType",
                table: "SoftwareLicense",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
