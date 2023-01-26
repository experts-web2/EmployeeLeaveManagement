using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class createAttendence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32b95a1a-ff12-432f-b250-453e2610e803");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d47ad64-2770-43b6-bc8d-7158091de26e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a864ad0-d763-473e-b262-0732267562e2", "d99ef420-a07c-4ebd-b0c5-923bdfd80cad", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef6e013c-5214-4bbe-80b8-ddfa3c934523", "3e8b833f-4523-41aa-be13-6dea336c239b", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a864ad0-d763-473e-b262-0732267562e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef6e013c-5214-4bbe-80b8-ddfa3c934523");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32b95a1a-ff12-432f-b250-453e2610e803", "d2c61ce4-1168-41c0-95ce-c0def4204a8b", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d47ad64-2770-43b6-bc8d-7158091de26e", "5e7310d4-b79b-4166-a1b0-4df8df4f35fd", "Administrator", "ADMINISTRATOR" });
        }
    }
}
