using OpenTelemetry.Trace;
using System.ComponentModel.DataAnnotations;

namespace eSport.Catalog.API.Models;

public partial class Stage : Entity<int>
{

    [Required]
    public required string Name { get; set; } // nvarchar(100)
    public string? C1WhoscoredName { get; set; } // nvarchar(100)
    public string? C3WhoscoredName { get; set; } // nvarchar(100)
    public string? EuroWhoscoredName { get; set; } // nvarchar(100)
    public string? WCWhoscoredName { get; set; } // nvarchar(100)
    public string? C1SofascoreName { get; set; } // nvarchar(100)
    public string? C3SofascoreName { get; set; } // nvarchar(100)
    public string? EuroSofascoreName { get; set; } // nvarchar(100)
    public string? WCSofascoreName { get; set; } // nvarchar(100)
    public int? ParentId { get; set; } // int
    public bool? GroupStage { get; set; } // bit
    /// <summary>
    /// to know how to get the latest stage of the current season
    /// 1 domestic => order by season year2
    ///tournament=> order by stage.displayOrder asc
    ///where status = 1 && !iscomplete &&
    /// </summary>
    [Required]
    public int DisplayOrder { get; set; } // int
    public int? NoOfMatches { get; set; } // int
    [Required]
    public int Round { get; set; } // int
    [Required]
    public bool Display { get; set; } // bit
    //navigation properties
    public ICollection<Season> Seasons { get; set; } = [];// skip navigation
    public ICollection<Category> Categories { get; set; } = [];  // skip navigation
    public ICollection<CategoryStage> CategoryStages { get; set; } = [];
    public ICollection<SeasonStage> SeasonStages { get; set; } = [];
}
