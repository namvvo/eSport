using System.ComponentModel.DataAnnotations;

namespace eSport.Catalog.API.Models;

public class Season : Entity<int>
{

    [Required]
    public required string Year { get; set; } = string.Empty; // varchar(50)
    public int? DisplayOrder { get; set; } // int
    public int Year2 { get; set; } // int
    [Required] public bool Status { get; set; } // bit

    public ICollection<Stage> Stages { get; } = [];// skip navigation
    public ICollection<SeasonStage> SeasonStages { get; } = [];// navigation
}