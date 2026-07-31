namespace eSport.MatchCentre.API.Dto;

public class FixtureSearchResultDto
{
    public int Id { get; set; }
    public int HomeId { get; set; }
    public int AwayId { get; set; }
    public DateTime Time { get; set; }
    public int? Round { get; set; }
    public bool IsAwarded { get; set; }
    public int? Year { get; set; }
    public bool IsComplete { get; set; }
    public bool HasVideos { get; set; }


    public int SeasonStageId { get; set; }
    public string? HalfTime { get; set; }
    public string? FullTime { get; set; }
    public string? ExtraTime { get; set; }
    public string? PK { get; set; }
    public string? TimeElapsed { get; set; }
    public string? LiveScore { get; set; }
    public string? AutoUrl { get; set; }

    // Gom thành 2 Complex Properties đại diện cho Home và Away
    public TeamMatchStatsDto HomeStats { get; set; } = new();
    public TeamMatchStatsDto AwayStats { get; set; } = new();
}