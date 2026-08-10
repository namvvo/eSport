namespace eSport.MatchCentre.API.Dto.TeamPlayer;

internal sealed record PlayerStatQueryDto
{
    public FixtureStat Stat { get; init; } = default!;

    public Fixture Fixture { get; init; } = default!;

    public FixtureCategory FixtureCategory { get; init; } = default!;
}
