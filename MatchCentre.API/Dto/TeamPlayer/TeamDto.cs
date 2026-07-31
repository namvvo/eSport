using eSport.MatchCentre.API.GraphQL.Dto;

namespace eSport.MatchCentre.API.Dto.TeamPlayer;

public record TeamDto: BaseEntity
{

    public string Name { get; set; } = String.Empty;
    public string Slug { get; set; } = String.Empty;
    public string ShortName { get; set; } = String.Empty;
    public string UefaName { get; set; } = String.Empty;
    public string Theme { get; set; } = String.Empty;
    public string? Web { get; set; }
    public int? Fame { get; set; }
    public int UefaRanking { get; set; }
    public string Background { get; set; } = String.Empty;
    public PictureDto TeamLogo { get; set; } = new();
    public PictureDto CoachAvatar { get; set; } = new();
    public string SeName { get; set; } = string.Empty;
    public int SeasonStageId { get; set; }
    //public List<CategoryStub> ParticipatedLeagues { get; set; } = new();
    //public List<CategorySimpleModel> Categories { get; set; } = new List<CategorySimpleModel>();
}
