using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class addSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84a42e75-21ff-4e95-b1f6-0218cceefd6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffca8de7-b71a-48e9-ac70-fb18abc32196");

            migrationBuilder.AddColumn<double>(
                name: "CurrentSalary",
                table: "Employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "SalaryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewSalary = table.Column<double>(type: "float", nullable: false),
                    IncrementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02a2016d-1fd4-4a48-ba69-447f508b020d", "48a6286c-8a4c-4c90-8cbb-4947fbbc754f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8943d1e0-11e6-4585-b625-e61da6403db7", "6c7040b2-c014-49b4-ad05-4441016b6d03", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHistories_EmployeeId",
                table: "SalaryHistories",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryHistories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02a2016d-1fd4-4a48-ba69-447f508b020d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8943d1e0-11e6-4585-b625-e61da6403db7");

            migrationBuilder.DropColumn(
                name: "CurrentSalary",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84a42e75-21ff-4e95-b1f6-0218cceefd6e", "fab73fe2-5f96-4abd-8b94-f686aef07686", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ffca8de7-b71a-48e9-ac70-fb18abc32196", "7901a4f8-6677-42a2-88f0-33166e4ae0c7", "Employee", "EMPLOYEE" });
        }
    }
}
