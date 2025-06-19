using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitFix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingSoftwareVersion_Product_ReviewedProductId",
                table: "RatingSoftwareVersion");

            migrationBuilder.DropIndex(
                name: "IX_RatingSoftwareVersion_ReviewedProductId",
                table: "RatingSoftwareVersion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RatingSoftwareVersion_ReviewedProductId",
                table: "RatingSoftwareVersion",
                column: "ReviewedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingSoftwareVersion_Product_ReviewedProductId",
                table: "RatingSoftwareVersion",
                column: "ReviewedProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
