namespace eSport.Catalog.API.Services
{
    public interface ISeasonStageService
    {
        Task<SeasonStage?> GetSeasonStageMappingByIdAsync(int seasonStageId, CancellationToken ct, bool loadSeason = false);


        Task<SeasonStage?> GetCurrentSeasonStageByCategoryAsync(int categoryId, CancellationToken ct);
        Task<SeasonStage?> GetCurrentSeasonStageByCategoryInTournamentAsync(int categoryId, CancellationToken ct);

        Task<IList<SeasonStage>> GetSeasonStagesAsync(CancellationToken ct, int seasonId = 0, IList<int> stageIds = null);
        Task<IList<SeasonStage>> GetSeasonStagesAsync(int categoryId, CancellationToken ct, int seasonId = 0, int stageId = 0);
        Task<IEnumerable<Stage>> GetStagesByCategoryAsync(int categoryId, CancellationToken ct);
        Task<IList<Stage>> GetTournamentStages(int seasonStageId, CancellationToken ct);
        Stage? GetStageById(int id, CancellationToken ct);

        Task<IList<Stage>> GetStagesByParentStageIdAsync(int parentStageId, CancellationToken ct);

        Task<Season> GetSeasonByIdAsync(int id);
    }
}
