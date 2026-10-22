using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adressr.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkersToPositionExplicitJoinPlainTextiesConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlainTextEmployees_Positions_PositionID",
                table: "PlainTextEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersToPosition_Positions_PositionsPositionID",
                table: "WorkersToPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersToPosition_Users_WorkersUserID",
                table: "WorkersToPosition");

            migrationBuilder.DropIndex(
                name: "IX_PlainTextEmployees_PositionID",
                table: "PlainTextEmployees");

            migrationBuilder.RenameColumn(
                name: "WorkersUserID",
                table: "WorkersToPosition",
                newName: "PositionID");

            migrationBuilder.RenameColumn(
                name: "PositionsPositionID",
                table: "WorkersToPosition",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkersToPosition_WorkersUserID",
                table: "WorkersToPosition",
                newName: "IX_WorkersToPosition_PositionID");

            migrationBuilder.RenameColumn(
                name: "PositionID",
                table: "PlainTextEmployees",
                newName: "CompanyID");

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "WorkersToPosition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Wage",
                table: "WorkersToPosition",
                type: "decimal(15,2)",
                precision: 15,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WageType",
                table: "WorkersToPosition",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Positions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PlainTextEmployees",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Openings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastClosedTime",
                table: "Openings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReopenedTime",
                table: "Openings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isClosed",
                table: "Openings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PlainTextEmployeesPositions",
                columns: table => new
                {
                    PlainTextiesPlainTextEmployeeID = table.Column<int>(type: "int", nullable: false),
                    PositionsPositionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlainTextEmployeesPositions", x => new { x.PlainTextiesPlainTextEmployeeID, x.PositionsPositionID });
                    table.ForeignKey(
                        name: "FK_PlainTextEmployeesPositions_PlainTextEmployees_PlainTextiesPlainTextEmployeeID",
                        column: x => x.PlainTextiesPlainTextEmployeeID,
                        principalTable: "PlainTextEmployees",
                        principalColumn: "PlainTextEmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlainTextEmployeesPositions_Positions_PositionsPositionID",
                        column: x => x.PositionsPositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlainTextEmployees_CompanyID_Email",
                table: "PlainTextEmployees",
                columns: new[] { "CompanyID", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlainTextEmployeesPositions_PositionsPositionID",
                table: "PlainTextEmployeesPositions",
                column: "PositionsPositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlainTextEmployees_Companies_CompanyID",
                table: "PlainTextEmployees",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersToPosition_Positions_PositionID",
                table: "WorkersToPosition",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "PositionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersToPosition_Users_UserID",
                table: "WorkersToPosition",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlainTextEmployees_Companies_CompanyID",
                table: "PlainTextEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersToPosition_Positions_PositionID",
                table: "WorkersToPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersToPosition_Users_UserID",
                table: "WorkersToPosition");

            migrationBuilder.DropTable(
                name: "PlainTextEmployeesPositions");

            migrationBuilder.DropIndex(
                name: "IX_PlainTextEmployees_CompanyID_Email",
                table: "PlainTextEmployees");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "WorkersToPosition");

            migrationBuilder.DropColumn(
                name: "Wage",
                table: "WorkersToPosition");

            migrationBuilder.DropColumn(
                name: "WageType",
                table: "WorkersToPosition");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PlainTextEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Openings");

            migrationBuilder.DropColumn(
                name: "LastClosedTime",
                table: "Openings");

            migrationBuilder.DropColumn(
                name: "ReopenedTime",
                table: "Openings");

            migrationBuilder.DropColumn(
                name: "isClosed",
                table: "Openings");

            migrationBuilder.RenameColumn(
                name: "PositionID",
                table: "WorkersToPosition",
                newName: "WorkersUserID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "WorkersToPosition",
                newName: "PositionsPositionID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkersToPosition_PositionID",
                table: "WorkersToPosition",
                newName: "IX_WorkersToPosition_WorkersUserID");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "PlainTextEmployees",
                newName: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_PlainTextEmployees_PositionID",
                table: "PlainTextEmployees",
                column: "PositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlainTextEmployees_Positions_PositionID",
                table: "PlainTextEmployees",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "PositionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersToPosition_Positions_PositionsPositionID",
                table: "WorkersToPosition",
                column: "PositionsPositionID",
                principalTable: "Positions",
                principalColumn: "PositionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersToPosition_Users_WorkersUserID",
                table: "WorkersToPosition",
                column: "WorkersUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
