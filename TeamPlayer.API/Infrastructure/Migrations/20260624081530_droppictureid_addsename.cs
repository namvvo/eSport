using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.TeamPlayer.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class droppictureid_addsename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "SquawkaId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Team",
                newName: "SeName");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Player",
                newName: "SeName");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Team",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Player",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "SeName",
                table: "Team",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "SeName",
                table: "Player",
                newName: "Slug");

            migrationBuilder.AddColumn<int>(
                name: "Logo",
                table: "Team",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SquawkaId",
                table: "Team",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
