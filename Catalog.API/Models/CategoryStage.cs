using System.ComponentModel.DataAnnotations;

namespace eSport.Catalog.API.Models;

public class CategoryStage : Entity<int>
{
    [Required] public int StageId { get; set; } // int
    [Required] public int CategoryId { get; set; } // int
    [Required] public int FromMonth { get; set; } // int
    [Required] public int ToMonth { get; set; } // int
    [Required] public int FromDateUseYearPart { get; set; } // int
    [Required] public int ToDateUseYearPart { get; set; } // int
    [Required] public int FixtureCount { get; set; } // int

    public Stage Stage { get; set; } = null!;
    public Category Category { get; set; } = null!;

}
