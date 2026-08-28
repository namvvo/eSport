namespace eSport.MatchCentre.API.Dto.Stats;

public sealed record AttackingStats
{
    public int Goals { get; set; }
    public int PenGoals { get; init; }
    public int Assists { get; init; }
    
    public double ShotsPerGame { get; init; }
    public double ShotsOnTarget { get; init; }
    public double KeyPasses { get; init; }
    public double Dribbles { get; init; }
    public double Fouled { get; init; }
    public double Offsides { get; init; }

}
