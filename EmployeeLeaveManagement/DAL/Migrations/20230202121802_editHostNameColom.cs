using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class editHostNameColom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "hostName",
                table: "Attendences",
                newName: "HostName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29c915e0-b7cc-4e03-9d66-b701933673cb", "255dde61-46d4-4dd5-9059-e01dea348241", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "416a1446-b35b-4467-8511-7bfb5419b07f", "3002da4e-96d7-485f-93a3-5bfc8daa1afc", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29c915e0-b7cc-4e03-9d66-b701933673cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "416a1446-b35b-4467-8511-7bfb5419b07f");

            migrationBuilder.RenameColumn(
                name: "HostName",
                table: "Attendences",
                newName: "hostName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a69f76ae-e9d4-4db5-a867-0584f93f31e4", "1a5fab46-3b2a-44a8-bb3f-5eeba5eeaa89", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3ad2ca0-1392-46c1-a8b3-4d89a2c48715", "1f78c135-007c-4346-ad7e-b496940087e0", "Employee", "EMPLOYEE" });
        }
    }
}
