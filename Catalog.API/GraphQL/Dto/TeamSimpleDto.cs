namespace eSport.Catalog.API.GraphQL.Dto;

public record TeamSimpleDto
{
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    //public PictureModel TeamLogo { get; set; } = new PictureModel();
    //public string SeName { get; set; }
    public int SeasonStageId { get; set; }
}
