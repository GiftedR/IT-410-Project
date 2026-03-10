using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT410Project.Lib.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class LongProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LongProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: false),
                    Deadline = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalWorkHours = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LongProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LongProjectDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    LongProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LongProjectDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LongProjectDays_LongProjects_LongProjectId",
                        column: x => x.LongProjectId,
                        principalTable: "LongProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LongProjectDays_LongProjectId",
                table: "LongProjectDays",
                column: "LongProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LongProjectDays");

            migrationBuilder.DropTable(
                name: "LongProjects");
        }
    }
}
