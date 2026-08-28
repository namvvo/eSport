namespace eSport.TeamPlayer.API.Models;
// Join table — chỉ lưu CategoryId, không có nav property sang Category
public class TeamCategory
{
    public int CategoryId { get; set; } // int
    public int TeamId { get; set; } // int
    public int TeamGoals { get; set; } // int
    public int SeasonStageId { get; set; } // int
    public int? Rank { get; set; } // int
    public int? LastRank { get; set; } // int
    public int W { get; set; } // int
    public int? D { get; set; } // int
    public int? L { get; set; } // int
    public int? GF { get; set; } // int
    public int? GA { get; set; } // int
    public int? GD { get; set; } // int
    public int? Pts { get; set; } // int
    public int? P { get; set; } // int
    public string? Forms { get; set; } // varchar(max)
    public int HomeWinStreak { get; set; } // int
    public int AwayWinStreak { get; set; } // int
    public int HomeUndefeated { get; set; } // int
    public int AwayUndefeated { get; set; } // int
    public int HomeCleanSheet { get; set; } // int
    public int AwayCleanSheet { get; set; } // int
    public int HomeFailedToScore { get; set; } // int
    public int AwayFailedToScore { get; set; } // int
    public int HomeLoseStreak { get; set; } // int
    public int AwayLoseStreak { get; set; } // int
    public int WinStreak { get; set; } // int
    public int LoseStreak { get; set; } // int
    public int Undefeated { get; set; } // int
    public int FailedToScore { get; set; } // int
    public int HomeScore { get; set; } // int
    public int AwayScore { get; set; } // int

    public Team Team { get; set; } = null!;
}
