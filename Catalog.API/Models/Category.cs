using Pgvector;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eSport.Catalog.API.Models;

public partial class Category : Entity<int>
{
    #region data
    //[PrimaryKey, Identity] public int Id { get; set; } // int
    [Required] public required string Name { get; set; } // nvarchar(400)
    [Required] public int ParentCategoryId { get; set; } // int
    /// <summary>
    /// vd: c1, c3 là Uefa. AFC Cup thu?c Cúp DNA
    /// </summary>
    public string? GroupName { get; set; } // nvarchar(50)
    [Required] public int Rounds { get; set; } // int
    [Required] public required string SeName { get; set; } // nvarchar(100)


    [Required] public int SofaScoreId { get; set; } // int
    public string? CountryCSS { get; set; } // varchar(5)
    [Required] public int CountryId { get; set; } // int
    [Required] public bool Published { get; set; } // bit
    [Required] public int DisplayOrder { get; set; } // int
    /// <summary>
    /// 0 = cup league
    /// </summary>
    public double? Coefficient { get; set; } // float
    [Required] public int FromMonth { get; set; } // int
    [Required] public int ToMonth { get; set; } // int
    public string? MetaKeywords { get; set; } // nvarchar(400)
    public string? MetaDescription { get; set; } // nvarchar(max)
    [Required] public int PictureId { get; set; } // int
    [Required] public int PageSize { get; set; } // int
    [Required] public bool ShowOnHomePage { get; set; } // bit
    [Required] public bool IncludeInTopMenu { get; set; } // bit
    [Required] public bool Deleted { get; set; } // bit
    [Required] public DateTime CreatedOnUtc { get; set; } // datetime
    [Required] public DateTime UpdatedOnUtc { get; set; } // datetime
    public string? Tags { get; set; } // nvarchar(200)
    public int? UefaC1 { get; set; } // int
    public int? UefaC1Qualifiers { get; set; } // int
    public int? EuropaLeagueQualifiers { get; set; } // int
    /// <summary>
    /// 5
    /// </summary>
    public int? EuropaLeague { get; set; } // int
    /// <summary>
    /// 0
    /// </summary>
    public int? Relegation { get; set; } // int
    public int? RelegationPlayOff { get; set; } // int
    public string? Transfermarkt { get; set; } // varchar(300)
    public int? LeagueLogo { get; set; } // int
    public bool? ShowStanding { get; set; } // bit
    [Required] public bool IsTournament { get; set; } // bit
    [Required] public bool IsData { get; set; } // bit

    /// <summary>Optional embedding for the catalog item's description.</summary>
    [GraphQLIgnore]
    [JsonIgnore]
    public Vector? Embedding { get; set; }// Pgvector.EntityFrameworkCore
    #endregion
    //navigation

    public ICollection<Stage> Stages { get; set; } = []; // skip navigation
    public ICollection<CategoryStage> CategoryStages { get; set; } = [];
    public ICollection<CategorySeasonStage> CategorySeasonStages { get; set; } = [];
}
