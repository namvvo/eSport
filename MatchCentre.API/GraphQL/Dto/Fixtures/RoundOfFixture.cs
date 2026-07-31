namespace eSport.MatchCentre.API.GraphQL.Dto.Fixtures;

public record RoundOfFixture
{
    public int Round { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
