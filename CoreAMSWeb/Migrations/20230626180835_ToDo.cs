using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreAMSWeb.Migrations
{
    /// <inheritdoc />
    public partial class ToDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectNameId = table.Column<int>(type: "int", nullable: false),
                    ActivityNameId = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDo_ActivityType_ActivityNameId",
                        column: x => x.ActivityNameId,
                        principalTable: "ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDo_ProjectType_ProjectNameId",
                        column: x => x.ProjectNameId,
                        principalTable: "ProjectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_ActivityNameId",
                table: "ToDo",
                column: "ActivityNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_ProjectNameId",
                table: "ToDo",
                column: "ProjectNameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDo");
        }
    }
}
