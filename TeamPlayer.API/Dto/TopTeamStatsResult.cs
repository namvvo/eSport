namespace eSport.MatchCentre.API.Dto;

public class TopTeamStatDto
{
    private IReadOnlyList<StatInfo> _possession { get; set; }
    public virtual IReadOnlyList<StatInfo> Possession
    {
        get { return _possession ?? (_possession = new List<StatInfo>()); }
        protected set { _possession = value; }
    }
    private IReadOnlyList<StatInfo> _aggression { get; set; } 
    public virtual IReadOnlyList<StatInfo> Aggression
    {
        get { return _aggression ?? (_aggression = new List<StatInfo>()); }
        protected set { _aggression = value; }
    }
    private IReadOnlyList<StatInfo> _aerialDuels { get; set; }
    public virtual IReadOnlyList<StatInfo> AerialDuels
    {
        get { return _aerialDuels ?? (_aerialDuels = new List<StatInfo>()); }
        protected set { _aerialDuels = value; }
    }
    private IReadOnlyList<StatInfo> _shotsPerGame { get; set; }
    public virtual IReadOnlyList<StatInfo> ShotsPerGame
    {
        get { return _shotsPerGame ?? (_shotsPerGame = new List<StatInfo>()); }
        protected set { _shotsPerGame = value; }
    }
    private IReadOnlyList<StatInfo> _passAccuracy { get; set; }
    public virtual IReadOnlyList<StatInfo> PassAccuracy
    {
        get { return _passAccuracy ?? (_passAccuracy = new List<StatInfo>()); }
        protected set { _passAccuracy = value; }
    }
    private IReadOnlyList<StatInfo> _ratings { get; set; }
    public virtual IReadOnlyList<StatInfo> Ratings
    {
        get { return _ratings ?? (_ratings = new List<StatInfo>()); }
        protected set { _ratings = value; }
    }
}
public sealed class StatInfo
{
    public string? Name { get; set; }
    public string? Info { get; set; }
    public string? InfoWithDecimal { get; set; }
    public string? Info2 { get; set; }
    public string? SeName { get; set; }
}
