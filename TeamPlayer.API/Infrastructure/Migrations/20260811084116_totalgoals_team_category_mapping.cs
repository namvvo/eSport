using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.TeamPlayer.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class totalgoals_team_category_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "TeamGoals",
                table: "Team_Category_Mapping",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamGoals",
                table: "Team_Category_Mapping");

        }
    }
}
 