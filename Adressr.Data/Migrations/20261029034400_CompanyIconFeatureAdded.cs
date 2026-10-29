using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adressr.Data.Migrations
{
    /// <inheritdoc />
    public partial class CompanyIconFeatureAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyIconImageLocation",
                table: "Companies",
                type: "VARCHAR(300)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyIconImageLocation",
                table: "Companies");
        }
    }
}
