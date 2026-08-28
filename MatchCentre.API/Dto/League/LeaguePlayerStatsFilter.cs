namespace eSport.MatchCentre.API.Dto.League;

public sealed class LeaguePlayerStatsFilter
{
    
    public IList<int> CategoryIds { get; set; } = [];
    public int SeasonStageIds { get; set; } 
    public int TeamId { get; set; } = 0;
    public int FixtureId { get; set; } = 0;
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 50;
    
    public int CountApp { get; set; } = 1;
    public IList<int> PlayerIds { get; set; } = [];
    public string PlayerName { get; set; } = "";
    public DateTime? TimeLimit { get; init; }

    public DateTime? TimeFrom { get; init; }

    public DateTime? TimeTo { get; init; }
    public string TeamPosition { get; set; } = "";

    //public int PriceMin { get; set; } = 0;
    //public int PriceMax { get; set; } = 0;
    public bool OnlyRegisteredPlayers { get; set; } = true;
    public int MinPlayed { get; set; } = 0;
    public int PriceOrder { get; set; } = 0;
   
    public LeaguePlayerOrderBy OrderBy { get; set; } = LeaguePlayerOrderBy.Goals;

    public SortDirection Direction { get; set; } = SortDirection.Desc;

}
