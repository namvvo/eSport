namespace eSport.MatchCentre.API.Dto.Stats;

public sealed record GoalkeepingStats
{
    public double Saves { get; init; }
    public double CleanSheets { get; init; }
    public double GoalsConceded { get; init; }
    public double PenSaves { get; init; }
}
