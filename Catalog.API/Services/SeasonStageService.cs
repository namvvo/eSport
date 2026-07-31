using static HotChocolate.Fusion.Rewriters.InlineFragmentOperationRewriter;

namespace eSport.Catalog.API.Services
{
    public class SeasonStageService : ISeasonStageService
    {
        private readonly CatalogContext _db;
        private readonly RedisCache _cached;
        public SeasonStageService(CatalogContext db, RedisCache cache)
        {
            _db = db;
            _cached = cache;
        }
        /// <summary>
        /// if a season in the past has not completed, it will return the latest season stage that is not completed for the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<SeasonStage?> GetCurrentSeasonStageByCategoryAsync(int categoryId, CancellationToken ct)
        {
            var category = _db.Categories.Find(categoryId);
            //if (category == null)
            //{
            //    throw new ArgumentException("Category is not a tournament or category is not available");
            //}
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId, nameof(categoryId));
            var result = from csm in _db.CategoryStages
                         join s in _db.Stages on csm.StageId equals s.Id
                         join ss in _db.SeasonStages on s.Id equals ss.StageId
                         join se in _db.Seasons on ss.SeasonId equals se.Id
                         where csm.CategoryId == categoryId && !ss.IsComplete
                         orderby se.Year2 ascending, s.DisplayOrder ascending
                         select ss;

            return await result.FirstOrDefaultAsync(ct);
        }

        public Task<SeasonStage?> GetCurrentSeasonStageByCategoryInTournamentAsync(int categoryId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// StageId =  1 for domestic league, StageId > 2 for tournament
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<SeasonStage?> GetLatestSeasonStageForDomesticByCategoryAsync(int categoryId, CancellationToken ct)
        => await _db.SeasonStages.Where(o => o.StageId == 1)
                                                   .OrderByDescending(o => o.SeasonId)
                                                   .FirstOrDefaultAsync();

        public async Task<SeasonStage?> GetSeasonStageMappingByIdAsync(int seasonStageId, CancellationToken ct, bool loadSeason = false)
        {
            if (seasonStageId <= 0)
                throw new ArgumentException("Invalid SeasonStage Id");
            //if (loadSeason)
            //{
            //    var source = _db.SeasonStages
            //                   .Include(x => x.Season)
            //                   .Include(x => x.Stage);
            //    return source;
            //}
            //else
            return await _db.SeasonStages.FirstOrDefaultAsync(a => a.Id == seasonStageId);
        }



        public Stage? GetStageById(int id, CancellationToken ct)
        {
            return _db.Stages.Find(id);
        }

        
        public async Task<IList<SeasonStage>> GetSeasonStagesAsync(CancellationToken ct, int seasonId = 0, IList<int> stageIds = null)
        {
            var query = _db.SeasonStages.Where(s => s.Status); // update manually on db
            if (seasonId > 0)
                query = query.Where(q => q.SeasonId == seasonId);

            //});

            if (stageIds != null && stageIds.Any())
            {
                if (stageIds[0] > 0)
                {
                    //foreach (var id in stageIds)
                    //{

                    //}
                    query = query.Where(q => stageIds.Contains(q.StageId) &&
                    (q.SeasonId == seasonId || seasonId == 0));
                    var query2 = query.Where(q => stageIds.Contains(q.StageId));
                }
            }

            return await query.OrderBy(o => o.Stage.DisplayOrder).ToListAsync();
        }

        public async Task<IEnumerable<Stage>> GetStagesByCategoryAsync(int categoryId, CancellationToken ct)
        {

            var category = await _db.Categories.FirstOrDefaultAsync(f => f.Id == categoryId);
            if (category is null) return new List<Stage>();

            var stageIds = await _db.CategoryStages.AsNoTracking()
                .Where(w => w.CategoryId == categoryId)
                .Select(x => x.StageId).ToListAsync();

            var result = new List<Stage>();
            result.AddRange(await GetStagesAsync(category.Name));

            foreach (var id in stageIds)
            {
                var stage = await _db.Stages.FirstOrDefaultAsync(f => f.Id == id);
                if (stage != null && !stage.ParentId.HasValue
                    && result.Count(c => c.Id == stage.Id) == 0)
                    result.Add(stage);
            }
            return result.OrderBy(o => o.DisplayOrder);

        }

        public async Task<IList<Stage>> GetStagesByParentStageIdAsync(int parentStageId, CancellationToken ct)
        {

            return await _db.Stages.Where(w => w.ParentId == parentStageId).ToListAsync();

        }

        public async Task<IList<Stage>> GetTournamentStages(int seasonStageId, CancellationToken ct)
        {

            var seasonStage = await GetSeasonStageMappingByIdAsync(seasonStageId, ct);
            if (seasonStage is null) return new List<Stage>();

            var stage = GetStageById(seasonStage.StageId, ct);
            var stages = await GetStagesByParentStageIdAsync(seasonStage.StageId, ct);

            if (!String.IsNullOrWhiteSpace(stage.EuroWhoscoredName)
                && stage.EuroWhoscoredName.Contains("EURO"))
                stages = await GetStagesAsync("EURO");

            if (!String.IsNullOrWhiteSpace(stage.C1WhoscoredName)
                && stage.C1WhoscoredName.Contains("Champions League"))
                stages = await GetStagesAsync("Champions League");

            if (!String.IsNullOrWhiteSpace(stage.C3WhoscoredName)
               && stage.C3WhoscoredName.Contains("Europa League"))
                stages = await GetStagesAsync("Europa League");

            return stages;
        }
        public async Task<Season> GetSeasonByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            return await _db.Seasons.FindAsync(id)??
                throw new System.Collections.Generic.KeyNotFoundException("Season not found");
        }
        public Task<IList<SeasonStage>> GetSeasonStagesAsync(int categoryId,
                                                             CancellationToken ct,
                                                             int seasonId = 0,
                                                             int stageId = 0)
        {
            var category = _db.Categories.Find(categoryId);
           
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId, nameof(categoryId));

            var stageIds = _db.Stages
    .Where(x => x.ParentId == stageId || stageId == 0)
    .Select(x => x.Id);

            var query =
                (from c in _db.Categories
                 join tcm in _db.TeamCategoryMappings
                     on c.Id equals tcm.CategoryId
                 join ssm in _db.SeasonStageMappings
                     on tcm.SeasonStageId equals ssm.Id
                 join s in _db.Seasons
                     on ssm.SeasonId equals s.Id
                 join st in _db.Stages
                     on ssm.StageId equals st.Id
                 where tcm.CategoryId == categoryId
                    && stageIds.Contains(ssm.StageId)
                    && (ssm.SeasonId == seasonId || seasonId == 0)
                    && s.Status == 1
                 select new
                 {
                     SeasonId = s.Id,
                     s.Year,
                     s.Year2,
                     SeasonStageId = tcm.SeasonStageId,
                     CategoryName = c.Name,
                     StageName = st.Name,
                     StageId = st.Id
                 })
                .Distinct();

        }

        private async Task<IList<Stage>> GetStagesAsync(string league)
        {
            string name = "";
            if (league.Contains("EURO"))
            {
                name = "EURO";
                return await _db.Stages.Where(w => w.EuroSofascoreName.Contains(name)).ToListAsync();
            }
            if (league.Contains("Europa League"))
            {
                name = "Europa League";
                return await _db.Stages.Where(w => w.C3SofascoreName.Contains(name)).ToListAsync();
            }
            if (league.Contains("Champions League"))
            {
                name = "Champions League";
                return await _db.Stages.Where(w => w.C1SofascoreName.Contains(name)).ToListAsync();
            }
            return new List<Stage>();
        }

        
    }
}
