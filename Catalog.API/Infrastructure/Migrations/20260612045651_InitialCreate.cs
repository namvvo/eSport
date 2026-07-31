using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pgvector;

#nullable disable

namespace eSport.Catalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "integer", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    Rounds = table.Column<int>(type: "integer", nullable: false),
                    Bet88Name = table.Column<string>(type: "text", nullable: true),
                    SofaScoreId = table.Column<int>(type: "integer", nullable: false),
                    CountryCSS = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Published = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Coefficient = table.Column<double>(type: "double precision", nullable: true),
                    FromMonth = table.Column<int>(type: "integer", nullable: false),
                    ToMonth = table.Column<int>(type: "integer", nullable: false),
                    MetaKeywords = table.Column<string>(type: "text", nullable: true),
                    MetaDescription = table.Column<string>(type: "text", nullable: true),
                    PictureId = table.Column<int>(type: "integer", nullable: false),
                    PageSize = table.Column<int>(type: "integer", nullable: false),
                    ShowOnHomePage = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeInTopMenu = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    UefaC1 = table.Column<int>(type: "integer", nullable: true),
                    UefaC1Qualifiers = table.Column<int>(type: "integer", nullable: true),
                    EuropaLeagueQualifiers = table.Column<int>(type: "integer", nullable: true),
                    EuropaLeague = table.Column<int>(type: "integer", nullable: true),
                    Relegation = table.Column<int>(type: "integer", nullable: true),
                    RelegationPlayOff = table.Column<int>(type: "integer", nullable: true),
                    Transfermarkt = table.Column<string>(type: "text", nullable: true),
                    LeagueLogo = table.Column<int>(type: "integer", nullable: true),
                    ShowStanding = table.Column<bool>(type: "boolean", nullable: true),
                    IsTournament = table.Column<bool>(type: "boolean", nullable: false),
                    IsData = table.Column<bool>(type: "boolean", nullable: false),
                    Embedding = table.Column<Vector>(type: "vector(384)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: true),
                    Year2 = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    C1WhoscoredName = table.Column<string>(type: "text", nullable: true),
                    C3WhoscoredName = table.Column<string>(type: "text", nullable: true),
                    EuroWhoscoredName = table.Column<string>(type: "text", nullable: true),
                    WCWhoscoredName = table.Column<string>(type: "text", nullable: true),
                    C1SofascoreName = table.Column<string>(type: "text", nullable: true),
                    C3SofascoreName = table.Column<string>(type: "text", nullable: true),
                    EuroSofascoreName = table.Column<string>(type: "text", nullable: true),
                    WCSofascoreName = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    GroupStage = table.Column<bool>(type: "boolean", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    NoOfMatches = table.Column<int>(type: "integer", nullable: true),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    Display = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category_Stage_Mapping",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    FromMonth = table.Column<int>(type: "integer", nullable: false),
                    ToMonth = table.Column<int>(type: "integer", nullable: false),
                    FromDateUseYearPart = table.Column<int>(type: "integer", nullable: false),
                    ToDateUseYearPart = table.Column<int>(type: "integer", nullable: false),
                    FixtureCount = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Stage_Mapping", x => new { x.CategoryId, x.StageId });
                    table.ForeignKey(
                        name: "FK_Category_Stage_Mapping_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Category_Stage_Mapping_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Season_Stage_Mapping",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    SeasonId = table.Column<int>(type: "integer", nullable: false),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season_Stage_Mapping", x => new { x.SeasonId, x.StageId });
                    table.ForeignKey(
                        name: "FK_Season_Stage_Mapping_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Season_Stage_Mapping_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Stage_Mapping_StageId",
                table: "Category_Stage_Mapping",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Season_Stage_Mapping_StageId",
                table: "Season_Stage_Mapping",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_Name",
                table: "Stage",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_Stage_Mapping");

            migrationBuilder.DropTable(
                name: "Season_Stage_Mapping");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Stage");
        }
    }
}
