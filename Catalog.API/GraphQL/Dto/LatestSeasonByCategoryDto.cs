using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eSport.Catalog.API.GraphQL.Dto
{
    public record LatestSeasonByCategoryDto
    {
        public SeasonStageDto CurrentSeasonStage { get; set; } = new();


        public IList<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public IList<StageDto> Stages { get; set; } = new List<StageDto>();
        public IList<SeasonDto> Seasons { get; set; } = new List<SeasonDto>();
        public IList<TeamSimpleDto> Teams { get; set; } = new List<TeamSimpleDto>();


    }
   
    
}
