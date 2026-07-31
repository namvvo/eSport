
using eSport.UI.Shared.Models.Common;

namespace eSport.UI.Shared.Models.Catalog
{
    public record CategoryModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        //public string CountryName { get; set; }
        public string SeName { get; set; } = string.Empty;
        //public string CountryCss { get; set; }
        public bool IsActive { get; set; }
        public bool IsTournament { get; set; }
        public bool IsSelected { get; set; }
        public List<SeasonStageModel> SeasonStages { get; set; } = new();
        public CountryModel? Country { get; set; } 
        public PictureModel Banner { get; set; } = new();
    }
    public record CategorySimpleModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string SeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsTournament { get; set; }
        public bool IsSelected { get; set; }
        public bool IsData { get; set; }

        public string Flag { get; set; } = string.Empty;
        public int CountryId { get; set; }

    }
}