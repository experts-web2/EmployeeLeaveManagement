using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class edit_colom_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29c915e0-b7cc-4e03-9d66-b701933673cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "416a1446-b35b-4467-8511-7bfb5419b07f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84a42e75-21ff-4e95-b1f6-0218cceefd6e", "fab73fe2-5f96-4abd-8b94-f686aef07686", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ffca8de7-b71a-48e9-ac70-fb18abc32196", "7901a4f8-6677-42a2-88f0-33166e4ae0c7", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84a42e75-21ff-4e95-b1f6-0218cceefd6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffca8de7-b71a-48e9-ac70-fb18abc32196");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29c915e0-b7cc-4e03-9d66-b701933673cb", "255dde61-46d4-4dd5-9059-e01dea348241", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "416a1446-b35b-4467-8511-7bfb5419b07f", "3002da4e-96d7-485f-93a3-5bfc8daa1afc", "Employee", "EMPLOYEE" });
        }
    }
}
