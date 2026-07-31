namespace eSport.UI.Dto.Catalog;

public record CategoryDto : BaseEntity
{
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
