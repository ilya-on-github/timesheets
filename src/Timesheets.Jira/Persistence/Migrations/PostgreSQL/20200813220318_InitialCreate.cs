using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Jira.Persistence.Migrations.PostgreSQL
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Checked = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Key = table.Column<string>(maxLength: 32, nullable: true),
                    Summary = table.Column<string>(maxLength: 1024, nullable: true),
                    AccountKey = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worklogs",
                columns: table => new
                {
                    TempoWorklogId = table.Column<int>(nullable: false),
                    Checked = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Worker = table.Column<string>(maxLength: 128, nullable: true),
                    IssueId = table.Column<int>(nullable: false),
                    Started = table.Column<DateTimeOffset>(nullable: false),
                    TimeSpentSeconds = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worklogs", x => x.TempoWorklogId);
                    table.ForeignKey(
                        name: "FK_Worklogs_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worklogs_IssueId",
                table: "Worklogs",
                column: "IssueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Worklogs");

            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
