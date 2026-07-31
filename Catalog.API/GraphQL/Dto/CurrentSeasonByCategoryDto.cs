namespace eSport.Catalog.API.GraphQL.Dto;

public record CurrentSeasonByCategoryDto
{
    public SeasonStageDto CurrentSeasonStage { get; set; } = new SeasonStageDto();
}
