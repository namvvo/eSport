using eSport.UI.Shared.Models.Common;

namespace eSport.UI.Shared.Models.Catalog
{
    public record TeamModel : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string Slug { get; set; } = String.Empty;
        public string ShortName { get; set; } = String.Empty;
        public string UefaName { get; set; } = String.Empty;
        public string Theme { get; set; } = String.Empty;
        public string Web { get; set; } = String.Empty;
        public int? Fame { get; set; }
        public int UefaRanking { get; set; }
        public string Background { get; set; } = String.Empty;
        public PictureModel TeamLogo { get; set; } = new();
        public PictureModel CoachAvatar { get; set; } = new();
        public string SeName { get; set; } = string.Empty;
        public int SeasonStageId { get; set; }
        public List<CategoryModel> ParticipatedLeagues { get; set; } = new();
        //public List<CategorySimpleModel> Categories { get; set; } = new List<CategorySimpleModel>();
    }
    public record TeamSimpleModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public PictureModel TeamLogo { get; set; } = new();
        public string SeName { get; set; } = string.Empty;
        public int SeasonStageId { get; set; }

    }
    public record TeamPositionModel
    {
        public string Position { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public int Apps { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
    }

}
