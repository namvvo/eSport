namespace eSport.Catalog.API.GraphQL.Dto;

public record CategoryModel 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    //public string CountryName { get; set; }
    public string SeName { get; set; } = string.Empty;
    //public string CountryCss { get; set; }
    public bool IsActive { get; set; }
    public bool IsTournament { get; set; }
    public bool IsSelected { get; set; }
    public List<SeasonStageModel> SeasonStages { get; set; } = new();
    public CountryModel? Country { get; set; } = new();
    //public PictureModel Banner { get; set; } = new();
}
public record CategorySimpleModel 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string SeName { get; set; } = string.Empty;
    /// <summary>
    /// league without url will be disabled on client Menu
    /// </summary>
    public string CurrentSeasonUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsTournament { get; set; }
    public bool IsSelected { get; set; }
    public bool IsData { get; set; }

    public string Flag { get; set; } = string.Empty;
    public int CountryId { get; set; }

}
