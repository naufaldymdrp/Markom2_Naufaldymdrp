using Microsoft.EntityFrameworkCore.Migrations;

namespace Markom2.Repository.Migrations
{
    public partial class AddIndexAddIsUnique_Code_MEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_M_Employee_Employee_Number",
                table: "M_Employee",
                column: "Employee_Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_M_Employee_Employee_Number",
                table: "M_Employee");
        }
    }
}
