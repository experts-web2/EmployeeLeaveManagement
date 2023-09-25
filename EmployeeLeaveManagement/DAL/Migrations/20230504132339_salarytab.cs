using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class salarytab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentSalary",
                table: "SalaryHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Deduction",
                table: "SalaryHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyWorkingHours",
                table: "SalaryHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PerviousSalary",
                table: "SalaryHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentSalary",
                table: "SalaryHistories");

            migrationBuilder.DropColumn(
                name: "Deduction",
                table: "SalaryHistories");

            migrationBuilder.DropColumn(
                name: "MonthlyWorkingHours",
                table: "SalaryHistories");

            migrationBuilder.DropColumn(
                name: "PerviousSalary",
                table: "SalaryHistories");
        }
    }
}
