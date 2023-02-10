using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class EmployeeId_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69a3e97f-9212-431d-b7ca-597879bcbaa9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c687478d-d3f9-4e47-9cd1-43b86ab50573");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "289b616a-b9ab-4b45-86d1-89ae3ceaba78", "5de9a3c0-0bfc-475f-ba33-d3f2c393b7fc", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0f0485e-fc51-41fb-a98a-ce3186c4c5b8", "e3e7cf29-1a84-47de-ad37-6f152b1512e7", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "289b616a-b9ab-4b45-86d1-89ae3ceaba78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0f0485e-fc51-41fb-a98a-ce3186c4c5b8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69a3e97f-9212-431d-b7ca-597879bcbaa9", "5008073b-6896-44a6-bce7-4c503b69d0e0", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c687478d-d3f9-4e47-9cd1-43b86ab50573", "1e2de659-c569-4bd7-8043-0458aebc280c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
