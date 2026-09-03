using System.ComponentModel.DataAnnotations;

namespace eSport.TeamPlayer.API.Models;

public class Player : Entity<int>, IAggregateRoot
{
    [Required]
    public required string Name { get; set; } // nvarchar(100)
    public string? ShortName { get; set; } // nvarchar(50)
    public DateTime? Birthdate { get; set; } // datetime
    [Required]
    public int CountryId { get; set; } // int
    public int? CountryId2 { get; set; } // int
    public string? Height { get; set; } // nchar(10)
    public string? Weight { get; set; } // nchar(10)
    //[Required]
    //public int PictureId { get; set; } // int
    // Legacy source URL retained during Media.API migration. It is not a cross-service relation.
    public string? PictureUrl { get; set; }
    public int? ImageMediaId { get; set; }
    public int? WhoscoredPlayerId { get; set; } // int
    public int? SofascorePlayerId { get; set; } // int
    public int? SquawkaPlayerId { get; set; } // int
    public int? GoalPlayerId { get; set; } // int
    public int? InternationalCaps { get; set; } // int
    public string? Url { get; set; } // varchar(max)
    public string? TransferMkName { get; set; } // nvarchar(50)
    public string? TeamPosition { get; set; } // nvarchar(200)
    [Required]
    public decimal PercentageGainInLast6Rounds { get; set; } // decimal(18, 0)
    public int? FantasyOwner { get; set; } // int
    public int? AttackIndexInLast6Rounds { get; set; } // int
    public int? DefenseIndexInLast6Rounds { get; set; } // int
    public int? MarketValue { get; set; } // int
    public string? PreferredFoot { get; set; } // nvarchar(10)
    public string? Slug { get; set; } // varchar(50)
    public string? SkillSets { get; set; } // varchar(max)
    public DateTime? UpdatedDate { get; set; } // datetime

    
    public ICollection<Team> Teams { get; } = [];
    public ICollection<TeamPlayer> TeamPlayers { get; } = [];
}
