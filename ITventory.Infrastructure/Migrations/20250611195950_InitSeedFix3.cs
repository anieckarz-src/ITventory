using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitSeedFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude_Value",
                table: "Office",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Lattitude_Value",
                table: "Office",
                newName: "Latitude");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Office",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                table: "Office",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-0001-0000-0000-000000000001"),
                column: "Name",
                value: "Warsaw AGR East");

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "BuildingNumber", "IsActive", "Latitude", "LocationId", "Longitude", "Street" },
                values: new object[,]
                {
                    { new Guid("f1a2b3c4-0001-0000-0000-000000000001"), "3A", true, 52.229700000000001, new Guid("1a2b3c4d-0001-0000-0000-000000000001"), 21.0122, "Marszałkowska 5" },
                    { new Guid("f1a2b3c4-0002-0000-0000-000000000002"), "5B", true, 50.064700000000002, new Guid("1a2b3c4d-0002-0000-0000-000000000002"), 19.945, "Krakowska 12" },
                    { new Guid("f1a2b3c4-0003-0000-0000-000000000003"), "1C", true, 54.351999999999997, new Guid("1a2b3c4d-0003-0000-0000-000000000003"), 18.646599999999999, "Długa 7" },
                    { new Guid("f1a2b3c4-0004-0000-0000-000000000004"), "2", true, 51.107900000000001, new Guid("1a2b3c4d-0004-0000-0000-000000000004"), 17.038499999999999, "Rynek 1" },
                    { new Guid("f1a2b3c4-0005-0000-0000-000000000005"), "A", true, 52.4084, new Guid("1a2b3c4d-0005-0000-0000-000000000005"), 16.934200000000001, "Stary Rynek 10" },
                    { new Guid("f1a2b3c4-0006-0000-0000-000000000006"), "1", true, 41.998100000000001, new Guid("2b3c4d5e-0001-0000-0000-000000000006"), 21.4254, "Main Street 10" },
                    { new Guid("f1a2b3c4-0007-0000-0000-000000000007"), "12", true, 41.033299999999997, new Guid("2b3c4d5e-0002-0000-0000-000000000007"), 21.333300000000001, "Bitola Blvd 45" },
                    { new Guid("f1a2b3c4-0008-0000-0000-000000000008"), "4C", true, 41.116700000000002, new Guid("2b3c4d5e-0003-0000-0000-000000000008"), 20.800000000000001, "Ohrid Lakeside 3" },
                    { new Guid("f1a2b3c4-0009-0000-0000-000000000009"), "10", true, 52.520000000000003, new Guid("3c4d5e6f-0001-0000-0000-000000000009"), 13.404999999999999, "Alexanderplatz 2" },
                    { new Guid("f1a2b3c4-0010-0000-0000-000000000010"), "7B", true, 48.135100000000001, new Guid("3c4d5e6f-0002-0000-0000-000000000010"), 11.582000000000001, "Marienplatz 1" },
                    { new Guid("f1a2b3c4-0011-0000-0000-000000000011"), "5A", true, 52.367600000000003, new Guid("4d5e6f70-0001-0000-0000-000000000011"), 4.9040999999999997, "Damrak 20" },
                    { new Guid("f1a2b3c4-0012-0000-0000-000000000012"), "2", true, 51.922499999999999, new Guid("4d5e6f70-0002-0000-0000-000000000012"), 4.4791699999999999, "Coolsingel 100" },
                    { new Guid("f1a2b3c4-0013-0000-0000-000000000013"), "1", true, 51.507399999999997, new Guid("5e6f7081-0001-0000-0000-000000000013"), -0.1278, "Baker Street 221B" },
                    { new Guid("f1a2b3c4-0014-0000-0000-000000000014"), "5", true, 53.480800000000002, new Guid("5e6f7081-0002-0000-0000-000000000014"), -2.2425999999999999, "Deansgate 50" },
                    { new Guid("f1a2b3c4-0015-0000-0000-000000000015"), "6", true, 41.902799999999999, new Guid("6f708192-0001-0000-0000-000000000015"), 12.4964, "Via del Corso 15" },
                    { new Guid("f1a2b3c4-0016-0000-0000-000000000016"), "3", true, 45.464199999999998, new Guid("6f708192-0002-0000-0000-000000000016"), 9.1899999999999995, "Via Monte Napoleone 20" },
                    { new Guid("f1a2b3c4-0017-0000-0000-000000000017"), "8", true, 48.8566, new Guid("708192a3-0001-0000-0000-000000000017"), 2.3521999999999998, "Rue de Rivoli 10" },
                    { new Guid("f1a2b3c4-0018-0000-0000-000000000018"), "4", true, 45.764000000000003, new Guid("708192a3-0002-0000-0000-000000000018"), 4.8357000000000001, "Rue Mercière 12" },
                    { new Guid("f1a2b3c4-0019-0000-0000-000000000019"), "7", true, 40.416800000000002, new Guid("8192a3b4-0001-0000-0000-000000000019"), -3.7038000000000002, "Gran Via 50" },
                    { new Guid("f1a2b3c4-0020-0000-0000-000000000020"), "9", true, 41.385100000000001, new Guid("8192a3b4-0002-0000-0000-000000000020"), 2.1734, "Passeig de Gràcia 30" },
                    { new Guid("f1a2b3c4-0021-0000-0000-000000000021"), "5", true, 52.229700000000001, new Guid("1a2b3c4d-0001-0000-0000-000000000001"), 21.0122, "Nowy Świat 20" },
                    { new Guid("f1a2b3c4-0022-0000-0000-000000000022"), "2", true, 51.107900000000001, new Guid("1a2b3c4d-0004-0000-0000-000000000004"), 17.038499999999999, "Kazimierza Wielkiego 8" },
                    { new Guid("f1a2b3c4-0023-0000-0000-000000000023"), "3", true, 54.351999999999997, new Guid("1a2b3c4d-0003-0000-0000-000000000003"), 18.646599999999999, "Chmielna 10" },
                    { new Guid("f1a2b3c4-0024-0000-0000-000000000024"), "6", true, 50.064700000000002, new Guid("1a2b3c4d-0002-0000-0000-000000000002"), 19.945, "Krakowska 45" },
                    { new Guid("f1a2b3c4-0025-0000-0000-000000000025"), "1", true, 52.4084, new Guid("1a2b3c4d-0005-0000-0000-000000000005"), 16.934200000000001, "Plac Wolności 5" },
                    { new Guid("f1a2b3c4-0026-0000-0000-000000000026"), "7", true, 41.998100000000001, new Guid("2b3c4d5e-0001-0000-0000-000000000006"), 21.4254, "Skopje Center 3" },
                    { new Guid("f1a2b3c4-0027-0000-0000-000000000027"), "8", true, 52.520000000000003, new Guid("3c4d5e6f-0001-0000-0000-000000000009"), 13.404999999999999, "Berlin Wall Str 15" },
                    { new Guid("f1a2b3c4-0028-0000-0000-000000000028"), "9", true, 51.507399999999997, new Guid("5e6f7081-0001-0000-0000-000000000013"), -0.1278, "London Bridge 22" },
                    { new Guid("f1a2b3c4-0029-0000-0000-000000000029"), "4", true, 41.902799999999999, new Guid("6f708192-0001-0000-0000-000000000015"), 12.4964, "Via Roma 100" },
                    { new Guid("f1a2b3c4-0030-0000-0000-000000000030"), "3", true, 48.8566, new Guid("708192a3-0001-0000-0000-000000000017"), 2.3521999999999998, "Rue Lafayette 10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0001-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0002-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0003-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0004-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0005-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0006-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0007-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0008-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0009-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0010-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0011-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0012-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0013-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0014-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0015-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0016-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0017-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0018-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0019-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0020-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0021-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0022-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0023-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0024-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0025-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0026-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0027-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0028-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0029-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-0030-0000-0000-000000000030"));

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Office",
                newName: "Longitude_Value");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Office",
                newName: "Lattitude_Value");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Office",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                table: "Office",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-0001-0000-0000-000000000001"),
                column: "Name",
                value: "Warsaw Factory");
        }
    }
}
