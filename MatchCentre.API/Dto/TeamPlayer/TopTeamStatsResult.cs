namespace eSport.MatchCentre.API.Dto;

public class TopTeamStatDto
{
    public virtual List<StatInfo> Possession { get; set; } = [];
    public virtual List<StatInfo> Aggression { get; set; } = [];
    public virtual List<StatInfo> AerialDuels { get; set; } = [];
    public virtual List<StatInfo> ShotsPerGame { get; set; } = [];
    public virtual List<StatInfo> PassAccuracy { get; set; } = [];
    public virtual List<StatInfo> Ratings { get; set; } = [];
}
public sealed class StatInfo
{
    public string? Name { get; set; }
    public string? Info { get; set; }
    public string? InfoWithDecimal { get; set; }
    public string? Info2 { get; set; }
    public string? SeName { get; set; }
}
