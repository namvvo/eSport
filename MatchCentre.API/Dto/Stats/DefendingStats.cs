namespace eSport.MatchCentre.API.Dto.Stats;

public sealed record DefendingStats
{
    public double Tackles { get; set; }
    public double Interceptions { get; set; }
    public double Clearances { get; set; }
    public double Blocks { get; set; }
    public double Fouls { get; set; }

    public int OwnGoals { get; init; }
    public double AerielWon { get; set; }
    public double Dispossessed { get; set; }
}
