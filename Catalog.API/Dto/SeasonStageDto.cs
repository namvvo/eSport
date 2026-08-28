namespace eSport.Catalog.API.Dto;

public record SeasonStageDto
{
    public int SeasonStageId { get; set; }
    public string Year { get; set; } = string.Empty;
    public int Year2 { get; set; } = default!;
    public int StageId { get; set; }
    public int SeasonId { get; set; }
    public string Stage { get; set; } = string.Empty;
    //public CategoryModel Category { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public TeamSimpleDto Team { get; set; } = new TeamSimpleDto();
    public IList<DateTime> SeasonDateLimit { get; set; } = [];
}
public record SeasonDto 
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Year { get; set; } = default!;
    public int Year2 { get; set; } = default!;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
public record StageDto  
{
    public int Id { get; set; }
    public required string Name { get; set; }
    //public int Round { get; set; }


    public bool IsActive { get; set; }
    public bool Display { get; set; }
}
