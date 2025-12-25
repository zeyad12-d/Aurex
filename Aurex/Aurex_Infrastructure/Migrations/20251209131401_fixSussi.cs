using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurex_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixSussi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Deals",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Deals");
        }
    }
}
