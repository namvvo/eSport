namespace eSport.Catalog.API.Services;

public class SeasonStageService : ISeasonStageService
{
    private readonly CatalogContext _db;
    private readonly RedisCache _cached;
    private readonly TeamPlayerGrpc.TeamPlayerGrpcClient _teamPlayerGrpcClient;
    public SeasonStageService(CatalogContext db,
        RedisCache cache,
        TeamPlayerGrpc.TeamPlayerGrpcClient teamPlayerGrpcClient)
    {
        _db = db;
        _cached = cache;
        _teamPlayerGrpcClient = teamPlayerGrpcClient;
    }
    /// <summary>
    /// if a season in the past has not completed, it will return the latest season stage that is not completed for the category
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<SeasonStage?> GetCurrentSeasonStageByCategoryAsync(int categoryId, CancellationToken ct)
    {
        //var category = _db.Categories.Find(categoryId);
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
                     orderby se.Year2 descending, s.DisplayOrder ascending
                     select ss;

        return await result.FirstOrDefaultAsync(ct);
    }
    /// <summary>
    /// 1. must have at least 1 fixture round
    //2.  otherwise, stay with the orderby desc seasonstageid, desc completeround
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<SeasonStage?> GetCurrentSeasonStageByCategoryInTournamentAsync(int categoryId, CancellationToken ct)
   => await _db.CategorySeasonStages
           .AsNoTracking()
           .Where(w => w.CategoryId == categoryId)
           .OrderByDescending(o => o.SeasonStageId).ThenByDescending(o => o.CompleteRound)
           .Select(s => s.SeasonStage)
           .FirstOrDefaultAsync(ct);


    /// <summary>
    /// StageId =  1 for domestic league, StageId > 2 for tournament
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public async Task<SeasonStage?> GetLatestSeasonStageForDomesticByCategoryAsync(int categoryId, CancellationToken ct)
    => throw new NotImplementedException();

    //await _db.SeasonStages.Where(o => o.StageId == 1)
    //                                           .OrderByDescending(o => o.SeasonId)
    //                                           .FirstOrDefaultAsync();

    public async Task<SeasonStage?> GetSeasonStageMappingByIdAsync(int seasonStageId, bool loadSeason = false, CancellationToken ct = default)
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


    public async Task<IList<SeasonStage>> GetSeasonStagesAsync(int seasonId = 0, IList<int> stageIds = null, CancellationToken ct = default)
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
    /// <summary>
    /// avoid using Include just to get simple fields
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<List<SeasonStageDto>> GetCategorySeasonStagesAsync(int categoryId, CancellationToken ct = default)
    => await _db.CategorySeasonStages
            .AsNoTracking()
            .Where(w => w.CategoryId == categoryId)
            .Select(s => new SeasonStageDto
            {
                SeasonId = s.SeasonStage.SeasonId,
                Year = s.SeasonStage.Season.Year,
                Year2 = s.SeasonStage.Season.Year2,
                SeasonStageId = s.SeasonStageId
            }).ToListAsync(ct);

    /// <summary>
    /// list all season stage mapping for a category, if seasonId = 0, it will return all seasons, if stageId = 0, it will return all stages
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="seasonId"></param>
    /// <param name="stageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<SeasonStageDto>> GetCategorySeasonStagesAsync(int categoryId,
                                                                  int seasonId = 0,
                                                                  int stageId = 0,
                                                                  CancellationToken ct = default)
    {
        // 1. Gọi gRPC lấy danh sách SeasonStageId từ TeamPlayer Subgraph
        var grpcResponse = await _teamPlayerGrpcClient.GetSeasonStageIdsByCategoryAsync(
            new GetSeasonStageIdsByCategoryRequest { CategoryId = categoryId },
            cancellationToken: ct);

        var seasonStageIds = grpcResponse.SeasonStageIds;

        // Nếu không tìm thấy mapping nào ở TeamPlayer, trả về danh sách rỗng luôn (tránh query DB vô ích)
        if (seasonStageIds.Count == 0)
        {
            return new List<SeasonStageDto>();
        }

        // 2. Lấy danh sách stageIds (sub-stages)
        var stageIdsQuery = _db.Stages
            .AsNoTracking()
            .Where(x => stageId == 0 || x.ParentId == stageId)
            .Select(x => x.Id);

        // 3. Lấy tên Category (vì categoryId truyền vào cố định)
        var categoryName = await _db.Categories
            .AsNoTracking()
            .Where(c => c.Id == categoryId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync(ct) ?? string.Empty;

        // 4. Truy vấn tối ưu trên DB Catalog
        var query = await (
            from ssm in _db.SeasonStages.AsNoTracking()
            join s in _db.Seasons.AsNoTracking() on ssm.SeasonId equals s.Id
            join st in _db.Stages.AsNoTracking() on ssm.StageId equals st.Id
            where seasonStageIds.Contains(ssm.Id)
               && stageIdsQuery.Contains(ssm.StageId)
               && (seasonId == 0 || ssm.SeasonId == seasonId)
               && s.Status == true
            select new SeasonStageDto
            {
                SeasonId = s.Id,
                Year = s.Year,
                Year2 = s.Year2,
                SeasonStageId = ssm.Id,
                CategoryName = categoryName,
                Stage = st.Name,
                StageId = st.Id
            }
        )
        .Distinct()
        .ToListAsync(ct);

        return query;
    }
    public async Task<IList<Stage>> GetStagesByParentStageIdAsync(int parentStageId, CancellationToken ct)
    {

        return await _db.Stages.Where(w => w.ParentId == parentStageId).ToListAsync();

    }

    public async Task<IList<Stage>> GetTournamentStages(int seasonStageId, CancellationToken ct)
    {

        var seasonStage = await GetSeasonStageMappingByIdAsync(seasonStageId, false, ct);
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

        return await _db.Seasons.FindAsync(id) ??
            throw new System.Collections.Generic.KeyNotFoundException("Season not found");
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
