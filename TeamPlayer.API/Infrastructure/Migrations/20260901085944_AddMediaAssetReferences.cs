using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.TeamPlayer.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaAssetReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoMediaId",
                table: "Team",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageMediaId",
                table: "Player",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoMediaId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "ImageMediaId",
                table: "Player");
        }
    }
}
