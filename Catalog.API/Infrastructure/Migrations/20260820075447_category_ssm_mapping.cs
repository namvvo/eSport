using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.Catalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class category_ssm_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category_SSM_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    SeasonStageId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CompleteRound = table.Column<int>(type: "integer", nullable: false),
                    FromMonth = table.Column<int>(type: "integer", nullable: false),
                    ToMonth = table.Column<int>(type: "integer", nullable: false),
                    ToDateUseYearPart = table.Column<int>(type: "integer", nullable: false),
                    FixtureCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_SSM_Mapping", x => new { x.Id, x.CategoryId, x.SeasonStageId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_SSM_Mapping");
        }
    }
}
