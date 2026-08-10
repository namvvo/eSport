namespace eSport.MatchCentre.API.Dto.TeamPlayer;

public sealed class TeamStatRow
{
    public int TeamId { get; set; }
    public string? SeName { get; set; }
    public double Possession { get; set; }
    public double PassAccuracy { get; set; }
    public double Rating { get; set; }
    public double AerielWonPS { get; set; }
    public int Shots { get; set; }
    public int AggressionY { get; set; } = 0;
    public int AggressionR { get; set; } = 0;
}
