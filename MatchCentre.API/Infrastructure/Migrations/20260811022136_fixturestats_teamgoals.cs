using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.MatchCentre.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixturestats_teamgoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamGoals",
                table: "FixtureStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamGoals",
                table: "FixtureStats");
        }
    }
}
