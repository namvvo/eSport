using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eSport.MatchCentre.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fixture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SofascoreId = table.Column<int>(type: "integer", nullable: false),
                    HomeId = table.Column<int>(type: "integer", nullable: false),
                    AwayId = table.Column<int>(type: "integer", nullable: false),
                    Machine = table.Column<string>(type: "text", nullable: true),
                    IsFriendly = table.Column<bool>(type: "boolean", nullable: true),
                    Weather = table.Column<string>(type: "text", nullable: true),
                    Stadium = table.Column<string>(type: "text", nullable: true),
                    Attendance = table.Column<int>(type: "integer", nullable: true),
                    Referee = table.Column<string>(type: "text", nullable: true),
                    RefereeStats = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AutoTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SeasonStageId = table.Column<int>(type: "integer", nullable: false),
                    HalfTime = table.Column<string>(type: "text", nullable: true),
                    FullTime = table.Column<string>(type: "text", nullable: true),
                    ExtraTime = table.Column<string>(type: "text", nullable: true),
                    PK = table.Column<string>(type: "text", nullable: true),
                    TimeElapsed = table.Column<string>(type: "text", nullable: true),
                    LiveScore = table.Column<string>(type: "text", nullable: true),
                    AutoUrl = table.Column<string>(type: "text", nullable: true),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    AutoComplete = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    GoalUrl = table.Column<string>(type: "text", nullable: true),
                    IsScraping = table.Column<bool>(type: "boolean", nullable: false),
                    Incidents = table.Column<string>(type: "text", nullable: true),
                    UpdatedStat = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedFixtureStats = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedMissingPlayers = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedLiveMatch = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedProbableLineup = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedProbableLineup404 = table.Column<bool>(type: "boolean", nullable: true),
                    UpdatedComment = table.Column<bool>(type: "boolean", nullable: false),
                    RunSquawkaLive = table.Column<bool>(type: "boolean", nullable: true),
                    RunGoalLive = table.Column<bool>(type: "boolean", nullable: true),
                    ProbableLineup = table.Column<string>(type: "text", nullable: true),
                    HasVideos = table.Column<bool>(type: "boolean", nullable: false),
                    LiveTV = table.Column<string>(type: "text", nullable: true),
                    MinuteExpanded = table.Column<int>(type: "integer", nullable: true),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    IsAwarded = table.Column<bool>(type: "boolean", nullable: false),
                    Away_AerielWon = table.Column<double>(type: "double precision", nullable: false),
                    Away_AggressionR = table.Column<int>(type: "integer", nullable: false),
                    Away_AggressionY = table.Column<int>(type: "integer", nullable: false),
                    Away_Coach = table.Column<string>(type: "text", nullable: true),
                    Away_Corner = table.Column<int>(type: "integer", nullable: false),
                    Away_Formation = table.Column<string>(type: "text", nullable: true),
                    Away_MissingPlayers = table.Column<string>(type: "text", nullable: true),
                    Away_PassAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    Away_Possession = table.Column<double>(type: "double precision", nullable: false),
                    Away_Rating = table.Column<double>(type: "double precision", nullable: false),
                    Away_Shots = table.Column<int>(type: "integer", nullable: false),
                    Away_ShotsGraph = table.Column<string>(type: "text", nullable: true),
                    Away_ShotsOnTarget = table.Column<int>(type: "integer", nullable: false),
                    Away_ThrowIns = table.Column<int>(type: "integer", nullable: false),
                    Home_AerielWon = table.Column<double>(type: "double precision", nullable: false),
                    Home_AggressionR = table.Column<int>(type: "integer", nullable: false),
                    Home_AggressionY = table.Column<int>(type: "integer", nullable: false),
                    Home_Coach = table.Column<string>(type: "text", nullable: true),
                    Home_Corner = table.Column<int>(type: "integer", nullable: false),
                    Home_Formation = table.Column<string>(type: "text", nullable: true),
                    Home_MissingPlayers = table.Column<string>(type: "text", nullable: true),
                    Home_PassAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    Home_Possession = table.Column<double>(type: "double precision", nullable: false),
                    Home_Rating = table.Column<double>(type: "double precision", nullable: false),
                    Home_Shots = table.Column<int>(type: "integer", nullable: false),
                    Home_ShotsGraph = table.Column<string>(type: "text", nullable: true),
                    Home_ShotsOnTarget = table.Column<int>(type: "integer", nullable: false),
                    Home_ThrowIns = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankingTemplate",
                columns: table => new
                {
                    SeasonStageId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Relegation = table.Column<int>(type: "integer", nullable: false),
                    RelegationPlayOff = table.Column<int>(type: "integer", nullable: false),
                    UefaC1 = table.Column<int>(type: "integer", nullable: false),
                    UefaC1Qualifiers = table.Column<int>(type: "integer", nullable: false),
                    EuropaLeagueQualifiers = table.Column<int>(type: "integer", nullable: false),
                    EuropaLeague = table.Column<int>(type: "integer", nullable: false),
                    EuropaLeagueSpots = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingTemplate", x => new { x.CategoryId, x.SeasonStageId });
                });

            migrationBuilder.CreateTable(
                name: "Fixture_Category_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    FixtureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixture_Category_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixture_Category_Mapping_Fixture_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixtureComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FixtureId = table.Column<int>(type: "integer", nullable: false),
                    Min = table.Column<string>(type: "text", nullable: false),
                    Team = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixtureComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixtureComment_Fixture_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixtureStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FixtureId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: true),
                    PlayablePositions = table.Column<string>(type: "text", nullable: true),
                    XY = table.Column<string>(type: "text", nullable: true),
                    WhoscoredFormationPlace = table.Column<int>(type: "integer", nullable: true),
                    ShirtNumber = table.Column<int>(type: "integer", nullable: true),
                    TeamOwnerId = table.Column<int>(type: "integer", nullable: false),
                    SubInMinute = table.Column<int>(type: "integer", nullable: false),
                    SubOutMinute = table.Column<int>(type: "integer", nullable: false),
                    MinPlayed = table.Column<int>(type: "integer", nullable: false),
                    Shots = table.Column<int>(type: "integer", nullable: false),
                    ShotsOnTarget = table.Column<int>(type: "integer", nullable: false),
                    ShotsOffTarget = table.Column<int>(type: "integer", nullable: false),
                    ShotsBlocked = table.Column<int>(type: "integer", nullable: false),
                    BigChanceCreated = table.Column<int>(type: "integer", nullable: false),
                    BigChanceMissed = table.Column<int>(type: "integer", nullable: false),
                    Dribbles = table.Column<int>(type: "integer", nullable: false),
                    DribblesWon = table.Column<int>(type: "integer", nullable: false),
                    DribblesPast = table.Column<int>(type: "integer", nullable: false),
                    DuelWon = table.Column<int>(type: "integer", nullable: false),
                    DuelLost = table.Column<int>(type: "integer", nullable: false),
                    Fouled = table.Column<int>(type: "integer", nullable: false),
                    Fouls = table.Column<int>(type: "integer", nullable: false),
                    Offsided = table.Column<int>(type: "integer", nullable: false),
                    PenaltyConceded = table.Column<int>(type: "integer", nullable: false),
                    Dispossessed = table.Column<int>(type: "integer", nullable: false),
                    UnsTouches = table.Column<int>(type: "integer", nullable: false),
                    KeyPasses = table.Column<int>(type: "integer", nullable: false),
                    AccPasses = table.Column<int>(type: "integer", nullable: false),
                    Passes = table.Column<int>(type: "integer", nullable: false),
                    Crosses = table.Column<int>(type: "integer", nullable: false),
                    AccCrosses = table.Column<double>(type: "double precision", nullable: false),
                    LongBall = table.Column<int>(type: "integer", nullable: false),
                    AccLB = table.Column<double>(type: "double precision", nullable: false),
                    ThroughBall = table.Column<int>(type: "integer", nullable: false),
                    AccThB = table.Column<double>(type: "double precision", nullable: false),
                    TotalTackles = table.Column<int>(type: "integer", nullable: false),
                    LastManTackle = table.Column<int>(type: "integer", nullable: false),
                    Interceptions = table.Column<int>(type: "integer", nullable: false),
                    PossessionLost = table.Column<int>(type: "integer", nullable: false),
                    Clearances = table.Column<int>(type: "integer", nullable: false),
                    BlockedShots = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    Motm = table.Column<bool>(type: "boolean", nullable: false),
                    KeyEvents = table.Column<string>(type: "text", nullable: true),
                    AerialWon = table.Column<int>(type: "integer", nullable: false),
                    AerialLost = table.Column<int>(type: "integer", nullable: false),
                    GroundDuelWon = table.Column<int>(type: "integer", nullable: false),
                    GroundDuelLost = table.Column<int>(type: "integer", nullable: false),
                    YellowCard = table.Column<int>(type: "integer", nullable: false),
                    YellowRed = table.Column<int>(type: "integer", nullable: false),
                    RedCard = table.Column<int>(type: "integer", nullable: false),
                    Assist = table.Column<int>(type: "integer", nullable: false),
                    NoMatches = table.Column<int>(type: "integer", nullable: false),
                    Goal = table.Column<int>(type: "integer", nullable: false),
                    OwnGoal = table.Column<int>(type: "integer", nullable: false),
                    Touches = table.Column<int>(type: "integer", nullable: false),
                    PenGoal = table.Column<int>(type: "integer", nullable: false),
                    PenWon = table.Column<int>(type: "integer", nullable: false),
                    Error2Goal = table.Column<int>(type: "integer", nullable: false),
                    ClearanceOffline = table.Column<int>(type: "integer", nullable: false),
                    ShotOnPost = table.Column<int>(type: "integer", nullable: false),
                    PKMissed = table.Column<int>(type: "integer", nullable: false),
                    PKShootoutScored = table.Column<int>(type: "integer", nullable: false),
                    PKShootoutMissed = table.Column<int>(type: "integer", nullable: false),
                    PKShootoutSaved = table.Column<int>(type: "integer", nullable: false),
                    ThrowIns = table.Column<int>(type: "integer", nullable: false),
                    GKSaves = table.Column<int>(type: "integer", nullable: false),
                    GKCatch = table.Column<int>(type: "integer", nullable: false),
                    GKPunch = table.Column<int>(type: "integer", nullable: false),
                    GKClearance = table.Column<int>(type: "integer", nullable: false),
                    GKTotalSweeper = table.Column<int>(type: "integer", nullable: false),
                    GKErrorLeadToAShot = table.Column<int>(type: "integer", nullable: false),
                    GKSweeper = table.Column<int>(type: "integer", nullable: false),
                    GKPenSaves = table.Column<int>(type: "integer", nullable: false),
                    IsCaptain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixtureStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixtureStats_Fixture_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixture_Category_Mapping_FixtureId",
                table: "Fixture_Category_Mapping",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureComment_FixtureId",
                table: "FixtureComment",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureStats_FixtureId",
                table: "FixtureStats",
                column: "FixtureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixture_Category_Mapping");

            migrationBuilder.DropTable(
                name: "FixtureComment");

            migrationBuilder.DropTable(
                name: "FixtureStats");

            migrationBuilder.DropTable(
                name: "RankingTemplate");

            migrationBuilder.DropTable(
                name: "Fixture");
        }
    }
}
