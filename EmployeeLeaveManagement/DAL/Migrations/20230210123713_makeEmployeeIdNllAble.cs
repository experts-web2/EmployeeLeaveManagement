using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class makeEmployeeIdNllAble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "13538493-45d0-4dd1-ba09-a9d73c9d96c0", "9c27fe06-44fc-4743-8e87-fdc6e4fc832f", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6431ea38-4a99-414c-bdcc-b55d71104ec8", "620ddaef-9464-42fe-87ab-37b03112d7b7", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13538493-45d0-4dd1-ba09-a9d73c9d96c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6431ea38-4a99-414c-bdcc-b55d71104ec8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d3db6aa-000b-4d0e-b419-f8ad403adc00", "67b3c19f-45f9-47fb-af6a-fe93bf2bec3c", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c352997-0b46-4683-b587-94dd2e149bee", "b70a883e-0ccf-49c1-9558-a03ce5bd8f23", "Employee", "EMPLOYEE" });
        }
    }
}
