namespace eSport.UI.Client.Dto;

public record TopTeamStatDto
{
    public virtual List<StatInfo> Possession { get; set; } = [];
    public virtual List<StatInfo> Aggression { get; set; } = [];
    public virtual List<StatInfo> AerialDuels { get; set; } = [];
    public virtual List<StatInfo> ShotsPerGame { get; set; } = [];
    public virtual List<StatInfo> PassAccuracy { get; set; } = [];
    public virtual List<StatInfo> Ratings { get; set; } = [];
}
public record StatInfo
{
    public string Name { get; set; } = string.Empty;
    public string Info { get; set; } = string.Empty;
    public string InfoWithDecimal { get; set; } = string.Empty;
    public string Info2 { get; set; } = string.Empty;
    public string SeName { get; set; } = string.Empty;
}