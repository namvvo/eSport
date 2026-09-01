using System.ComponentModel.DataAnnotations;

namespace eSport.TeamPlayer.API.Models;

public class Team : Entity<int>, IAggregateRoot
{
    [Required]
    public required string Name { get; set; } // nvarchar(50)
    [Required]
    public required string UefaName { get; set; } // nvarchar(50)
    [Required]
    public int UefaRanking { get; set; } // int
    [Required] public int Fame { get; set; } = 0;// int
    public int? SofascoreId { get; set; } // int
      
    [Required]
    public required string ShortName { get; set; } // nvarchar(50)
    public string? Web { get; set; } // varchar(200)
    public string? Logo { get; set; }
    //[Required] public int Logo { get; set; } // int
    public string? AutoUrl { get; set; } // varchar(200)
    public string? Theme { get; set; } // varchar(50)

    public string? Background { get; set; } // varchar(300)
    public string? TransferMarktUrl { get; set; } // varchar(300)
    public string? TransferMarktName { get; set; } // nvarchar(100)
    public DateTime? TransferMarkUpdateDate { get; set; } // datetime
    public string? Bet188 { get; set; } // nvarchar(100)
    public int? WhoscoredId { get; set; } // int
    public int? CountryId { get; set; } // int
    public DateTime? GoalTeamCheck { get; set; } // datetime
    public DateTime? TransferMarktCheck { get; set; } // datetime
    public DateTime? WSTeamCheck { get; set; } // datetime
    public DateTime? SofascoreUpdate { get; set; } // datetime
    [Required]
    public required string SeName { get; set; } // varchar(50)

    public ICollection<TeamCategory> TeamCategories { get; set; } = [];
    public ICollection<TeamPlayer> TeamPlayers { get; set; } = [];
    public ICollection<Player> Players { get; set; } = [];// skip navigation
}

