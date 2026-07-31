using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.TeamPlayer.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addkeySeasonstageId_to_team_category_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Team_Category_Mapping",
                table: "Team_Category_Mapping");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team_Category_Mapping",
                table: "Team_Category_Mapping",
                columns: new[] { "TeamId", "CategoryId", "SeasonStageId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Team_Category_Mapping",
                table: "Team_Category_Mapping");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team_Category_Mapping",
                table: "Team_Category_Mapping",
                columns: new[] { "TeamId", "CategoryId" });
        }
    }
}
