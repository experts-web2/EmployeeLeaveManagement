using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class removeDesignationProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5deca622-ee14-4452-a84d-6869b4d230f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f04dfa79-18f3-4fbc-b249-765ac924994d");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Attendences");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7390b6e8-b65e-4787-8e53-b7f633e04966", "75796cb2-145f-43bf-8a96-30622070ca7d", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c84d0ed4-806d-4620-bad5-13321e7cad38", "59b090cd-da5d-4f57-861b-da431b5e9040", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7390b6e8-b65e-4787-8e53-b7f633e04966");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c84d0ed4-806d-4620-bad5-13321e7cad38");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Attendences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5deca622-ee14-4452-a84d-6869b4d230f6", "e2e6260a-1d61-48fd-99c2-4ef9254cf15d", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f04dfa79-18f3-4fbc-b249-765ac924994d", "9d98d88f-6d91-4ea6-88c9-440e3ffd9abe", "Administrator", "ADMINISTRATOR" });
        }
    }
}
