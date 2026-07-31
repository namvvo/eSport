namespace eSport.MatchCentre.API.Dto.TeamPlayer;

public sealed class TeamStatDto
{
    public int P { get; set; }
    public int W { get; set; }
    public int D { get; set; }
    public int L { get; set; }
    public int GF { get; set; } // goal for
    public int GA { get; set; }
    public string? GD { get; set; }
    public string? Forms { get; set; }
    public string? Progress { get; set; }
    [ID]
    public int TeamId { get; set; }
    public string? TeamName { get; set; }
    public string? TeamShortName { get; set; }
    public string? TeamSeName { get; set; }
    public double TeamPossession { get; set; }
    public int Pts { get; set; }
    public int Rank { get; set; }
    public int LastRank { get; set; }
    public double ShotsPerGame { get; set; }
    public double PassAcc { get; set; }
    public double Ratings { get; set; }
    public double AggressionR { get; set; }
    public double AggressionY { get; set; }
    public double AerialWonPS { get; set; }
}
