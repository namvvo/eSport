using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAssetStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "MediaAsset");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "MediaAsset",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MediaAsset");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "MediaAsset",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
