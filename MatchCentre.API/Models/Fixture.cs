using System.ComponentModel.DataAnnotations;

namespace eSport.MatchCentre.API.Models;

public class Fixture : Entity<int>, IAggregateRoot
{
    [Required] public int SofascoreId { get; set; } // int
    [Required] public int HomeId { get; set; } // int
    [Required] public int AwayId { get; set; } // int
    public string? Machine { get; set; } // nchar(10)
    public bool? IsFriendly { get; set; } // bit
  
    public string? Weather { get; set; } // nvarchar(100)
    public string? Stadium { get; set; } // nvarchar(100)
    public int? Attendance { get; set; } // int
    public string? Referee { get; set; } // nvarchar(100)
    public string? RefereeStats { get; set; } // nvarchar(100)
   
    [Required] public DateTime Time { get; set; } // datetime
    public DateTime? CreatedDate { get; set; } // datetime
    public DateTime? AutoTime { get; set; } // datetime
    [Required]
    public int SeasonStageId { get; set; } // int
    public string? HalfTime { get; set; } // varchar(10)
    public string? FullTime { get; set; } // varchar(10)
    public string? ExtraTime { get; set; } // varchar(10)
    public string? PK { get; set; } // varchar(10)
    public string? TimeElapsed { get; set; } // nvarchar(50)
    public string? LiveScore { get; set; } // nchar(20)

    public string? AutoUrl { get; set; } // varchar(200)
    [Required]
    public bool IsComplete { get; set; } // bit
    [Required]
    public bool AutoComplete { get; set; } // bit
    [Required]
    public FixtureStatusEnum Status { get; set; } // int
    public string? GoalUrl { get; set; } // varchar(200)
    [Required]
    public bool IsScraping { get; set; } // bit

   
    public string? Incidents { get; set; } // varchar(max)
    public bool? UpdatedStat { get; set; } // bit
    public bool? UpdatedFixtureStats { get; set; } // bit
    public bool? UpdatedMissingPlayers { get; set; } // bit
    public bool? UpdatedLiveMatch { get; set; } // bit
    public bool? UpdatedProbableLineup { get; set; } // bit
    public bool? UpdatedProbableLineup404 { get; set; } // bit
    [Required]
    public bool UpdatedComment { get; set; } // bit
    public bool? RunSquawkaLive { get; set; } // bit
    public bool? RunGoalLive { get; set; } // bit
    public string? ProbableLineup { get; set; } // nvarchar(max)

    [Required]
    public bool HasVideos { get; set; } // bit
    public string? LiveTV { get; set; } // varchar(max)
    public int? MinuteExpanded { get; set; } // int
    [Required] public int Round { get; set; } // int
    [Required] public bool IsAwarded { get; set; } // bit


    public required Stats Home { get; set; }
    public required Stats Away { get; set; }

    // navigation property
    public ICollection<FixtureCategory> FixtureCategories { get; set; } = [];
    public ICollection<FixtureStat> FixtureStats { get; set; } = [];

    public ICollection<FixtureComment> FixtureComments { get; set; } = [];
}
