using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Markom2.Repository.Migrations
{
    public partial class AddMUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    M_Role_Id = table.Column<int>(nullable: false),
                    M_Employee_Id = table.Column<int>(nullable: false),
                    Is_Delete = table.Column<bool>(nullable: false),
                    Created_By = table.Column<string>(nullable: false),
                    Created_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M_User_AspNetUsers_Created_By",
                        column: x => x.Created_By,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_User_M_Employee_M_Employee_Id",
                        column: x => x.M_Employee_Id,
                        principalTable: "M_Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_M_User_M_Role_M_Role_Id",
                        column: x => x.M_Role_Id,
                        principalTable: "M_Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_M_User_AspNetUsers_Updated_By",
                        column: x => x.Updated_By,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_User_Created_By",
                table: "M_User",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_M_User_M_Employee_Id",
                table: "M_User",
                column: "M_Employee_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_M_User_M_Role_Id",
                table: "M_User",
                column: "M_Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_M_User_Updated_By",
                table: "M_User",
                column: "Updated_By");

            migrationBuilder.CreateIndex(
                name: "IX_M_User_Username",
                table: "M_User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_User");
        }
    }
}
