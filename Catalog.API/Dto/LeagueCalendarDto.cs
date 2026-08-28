namespace eSport.Catalog.API.Dto;

public record LeagueCalendarDto
{
    public int CategoryId { get; set; }
    public RoundOfFixture CurrentRound { get; set; } = new();
    /// <summary>
    /// for domestic league, e.g bundesliga has 34 rounds, epl = 38
    /// </summary>
    public IList<int> Rounds { get; set; } = [];
    public int LeagueEndRound { get; set; }
}
public record RoundOfFixture
{
    public int Round { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}
