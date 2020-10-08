using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Markom2.Repository.Migrations
{
    public partial class AddRoleTableAndSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Is_Delete = table.Column<bool>(nullable: false),
                    Created_By = table.Column<string>(nullable: false),
                    Created_Date = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M_Role_AspNetUsers_Created_By",
                        column: x => x.Created_By,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_Role_AspNetUsers_Updated_By",
                        column: x => x.Updated_By,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_Role_Created_By",
                table: "M_Role",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_M_Role_Updated_By",
                table: "M_Role",
                column: "Updated_By");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_Role");
        }
    }
}
