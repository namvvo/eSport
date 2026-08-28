namespace eSport.MatchCentre.API.Dto.League;

public sealed record LeaguePlayerAggregateDto
{
    public int PlayerId { get; set; }
    public int TeamOwnerId { get; set; }
    public int CategoryId { get; set; }
    public double Rating { get; set; }
    public int Apps { get; set; }

    public string? CountryCSS { get; set; }
    public string? CategoryName { get; set; }
    public int? LeaguePictureId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string? TeamPosition { get; set; }

    public int MinPlayed { get; set; }
    public double MarketValue { get; set; }
    public double CustomScore { get; set; }
    public int TeamGoals { get; set; }
    public GeneralPlayerStats General { get; init; } = default!;

    public AttackingStats Attacking { get; init; } = default!;

    public PassingStats Passing { get; init; } = default!;

    public DefendingStats Defending { get; init; } = default!;

    public GoalkeepingStats Goalkeeping { get; init; } = default!;
}
