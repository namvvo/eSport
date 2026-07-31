using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.Catalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcategorySename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bet88Name",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "SeName",
                table: "Category",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeName",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Bet88Name",
                table: "Category",
                type: "text",
                nullable: true);
        }
    }
}
