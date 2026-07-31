namespace eSport.MatchCentre.API.GraphQL.Dto.Fixtures;

public record LeagueInfo
{
    public int CategoryId { get; set; }

    public string Country { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public string CountryCss { get; set; } = string.Empty;
    public string SeName { get; set; } = string.Empty;
}