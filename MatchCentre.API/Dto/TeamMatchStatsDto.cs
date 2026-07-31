namespace eSport.MatchCentre.API.Dto;

public class TeamMatchStatsDto
{
    public double? Possession { get; set; }
    public double? Rating { get; set; }
    public int? YellowCards { get; set; }
    public int? RedCards { get; set; }
    public int? Goals { get; set; }
    public int? ShotsOnTarget { get; set; }
    public int? Fouls { get; set; }
    public int? AerialsWon { get; set; }
    public double? PassAccuracy { get; set; } // PS
    public int? Assists { get; set; }
    public int? Offsides { get; set; }
    public int? Dribbles { get; set; }
    public int? Fouled { get; set; }
    public int? Dispossessed { get; set; } // Disp
    public int? Tackles { get; set; }
    public int? Interceptions { get; set; }
    public int? BlockedShots { get; set; }
    public int? Clearances { get; set; }
    public string? ShotsGraph { get; set; }
    public string? Formation { get; set; }
}
