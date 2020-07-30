using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Persistence.Migrations.PostgreSQL
{
    public partial class Rename_WorkLogToWorklog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkLogs_Employees_EmployeeId",
                table: "WorkLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkLogs_Issues_IssueId",
                table: "WorkLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs");

            migrationBuilder.RenameTable(
                name: "WorkLogs",
                newName: "Worklogs");

            migrationBuilder.RenameIndex(
                name: "IX_WorkLogs_IssueId",
                table: "Worklogs",
                newName: "IX_Worklogs_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkLogs_EmployeeId",
                table: "Worklogs",
                newName: "IX_Worklogs_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Worklogs",
                table: "Worklogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Worklogs_Employees_EmployeeId",
                table: "Worklogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Worklogs_Issues_IssueId",
                table: "Worklogs",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worklogs_Employees_EmployeeId",
                table: "Worklogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Worklogs_Issues_IssueId",
                table: "Worklogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Worklogs",
                table: "Worklogs");

            migrationBuilder.RenameTable(
                name: "Worklogs",
                newName: "WorkLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Worklogs_IssueId",
                table: "WorkLogs",
                newName: "IX_WorkLogs_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Worklogs_EmployeeId",
                table: "WorkLogs",
                newName: "IX_WorkLogs_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkLogs",
                table: "WorkLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLogs_Employees_EmployeeId",
                table: "WorkLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLogs_Issues_IssueId",
                table: "WorkLogs",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
