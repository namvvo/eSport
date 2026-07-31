

namespace eSport.MatchCentre.API.GraphQL.Dto;

public record PictureDto : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;

    public string FullSizeImageUrl { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string AlternateText { get; set; } = string.Empty;
}