using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.TeamPlayer.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addindex_to_teamcategory_teamPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Team_Category_Mapping_CategoryId",
                table: "Team_Category_Mapping",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Team_Category_Mapping_CategoryId",
                table: "Team_Category_Mapping");
        }
    }
}
