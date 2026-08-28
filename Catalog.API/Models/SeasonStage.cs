namespace eSport.Catalog.API.Models;

public class SeasonStage : Entity<int>
{
    public int StageId { get; set; }
    public int SeasonId { get; set; }
    /// <summary>
    /// show how progress of a season 
    /// </summary>
    public bool IsComplete { get; set; }
    /// <summary>
    /// true = ready for auto scrape data
    /// </summary>
    public bool Status { get; set; } // bit

    //public List<Team> Teams { get; } = [];
    public Season Season { get; set; } = null!;

    public Stage Stage { get; set; } = null!;
    public ICollection<CategorySeasonStage> CategorySeasonStages { get; set; } = [];
}
