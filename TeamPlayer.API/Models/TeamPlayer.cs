using System.ComponentModel.DataAnnotations;

namespace eSport.TeamPlayer.API.Models;

public class TeamPlayer : Entity<int>
{
    [Required] public int TeamId { get; set; } // int
    [Required] public int PlayerId { get; set; } // int
    public int? ShirtNumber { get; set; } // int
    [Required] public required string Position { get; set; } // nvarchar(200)
    [Required] public int SeasonStageId { get; set; } // int
    public int? GoalShirtNumber { get; set; } // int
    public int? SquawkaShirtNumber { get; set; } // int
    [Required] public bool Status { get; set; } // bit

    public Team Team { get; set; } = null!;
    public Player Player { get; set; } = null!;
}
