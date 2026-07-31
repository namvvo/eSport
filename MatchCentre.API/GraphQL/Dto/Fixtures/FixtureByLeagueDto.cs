namespace eSport.MatchCentre.API.GraphQL.Dto.Fixtures;

public record FixtureByLeagueDto
{
    public int Id { get; set; }
    public TeamDto Home { get; set; } = new();
    public TeamDto Away { get; set; } = new();
    //public string ShortHome { get; set; }
    //public string ShortAway { get; set; }
    //public string Away { get; set; }
    //public int HomeId { get; set; }
    //public string HomeLogo { get; set; }

    //public int AwayId { get; set; }
    //public string AwayLogo { get; set; } 
    //public string HomeSeName { get; set; }
    //public string AwaySeName { get; set; }
    public bool ShowStats { get; set; } = false;
    public string League { get; set; } = string.Empty;
    public int Round { get; set; } //for domestic leagues
    public string? ParentCategoryName { get; set; } = string.Empty; // vd: bóng đá Nga có giải premier league trùng với EPL

    public bool IsComplete { get; set; }
    public bool IsAwarded { get; set; }
    public bool NotStarted
    {
        get
        {
            return (DateTime.Now - Convert.ToDateTime(Time)).TotalSeconds < 0;
        }
    }
    public bool HasVideos { get; set; }
    public int LeagueId { get; set; }
    public string LeagueLogo { get; set; } = string.Empty;
    public string LeagueCss { get; set; } = string.Empty;  //category css

    public string Time2 { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string ShortDate { get; set; } = string.Empty;
    public string TimeElapsed { get; set; } = string.Empty;
    public string Score { get; set; } = string.Empty;
    public int Comments { get; set; }
    public string MatchTitle { get; set; } = string.Empty;
    public bool CurrentItem { get; set; }
    public bool IsLive { get; set; }
    public int HomeYellowCards { get; set; }
    public int HomeRedCards { get; set; }
    public int AwayYellowCards { get; set; }
    public int AwayRedCards { get; set; }
    public int AwayGoals { get; set; } = 0;
    public bool Topmatch { get; set; }
    public string CssScoreStatus { get; set; } = string.Empty;
    public string ScoreStatus { get; set; } = string.Empty;
    public Tournament? Tournament { get; set; } = new();
    //public string TournamentLogo { get; set; } = string.Empty;
    //public string TournamentSeName { get; set; } = string.Empty;
    public string LiveOdds { get; set; } = string.Empty;
    public string HomeCoach { get; set; } = string.Empty;
    public int HomeGoals { get; set; } = 0;
    public string AwayCoach { get; set; } = string.Empty;
    public string Stadium { get; set; } = string.Empty;
    public string Referee { get; set; } = string.Empty;
    public string Weather { get; set; } = string.Empty;
    public string Attendance { get; set; } = string.Empty;
    public int SeasonStageId { get; set; }
    public int Status { get; set; }
    public MatchTime MatchTime { get; set; } = new MatchTime();
}
public record MatchTime
{
    public string HalfTime { get; set; } = string.Empty; // varchar(10)
    public string FullTime { get; set; } = string.Empty; // varchar(10)
    public string ExtraTime { get; set; } = string.Empty;// varchar(10)
    public string PK { get; set; } = string.Empty;// varchar(10)
}
