using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adressr.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentTaskID",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ParentTaskID",
                table: "Tasks",
                column: "ParentTaskID");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkTask_NotOwnParent",
                table: "Tasks",
                sql: "[ParentTaskID] IS NULL OR [ParentTaskID] <> [WorkTaskID]");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_ParentTaskID",
                table: "Tasks",
                column: "ParentTaskID",
                principalTable: "Tasks",
                principalColumn: "WorkTaskID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_ParentTaskID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ParentTaskID",
                table: "Tasks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkTask_NotOwnParent",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ParentTaskID",
                table: "Tasks");
        }
    }
}
