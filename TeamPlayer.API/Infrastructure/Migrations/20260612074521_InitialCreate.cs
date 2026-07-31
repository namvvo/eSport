using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eSport.TeamPlayer.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    CountryId2 = table.Column<int>(type: "integer", nullable: true),
                    Height = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<string>(type: "text", nullable: true),
                    PictureId = table.Column<int>(type: "integer", nullable: false),
                    WhoscoredPlayerId = table.Column<int>(type: "integer", nullable: true),
                    SofascorePlayerId = table.Column<int>(type: "integer", nullable: true),
                    SquawkaPlayerId = table.Column<int>(type: "integer", nullable: true),
                    GoalPlayerId = table.Column<int>(type: "integer", nullable: true),
                    InternationalCaps = table.Column<int>(type: "integer", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    TransferMkName = table.Column<string>(type: "text", nullable: true),
                    TeamPosition = table.Column<string>(type: "text", nullable: true),
                    PercentageGainInLast6Rounds = table.Column<decimal>(type: "numeric", nullable: false),
                    FantasyOwner = table.Column<int>(type: "integer", nullable: true),
                    AttackIndexInLast6Rounds = table.Column<int>(type: "integer", nullable: true),
                    DefenseIndexInLast6Rounds = table.Column<int>(type: "integer", nullable: true),
                    MarketValue = table.Column<int>(type: "integer", nullable: true),
                    PreferredFoot = table.Column<string>(type: "text", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: true),
                    SkillSets = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UefaName = table.Column<string>(type: "text", nullable: false),
                    UefaRanking = table.Column<int>(type: "integer", nullable: false),
                    Fame = table.Column<int>(type: "integer", nullable: false),
                    SofascoreId = table.Column<int>(type: "integer", nullable: true),
                    SquawkaId = table.Column<int>(type: "integer", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Web = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<int>(type: "integer", nullable: false),
                    AutoUrl = table.Column<string>(type: "text", nullable: true),
                    Theme = table.Column<string>(type: "text", nullable: true),
                    Background = table.Column<string>(type: "text", nullable: true),
                    TransferMarktUrl = table.Column<string>(type: "text", nullable: true),
                    TransferMarktName = table.Column<string>(type: "text", nullable: true),
                    TransferMarkUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Bet188 = table.Column<string>(type: "text", nullable: true),
                    WhoscoredId = table.Column<int>(type: "integer", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: true),
                    GoalTeamCheck = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TransferMarktCheck = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WSTeamCheck = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SofascoreUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team_Category_Mapping",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    SeasonStageId = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: true),
                    LastRank = table.Column<int>(type: "integer", nullable: true),
                    W = table.Column<int>(type: "integer", nullable: false),
                    D = table.Column<int>(type: "integer", nullable: true),
                    L = table.Column<int>(type: "integer", nullable: true),
                    GF = table.Column<int>(type: "integer", nullable: true),
                    GA = table.Column<int>(type: "integer", nullable: true),
                    GD = table.Column<int>(type: "integer", nullable: true),
                    Pts = table.Column<int>(type: "integer", nullable: true),
                    P = table.Column<int>(type: "integer", nullable: true),
                    Forms = table.Column<string>(type: "text", nullable: true),
                    HomeWinStreak = table.Column<int>(type: "integer", nullable: false),
                    AwayWinStreak = table.Column<int>(type: "integer", nullable: false),
                    HomeUndefeated = table.Column<int>(type: "integer", nullable: false),
                    AwayUndefeated = table.Column<int>(type: "integer", nullable: false),
                    HomeCleanSheet = table.Column<int>(type: "integer", nullable: false),
                    AwayCleanSheet = table.Column<int>(type: "integer", nullable: false),
                    HomeFailedToScore = table.Column<int>(type: "integer", nullable: false),
                    AwayFailedToScore = table.Column<int>(type: "integer", nullable: false),
                    HomeLoseStreak = table.Column<int>(type: "integer", nullable: false),
                    AwayLoseStreak = table.Column<int>(type: "integer", nullable: false),
                    WinStreak = table.Column<int>(type: "integer", nullable: false),
                    LoseStreak = table.Column<int>(type: "integer", nullable: false),
                    Undefeated = table.Column<int>(type: "integer", nullable: false),
                    FailedToScore = table.Column<int>(type: "integer", nullable: false),
                    HomeScore = table.Column<int>(type: "integer", nullable: false),
                    AwayScore = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Category_Mapping", x => new { x.TeamId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Team_Category_Mapping_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team_Player_Mapping",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    SeasonStageId = table.Column<int>(type: "integer", nullable: false),
                    ShirtNumber = table.Column<int>(type: "integer", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: false),
                    GoalShirtNumber = table.Column<int>(type: "integer", nullable: true),
                    SquawkaShirtNumber = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Player_Mapping", x => new { x.TeamId, x.PlayerId, x.SeasonStageId });
                    table.ForeignKey(
                        name: "FK_Team_Player_Mapping_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Player_Mapping_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_Name",
                table: "Player",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Name",
                table: "Team",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Player_Mapping_PlayerId",
                table: "Team_Player_Mapping",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team_Category_Mapping");

            migrationBuilder.DropTable(
                name: "Team_Player_Mapping");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
