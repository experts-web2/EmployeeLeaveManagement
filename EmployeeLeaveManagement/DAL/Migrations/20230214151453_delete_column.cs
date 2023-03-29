using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class delete_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Employees_EmployeeId",
                table: "Alerts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49acc326-851c-494b-9612-637bb4b55acc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a77fb03e-a07c-4a3d-88a8-3519a91625e0");

            migrationBuilder.DropColumn(
                name: "Employee_Id",
                table: "Alerts");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Alerts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d182f80-3677-4277-9961-5843186228b2", "0ac2fee1-ecfc-4d88-9b59-d94c6f0fcc33", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7909b04f-8d7f-4e9f-b1e7-e1b444b1216c", "d764f662-048f-4ad5-9526-e70df93f096b", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Employees_EmployeeId",
                table: "Alerts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Employees_EmployeeId",
                table: "Alerts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d182f80-3677-4277-9961-5843186228b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7909b04f-8d7f-4e9f-b1e7-e1b444b1216c");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Employee_Id",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49acc326-851c-494b-9612-637bb4b55acc", "a1d31602-b344-481a-adba-b1e34b98ecd9", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a77fb03e-a07c-4a3d-88a8-3519a91625e0", "b7d9f4be-84be-4d99-a000-77bebd0b5328", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Employees_EmployeeId",
                table: "Alerts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
