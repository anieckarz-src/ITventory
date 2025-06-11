using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Regulations = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ProductType = table.Column<string>(type: "text", nullable: false),
                    MaxSKU = table.Column<int>(type: "integer", nullable: false),
                    NominalWorth = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    TypeOfPlant = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producent_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BuildingNumber = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Lattitude_Value = table.Column<double>(type: "double precision", nullable: false),
                    Longitude_Value = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Office_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProducentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Model_Producent_ProducentId",
                        column: x => x.ProducentId,
                        principalTable: "Producent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PublisherId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovalType = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Software_Producent_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Producent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionNumber = table.Column<string>(type: "text", nullable: false),
                    SoftwareId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Published = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LicenseType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwareVersions_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareLicense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicenseType = table.Column<int>(type: "integer", nullable: false),
                    LicenseKey = table.Column<string>(type: "text", nullable: false),
                    ValidUntil = table.Column<DateOnly>(type: "date", nullable: false),
                    MaxUse = table.Column<int>(type: "integer", nullable: false),
                    SoftwareVersion = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwareLicense_SoftwareVersions_SoftwareVersion",
                        column: x => x.SoftwareVersion,
                        principalTable: "SoftwareVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Area = table.Column<string>(type: "text", nullable: true),
                    PositionName = table.Column<string>(type: "text", nullable: true),
                    Seniority = table.Column<string>(type: "text", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Employee_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLicense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLicense_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLicense_SoftwareLicense_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "SoftwareLicense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingSoftwareVersion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SoftwareVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewedProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RatingMark = table.Column<int>(type: "integer", nullable: false),
                    RatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    RateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingSoftwareVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingSoftwareVersion_Employee_RatedById",
                        column: x => x.RatedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingSoftwareVersion_Product_ReviewedProductId",
                        column: x => x.ReviewedProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingSoftwareVersion_SoftwareVersions_SoftwareVersionId",
                        column: x => x.SoftwareVersionId,
                        principalTable: "SoftwareVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    Area = table.Column<float>(type: "real", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    PersonResponsibleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Employee_PersonResponsibleId",
                        column: x => x.PersonResponsibleId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Worth = table.Column<double>(type: "double precision", nullable: false),
                    ProducentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelYear = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    PurchasedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_Producent_ProducentId",
                        column: x => x.ProducentId,
                        principalTable: "Producent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hardware",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrimaryUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultDomain = table.Column<string>(type: "text", nullable: false),
                    HardwareType = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Worth = table.Column<double>(type: "double precision", nullable: false),
                    ProducentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelYear = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    PurchasedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardware", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hardware_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hardware_Employee_PrimaryUserId",
                        column: x => x.PrimaryUserId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hardware_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hardware_Producent_ProducentId",
                        column: x => x.ProducentId,
                        principalTable: "Producent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hardware_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    SKU = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryProduct_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviwedEquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true),
                    ReviewDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Employee_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_Equipment_ReviwedEquipmentId",
                        column: x => x.ReviwedEquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HardwareLicense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    HardwareId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareLicense_Hardware_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "Hardware",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HardwareLicense_SoftwareLicense_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "SoftwareLicense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HardwareId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Domain = table.Column<string>(type: "text", nullable: false),
                    LogonTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logon_Employee_UserId",
                        column: x => x.UserId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logon_Hardware_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "Hardware",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryCode", "Name", "Region", "Regulations" },
                values: new object[,]
                {
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0101"), "US", "United States", "NorthAmerica", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0102"), "CN", "China", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0103"), "JP", "Japan", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0104"), "DE", "Germany", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0105"), "KR", "South Korea", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0106"), "IN", "India", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0107"), "TW", "Taiwan", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0108"), "IL", "Israel", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0109"), "GB", "United Kingdom", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0110"), "FR", "France", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0111"), "CA", "Canada", "NorthAmerica", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0112"), "SG", "Singapore", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0113"), "NL", "Netherlands", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0114"), "SE", "Sweden", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0115"), "FI", "Finland", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0116"), "AU", "Australia", "Australia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0117"), "IE", "Ireland", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0118"), "CH", "Switzerland", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0119"), "BE", "Belgium", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), "PL", "Poland", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0121"), "IT", "Italy", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0122"), "ES", "Spain", "Europe", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0123"), "AE", "UAE", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0124"), "MY", "Malaysia", "Asia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0125"), "NZ", "New Zealand", "Australia", null },
                    { new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0126"), "NO", "Norway", "Europe", null }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "CountryId", "Latitude", "Longitude", "Name", "TypeOfPlant", "ZipCode" },
                values: new object[] { new Guid("1a2b3c4d-0001-0000-0000-000000000001"), "Warsaw", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), 52.229700000000001, 21.0122, "Warsaw Factory", "Factory", "00-001" });

            migrationBuilder.InsertData(
                table: "Producent",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { new Guid("04dabce6-41f7-4a66-8c34-963cedd62a7f"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0103"), "Toshiba" },
                    { new Guid("58ea87f5-103a-4567-8fda-9b2a32be2ff9"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0103"), "Yamaha" },
                    { new Guid("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f6"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0102"), "Huawei" },
                    { new Guid("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f7"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0115"), "Nokia" },
                    { new Guid("890a1b2c-d4e5-f678-90a1-b2c3d4e5f678"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0102"), "Lenovo" },
                    { new Guid("890a1b2c-d4e5-f678-90a1-b2c3d4e5f679"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0114"), "Ericsson" },
                    { new Guid("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f678"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0104"), "Siemens" },
                    { new Guid("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f680"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0104"), "SAP" },
                    { new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0101"), "Apple" },
                    { new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67891"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0104"), "Bosch" },
                    { new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67892"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0113"), "Philips" },
                    { new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0101"), "Microsoft" },
                    { new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a2"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0106"), "Infosys" },
                    { new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a3"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), "Comarch" },
                    { new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0101"), "Intel" },
                    { new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b3"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0107"), "ASUS" },
                    { new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b4"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0110"), "Dassault Systèmes" },
                    { new Guid("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0101"), "Google" },
                    { new Guid("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c4"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0107"), "Acer" },
                    { new Guid("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0105"), "Samsung" },
                    { new Guid("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d5"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0109"), "Spotify" },
                    { new Guid("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e5"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0105"), "LG Electronics" },
                    { new Guid("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e6"), new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0109"), "ARM Holdings" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "Comments", "Name", "ProducentId", "ReleaseDate" },
                values: new object[,]
                {
                    { new Guid("a1122334-5566-7788-99aa-bbccddeeff00"), null, "Xperia Z5", new Guid("04dabce6-41f7-4a66-8c34-963cedd62a7f"), new DateOnly(2015, 10, 1) },
                    { new Guid("a2233445-6677-8899-aabb-ccddeeff0011"), null, "iPhone 12", new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new DateOnly(2020, 10, 23) },
                    { new Guid("a3344556-7788-99aa-bbcc-ddeeff001122"), null, "Surface Pro 7", new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), new DateOnly(2019, 10, 22) },
                    { new Guid("a4455667-8899-aabb-ccdd-eeff00112233"), null, "Core i9-11900K", new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), new DateOnly(2021, 3, 16) },
                    { new Guid("a5566778-99aa-bbcc-ddee-ff0011223344"), null, "Pixel 5", new Guid("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), new DateOnly(2020, 10, 15) },
                    { new Guid("a6677889-aabb-ccdd-eeff-001122334455"), null, "Galaxy S21", new Guid("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), new DateOnly(2021, 1, 29) },
                    { new Guid("a778899a-bbcc-ddee-ff00-112233445566"), null, "LG Velvet", new Guid("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e5"), new DateOnly(2020, 5, 15) },
                    { new Guid("a8899aab-ccdd-eeff-0011-223344556677"), null, "Mate 40 Pro", new Guid("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f6"), new DateOnly(2020, 10, 22) },
                    { new Guid("a99aabbc-ddee-ff00-1122-334455667788"), null, "ThinkPad X1 Carbon Gen 9", new Guid("890a1b2c-d4e5-f678-90a1-b2c3d4e5f678"), new DateOnly(2021, 1, 12) },
                    { new Guid("aaabbcdd-eeff-0011-2233-445566778899"), null, "XPS 13", new Guid("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f678"), new DateOnly(2020, 9, 30) },
                    { new Guid("bbccddee-ff00-1122-3344-5566778899aa"), null, "SIMATIC S7-1500", new Guid("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f678"), new DateOnly(2015, 7, 15) },
                    { new Guid("bfeee839-38a5-4aa7-89dd-6296f39b1694"), null, "ZH-500", new Guid("58ea87f5-103a-4567-8fda-9b2a32be2ff9"), new DateOnly(2016, 5, 2) },
                    { new Guid("ccddeeff-0011-2233-4455-66778899aabb"), null, "PS5", new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new DateOnly(2020, 11, 12) },
                    { new Guid("ddeeff00-1122-3344-5566-778899aabbcc"), null, "Nokia 8.3 5G", new Guid("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f7"), new DateOnly(2020, 9, 15) },
                    { new Guid("eeff0011-2233-4455-6677-8899aabbccdd"), null, "AirPods Pro", new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new DateOnly(2019, 10, 30) },
                    { new Guid("ff001122-3344-5566-7788-99aabbccddee"), null, "Razer Blade 15", new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new DateOnly(2021, 3, 1) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerId",
                table: "Department",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ManagerId",
                table: "Employee",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RoomId",
                table: "Employee",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLicense_EmployeeId",
                table: "EmployeeLicense",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLicense_LicenseId",
                table: "EmployeeLicense",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_DepartmentId",
                table: "Equipment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ModelId",
                table: "Equipment",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ProducentId",
                table: "Equipment",
                column: "ProducentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RoomId",
                table: "Equipment",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_DepartmentId",
                table: "Hardware",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_ModelId",
                table: "Hardware",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_PrimaryUserId",
                table: "Hardware",
                column: "PrimaryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_ProducentId",
                table: "Hardware",
                column: "ProducentId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_RoomId",
                table: "Hardware",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareLicense_HardwareId",
                table: "HardwareLicense",
                column: "HardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareLicense_LicenseId",
                table: "HardwareLicense",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_ProductId",
                table: "InventoryProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_RoomId",
                table: "InventoryProduct",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CountryId",
                table: "Location",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Logon_HardwareId",
                table: "Logon",
                column: "HardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Logon_UserId",
                table: "Logon",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ProducentId",
                table: "Model",
                column: "ProducentId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_LocationId",
                table: "Office",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Producent_CountryId",
                table: "Producent",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingSoftwareVersion_RatedById",
                table: "RatingSoftwareVersion",
                column: "RatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RatingSoftwareVersion_ReviewedProductId",
                table: "RatingSoftwareVersion",
                column: "ReviewedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingSoftwareVersion_SoftwareVersionId",
                table: "RatingSoftwareVersion",
                column: "SoftwareVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_EquipmentId",
                table: "Review",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewerId",
                table: "Review",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviwedEquipmentId",
                table: "Review",
                column: "ReviwedEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OfficeId",
                table: "Rooms",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PersonResponsibleId",
                table: "Rooms",
                column: "PersonResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_DepartmentId",
                table: "Software",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_PublisherId",
                table: "Software",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareLicense_SoftwareVersion",
                table: "SoftwareLicense",
                column: "SoftwareVersion");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareVersions_SoftwareId",
                table: "SoftwareVersions",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Employee_ManagerId",
                table: "Department",
                column: "ManagerId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Rooms_RoomId",
                table: "Employee",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Employee_ManagerId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Employee_PersonResponsibleId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "EmployeeLicense");

            migrationBuilder.DropTable(
                name: "HardwareLicense");

            migrationBuilder.DropTable(
                name: "InventoryProduct");

            migrationBuilder.DropTable(
                name: "Logon");

            migrationBuilder.DropTable(
                name: "RatingSoftwareVersion");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SoftwareLicense");

            migrationBuilder.DropTable(
                name: "Hardware");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "SoftwareVersions");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.DropTable(
                name: "Producent");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
