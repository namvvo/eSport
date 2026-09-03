using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_index_ownertype_onwerid_assetname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetName",
                table: "MediaAsset",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "MediaAsset",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerType",
                table: "MediaAsset",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAsset_OwnerId_OwnerType_AssetName",
                table: "MediaAsset",
                columns: new[] { "OwnerId", "OwnerType", "AssetName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MediaAsset_OwnerId_OwnerType_AssetName",
                table: "MediaAsset");

            migrationBuilder.DropColumn(
                name: "AssetName",
                table: "MediaAsset");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "MediaAsset");

            migrationBuilder.DropColumn(
                name: "OwnerType",
                table: "MediaAsset");
        }
    }
}
