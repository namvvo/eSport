namespace eSport.MatchCentre.API.Dto.Stats;

public sealed record PassingStats
{
    public double Passes { get; init; }
    public double PassAccuracy { get; init; }    

    public double Crosses { get; init; }
    public double AccCrosses { get; init; }

    public double LongBalls { get; init; }
    public double ThroughBalls { get; init; }
    public double PSPercentage { get; set; }
    public double AvgP { get; set; }
    public double Touches { get; init; }
}
