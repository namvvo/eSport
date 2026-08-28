
using eSport.UI.Shared.Models.Catalog;

namespace eSport.UI.Shared.Models.Data
{
    public partial class CurrentSeasonModel
    {
        public int SelectedSeasonId { get; set; }
        public List<int> SelectedSeasonYears { get; set; } = new();
        public int SelectedStageId { get; set; }
        public int SelectedRound { get; set; }
        public int TabIndex { get; set; } = -1;
        public int SelectedCatId { get; set; }
        public string SelectedLeague { get; set; } = string.Empty;
        public IList<DateTime> SeasonDateRange { get; set; } = [];

        public LeagueCalendarModel LeagueCalendar { get; set; } = new();
        public LatestSeasonByCategoryModel LatestSeasonsByCategory { get; set; } = new();
        public SeasonStageModel ActiveSeasonStage { get; set; } = new();
        public CategoryModel ActiveCategory { get; set; } = new();
    }
    public partial class LatestSeasonByCategoryModel
    {

        public SeasonStageModel CurrentSeasonStage { get; set; } = new();


        public IList<CategoryModel> Categories { get; set; } = [];
        public IList<StageModel> Stages { get; set; } = [];
        public IList<SeasonModel> Seasons { get; set; } = [];
        public IList<TeamSimpleModel> Teams { get; set; } = [];


    }
    public record LeagueCalendarModel
    {
        public int CategoryId { get; set; }
        public RoundOfFixture CurrentRound { get; set; } = new();

        public IList<int> Rounds { get; set; } = [];
        public int LeagueEndRound { get; set; }
    }
    public class RoundOfFixture
    {
        public int Round { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
    }
}
