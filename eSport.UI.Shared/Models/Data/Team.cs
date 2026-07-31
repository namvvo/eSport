
using eSport.UI.Shared.Models.Common;

namespace eSport.UI.Shared.Models.Data
{
    public record TeamOfTheWeekModel
    {
        public IReadOnlyList<TeamOfTheWeekPlayerModel> Players { get; set; } =new List<TeamOfTheWeekPlayerModel>();
        public string Time { get; set; }
    }


    public record TeamOfTheWeekPlayerModel
    {
        public int Id { get; set; }
        public int Apps { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }
        public string? TeamSeName { get; set; }
        public string? SeName { get; set; }
        public string? Rating { get; set; }
        public PictureModel? Logo { get; set; }
        public string? Position { get; set; }

    }
}
