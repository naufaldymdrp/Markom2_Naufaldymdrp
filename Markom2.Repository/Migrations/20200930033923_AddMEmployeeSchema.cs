using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Markom2.Repository.Migrations
{
    public partial class AddMEmployeeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M_Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    First_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    M_Company_Id = table.Column<int>(nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
                    Is_Delete = table.Column<bool>(nullable: false),
                    Created_By = table.Column<string>(nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M_Employee_AspNetUsers_Created_By",
                        column: x => x.Created_By,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_Employee_M_Company_M_Company_Id",
                        column: x => x.M_Company_Id,
                        principalTable: "M_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_M_Employee_AspNetUsers_Updated_By",
                        column: x => x.Updated_By,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_Employee_Created_By",
                table: "M_Employee",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_M_Employee_M_Company_Id",
                table: "M_Employee",
                column: "M_Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_M_Employee_Updated_By",
                table: "M_Employee",
                column: "Updated_By");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_Employee");
        }
    }
}
