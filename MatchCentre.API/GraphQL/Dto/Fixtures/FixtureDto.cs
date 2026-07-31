namespace eSport.MatchCentre.API.GraphQL.Dto.Fixtures
{
    public record FixtureDto()
    {
        public FixtureByLeagueDto Fixture { get; set; } = new();
        public LeagueInfo LeagueInfo { get; set; } = new();
        public RoundOfFixture CurrentRound { get; set; } = new();
        // for fixtures by 2 teams of a fixture
        public int HomeId { get; set; }
        public int AwayId { get; set; }
    }

    public record Tournament
    {
        public string Name { get; set; } = string.Empty;
        public string SeName { get; set; } = string.Empty;
        //public PictureModel Logo { get; set; } = new();
        public bool IsTournament { get; set; }
    }
}
