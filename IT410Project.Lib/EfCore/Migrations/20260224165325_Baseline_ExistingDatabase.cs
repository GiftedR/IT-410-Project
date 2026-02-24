using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT410Project.Lib.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Baseline_ExistingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<string>(type: "DATETIME", nullable: false),
                    EndTime = table.Column<string>(type: "DATETIME", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: false),
                    IsRepeating = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sleep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<string>(type: "DATETIME", nullable: false),
                    EndTime = table.Column<string>(type: "DATETIME", nullable: false),
                    Quality = table.Column<int>(type: "INTEGER", nullable: false),
                    RepeatDays = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sleep", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Sleep");
        }
    }
}
