using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.MatchCentre.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_totalgoals_from_fixturestats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamGoals",
                table: "FixtureStats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamGoals",
                table: "FixtureStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
