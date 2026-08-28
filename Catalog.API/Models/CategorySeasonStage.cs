using System.ComponentModel.DataAnnotations;

namespace eSport.Catalog.API.Models;

public class CategorySeasonStage
{
    [Required] public int SeasonStageId { get; set; } // int
    [Required] public int CategoryId { get; set; } // int
    /// <summary>
    /// rounds that have been completed, used to determine the current round 
    /// => lookup on fixture.round for the current round/start/end date of the round
    /// </summary>
    [Required] public int CompleteRound { get; set; } // int
    /// <summary>
    /// number of rounds per league
    /// </summary>
    public int LeagueRound { get; set; } = 0;

    
    /// <summary>
    /// stard/end of the season stage, used to determine the duration of 1 category/league. e.g bundesliga, epl...
    /// </summary>
    [Required] public int FromMonth { get; set; } // int
    [Required] public int ToMonth { get; set; } // int
    [Required] public int ToDateUseYearPart { get; set; } // int
    /// <summary>
    /// fixed number
    /// </summary>
    [Required] public int FixtureCount { get; set; } // int
    /// <summary>
    /// based on real time fixtures have progressed
    /// </summary>
    public FixtureRound CurrentRound { get; set; } = new();//complextype
    public SeasonStage SeasonStage { get; set; } = null!;
    public Category Category { get; set; } = null!;

}
