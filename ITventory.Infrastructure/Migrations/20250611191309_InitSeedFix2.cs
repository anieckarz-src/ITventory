using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitSeedFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "CountryId", "Latitude", "Longitude", "Name", "TypeOfPlant", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-0002-0000-0000-000000000002"), "Kraków", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), 50.064700000000002, 19.945, "Krakow Warehouse", "Warehouse", "30-001" },
                    { new Guid("1a2b3c4d-0003-0000-0000-000000000003"), "Gdańsk", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), 54.351999999999997, 18.646599999999999, "Gdansk Other", "Other", "80-001" },
                    { new Guid("1a2b3c4d-0004-0000-0000-000000000004"), "Wrocław", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), 51.107900000000001, 17.038499999999999, "Wroclaw Factory", "Factory", "50-001" },
                    { new Guid("1a2b3c4d-0005-0000-0000-000000000005"), "Poznań", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0120"), 52.4084, 16.934200000000001, "Poznan Warehouse", "Warehouse", "60-001" },
                    { new Guid("2b3c4d5e-0001-0000-0000-000000000006"), "Skopje", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0117"), 41.998100000000001, 21.4254, "Skopje Factory", "Factory", "10-00" },
                    { new Guid("2b3c4d5e-0002-0000-0000-000000000007"), "Bitola", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0117"), 41.033299999999997, 21.333300000000001, "Bitola Warehouse", "Warehouse", "70-00" },
                    { new Guid("2b3c4d5e-0003-0000-0000-000000000008"), "Ohrid", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0117"), 41.116700000000002, 20.800000000000001, "Ohrid Other", "Other", "60-00" },
                    { new Guid("3c4d5e6f-0001-0000-0000-000000000009"), "Berlin", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0104"), 52.520000000000003, 13.404999999999999, "Berlin Factory", "Factory", "10-115" },
                    { new Guid("3c4d5e6f-0002-0000-0000-000000000010"), "Munich", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0104"), 48.135100000000001, 11.582000000000001, "Munich Warehouse", "Warehouse", "80-331" },
                    { new Guid("4d5e6f70-0001-0000-0000-000000000011"), "Amsterdam", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0113"), 52.367600000000003, 4.9040999999999997, "Amsterdam Factory", "Factory", "10-12" },
                    { new Guid("4d5e6f70-0002-0000-0000-000000000012"), "Rotterdam", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0113"), 51.922499999999999, 4.4791699999999999, "Rotterdam Warehouse", "Warehouse", "30-11" },
                    { new Guid("5e6f7081-0001-0000-0000-000000000013"), "London", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0109"), 51.507399999999997, -0.1278, "London Factory", "Factory", "EC1A-1BB" },
                    { new Guid("5e6f7081-0002-0000-0000-000000000014"), "Manchester", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0109"), 53.480800000000002, -2.2425999999999999, "Manchester Warehouse", "Warehouse", "M1-1AE" },
                    { new Guid("6f708192-0001-0000-0000-000000000015"), "Rome", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0105"), 41.902799999999999, 12.4964, "Rome Factory", "Factory", "00-184" },
                    { new Guid("6f708192-0002-0000-0000-000000000016"), "Milan", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0105"), 45.464199999999998, 9.1899999999999995, "Milan Warehouse", "Warehouse", "20-121" },
                    { new Guid("708192a3-0001-0000-0000-000000000017"), "Paris", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0106"), 48.8566, 2.3521999999999998, "Paris Factory", "Factory", "75-001" },
                    { new Guid("708192a3-0002-0000-0000-000000000018"), "Lyon", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0106"), 45.764000000000003, 4.8357000000000001, "Lyon Warehouse", "Warehouse", "69-001" },
                    { new Guid("8192a3b4-0001-0000-0000-000000000019"), "Madrid", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0110"), 40.416800000000002, -3.7038000000000002, "Madrid Factory", "Factory", "28-001" },
                    { new Guid("8192a3b4-0002-0000-0000-000000000020"), "Barcelona", new Guid("d61dc6e9-a541-4337-8c4d-7480dfdd0110"), 41.385100000000001, 2.1734, "Barcelona Warehouse", "Warehouse", "08-001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-0002-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-0003-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-0004-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-0005-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("2b3c4d5e-0001-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("2b3c4d5e-0002-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("2b3c4d5e-0003-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("3c4d5e6f-0001-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("3c4d5e6f-0002-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("4d5e6f70-0001-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("4d5e6f70-0002-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("5e6f7081-0001-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("5e6f7081-0002-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("6f708192-0001-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("6f708192-0002-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("708192a3-0001-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("708192a3-0002-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("8192a3b4-0001-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("8192a3b4-0002-0000-0000-000000000020"));
        }
    }
}
