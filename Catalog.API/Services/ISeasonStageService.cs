namespace eSport.Catalog.API.Services
{
    public interface ISeasonStageService
    {
        Task<SeasonStage?> GetSeasonStageMappingByIdAsync(int seasonStageId, bool loadSeason = false, CancellationToken ct = default);


        Task<SeasonStage?> GetCurrentSeasonStageByCategoryAsync(int categoryId, CancellationToken ct = default);
        Task<SeasonStage?> GetCurrentSeasonStageByCategoryInTournamentAsync(int categoryId, CancellationToken ct);

        Task<IList<SeasonStage>> GetSeasonStagesAsync(int seasonId = 0, IList<int> stageIds = null, CancellationToken ct = default);
        Task<IEnumerable<Stage>> GetStagesByCategoryAsync(int categoryId, CancellationToken ct = default);
        Task<IList<Stage>> GetTournamentStages(int seasonStageId, CancellationToken ct = default);
        Stage? GetStageById(int id, CancellationToken ct = default);

        Task<IList<Stage>> GetStagesByParentStageIdAsync(int parentStageId, CancellationToken ct = default);
        Task<List<SeasonStageDto>> GetCategorySeasonStagesAsync(
        int categoryId,
        int seasonId=0,
        int stageId=0,
        CancellationToken cancellationToken = default);
        Task<Season> GetSeasonByIdAsync(int id);
    }
}
