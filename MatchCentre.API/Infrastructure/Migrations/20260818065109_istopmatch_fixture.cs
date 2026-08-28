using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.MatchCentre.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class istopmatch_fixture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTopMatch",
                table: "Fixture",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTopMatch",
                table: "Fixture");
        }
    }
}
