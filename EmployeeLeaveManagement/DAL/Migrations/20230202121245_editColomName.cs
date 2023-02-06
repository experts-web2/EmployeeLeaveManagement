using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class editColomName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71543425-6ce3-401b-9d0a-06b2401bb6f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1fe1c7f-3f79-4196-822c-a3140042e497");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Attendences",
                newName: "AttendenceDate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a69f76ae-e9d4-4db5-a867-0584f93f31e4", "1a5fab46-3b2a-44a8-bb3f-5eeba5eeaa89", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3ad2ca0-1392-46c1-a8b3-4d89a2c48715", "1f78c135-007c-4346-ad7e-b496940087e0", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a69f76ae-e9d4-4db5-a867-0584f93f31e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3ad2ca0-1392-46c1-a8b3-4d89a2c48715");

            migrationBuilder.RenameColumn(
                name: "AttendenceDate",
                table: "Attendences",
                newName: "Date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71543425-6ce3-401b-9d0a-06b2401bb6f5", "c2d49373-db7f-4d09-8e76-60a65fbb2bac", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1fe1c7f-3f79-4196-822c-a3140042e497", "fca9cbea-1cbe-4dad-916e-e861d99cb37f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
