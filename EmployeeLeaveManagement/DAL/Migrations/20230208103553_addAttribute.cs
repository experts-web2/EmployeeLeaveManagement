using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class addAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02a2016d-1fd4-4a48-ba69-447f508b020d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8943d1e0-11e6-4585-b625-e61da6403db7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d3db6aa-000b-4d0e-b419-f8ad403adc00", "67b3c19f-45f9-47fb-af6a-fe93bf2bec3c", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c352997-0b46-4683-b587-94dd2e149bee", "b70a883e-0ccf-49c1-9558-a03ce5bd8f23", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d3db6aa-000b-4d0e-b419-f8ad403adc00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c352997-0b46-4683-b587-94dd2e149bee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02a2016d-1fd4-4a48-ba69-447f508b020d", "48a6286c-8a4c-4c90-8cbb-4947fbbc754f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8943d1e0-11e6-4585-b625-e61da6403db7", "6c7040b2-c014-49b4-ad05-4441016b6d03", "Employee", "EMPLOYEE" });
        }
    }
}
