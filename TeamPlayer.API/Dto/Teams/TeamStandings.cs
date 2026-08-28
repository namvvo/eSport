namespace eSport.TeamPlayer.API.Dto.Teams;

public record TeamStandingModel
{
    public RankRuleModel RankRule { get; set; } = new();
    public IList<TeamStatDto> TeamStandings { get; set; } = new List<TeamStatDto>();
    //public StageModel StageModel { get; set; } = new StageModel();
    public bool IsTournament { get; set; }
    public bool IsComplete { get; set; }
}
public record RankRuleModel
{
    //public int? UefaC1 { get; set; }
    //public int? UefaC1Qualifiers { get; set; }
    //public int? EuropaLeague { get; set; }
    //public int? Relegation { get; set; }
    //public int? RelegationPlayOff { get; set; }


    public int? Group1 { get; set; }
    public int? Group2 { get; set; }
    public int? Group3 { get; set; }
    public int? Group4 { get; set; }
    public int? Group5 { get; set; }
    public int? Group6 { get; set; }

}