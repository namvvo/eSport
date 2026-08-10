using eSport.TeamPlayer.API.Dto.Teams;

namespace eSport.TeamPlayer.API.Dto.Leagues
{
    public class PlayerLeagueInfo
    {
        public int PlayerId { get; set; }

        public string Name { get; set; } = "";

        public DateOnly BirthDate { get; set; }

        public decimal MarketValue { get; set; }

        public string TeamPosition { get; set; } = "";

        public int ShirtNumber { get; set; }

        public TeamDTO Team { get; set; } = default!;
    }
}
