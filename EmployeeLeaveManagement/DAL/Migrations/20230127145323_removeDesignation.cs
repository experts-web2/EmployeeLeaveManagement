using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class removeDesignation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7390b6e8-b65e-4787-8e53-b7f633e04966");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c84d0ed4-806d-4620-bad5-13321e7cad38");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b1af69f-423c-4767-864b-cee3e603b4ed", "b3053efe-3a8e-425e-b200-51a9efacf42d", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee8ac410-edfc-40b0-9564-2a5e90dffc92", "6043d164-27d0-41f3-af7c-d6c98ed4742e", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b1af69f-423c-4767-864b-cee3e603b4ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee8ac410-edfc-40b0-9564-2a5e90dffc92");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7390b6e8-b65e-4787-8e53-b7f633e04966", "75796cb2-145f-43bf-8a96-30622070ca7d", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c84d0ed4-806d-4620-bad5-13321e7cad38", "59b090cd-da5d-4f57-861b-da431b5e9040", "Administrator", "ADMINISTRATOR" });
        }
    }
}
