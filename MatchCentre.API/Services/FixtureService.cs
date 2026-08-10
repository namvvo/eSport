
namespace eSport.MatchCentre.API.Services;


public class FixtureService : IFixtureService
{
    private readonly FixtureContext _db;

    
    public FixtureService(FixtureContext db)
    {
        _db = db;
    }
    public Task<FixtureDto> GetFixtureAsync(int id)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// get fixtures based on multi seasonstageids, categories
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<FixtureSearchResultDto>> SearchFixturesAsync(SearchFixturesRequest request, CancellationToken cancellationToken = default)
    {
        if (request.PreviousHead2Head == 0)
        {
            return await ExecuteStandardSearchAsync(request, cancellationToken);
        }
        else
        {
            return await ExecuteHeadToHeadSearchAsync(request, cancellationToken);
        }
    }
    /// <summary>
    /// Nhánh 1: Search Fixtures thông thường (PreviousHead2Head == 0)
    /// </summary>
    private async Task<List<FixtureSearchResultDto>> ExecuteStandardSearchAsync(SearchFixturesRequest req, CancellationToken ct)
    {
        // 1. Loại bỏ .AsQueryable() dư thừa
        var query = _db.Fixtures.AsNoTracking();

        // 2. Filter theo Date range dạng [start, endExclusive)
        if (req.StartDate.HasValue)
        {
            var start = req.StartDate.Value.Date;
            var endExclusive = (req.EndDate?.Date ?? start).AddDays(1);

            query = query.Where(f => f.Time >= start && f.Time < endExclusive);
        }
        else if (req.EndDate.HasValue)
        {
            var endExclusive = req.EndDate.Value.Date.AddDays(1);
            query = query.Where(f => f.Time < endExclusive);
        }

        // 3. Filter theo Teams
        if (req.TeamId > 0 && req.TeamId2 == 0)
        {
            query = query.Where(f => f.HomeId == req.TeamId || f.AwayId == req.TeamId);
        }
        else if (req.TeamId > 0 && req.TeamId2 > 0)
        {
            query = query.Where(f =>
                (f.HomeId == req.TeamId || f.HomeId == req.TeamId2) &&
                (f.AwayId == req.TeamId || f.AwayId == req.TeamId2));
        }

        // 4. Filter theo SeasonStageId (= ANY(...))
        if (req.SeasonStageIds is { Count: > 0 })
        {
            query = query.Where(f => req.SeasonStageIds.Contains(f.SeasonStageId));
        }

        // 5. Filter theo CategoryIds (EXISTS (...))
        if (req.CategoryIds is { Count: > 0 })
        {
            query = query.Where(f => f.FixtureCategories.Any(cm => req.CategoryIds.Contains(cm.CategoryId)));
        }

        // 6. Filter theo Round
        if (req.StartRound > 0 && req.ToRound > 0)
        {
            query = query.Where(f => f.Round >= req.StartRound && f.Round <= req.ToRound);
        }

        // 7. Filter theo Score & Completion
        if (req.HasScore == 1 || req.IsComplete)
        {
            query = query.Where(f => f.IsComplete);
        }
        else if (req.HasScore == 0)
        {
            query = query.Where(f => f.FullTime == null || f.FullTime == "vs");
        }

        // 8. Omitted Id
        if (req.OmittedId > 0)
        {
            query = query.Where(f => f.Id != req.OmittedId.Value);
        }
        var results =  query
            .OrderByDescending(f => f.Time)
            .Select(f => new FixtureSearchResultDto
            {
                Id = f.Id,
                Round = f.Round,
                IsAwarded = f.IsAwarded,
                //Year = f.SeasonStageMapping.Season.Year,
                IsComplete = f.IsComplete,
                HasVideos = f.HasVideos,
                HomeId = f.HomeId,
                AwayId = f.AwayId,

                Time = f.Time,
                SeasonStageId = f.SeasonStageId,
                HalfTime = f.HalfTime,
                FullTime = f.FullTime,
                ExtraTime = f.ExtraTime,
                PK = f.PK,
                TimeElapsed = f.TimeElapsed,
                LiveScore = f.LiveScore,
                AutoUrl = f.AutoUrl,
                HomeStats = new TeamMatchStatsDto
                {
                    Possession = f.Home.Possession,
                    ShotsGraph = f.Home.ShotsGraph,
                    Formation = f.Home.Formation,

                },
                AwayStats = new TeamMatchStatsDto
                {
                    Possession = f.Away.Possession,
                    ShotsGraph = f.Away.ShotsGraph,
                    Formation = f.Away.Formation,
                }
            });
        var abc = results.ToQueryString();
        // Projection trực tiếp thuộc tính phẳng trên Fixture, tránh JOIN vô ích
        return await query
            .OrderByDescending(f => f.Time)
            .Select(f => new FixtureSearchResultDto
            {
                Id = f.Id,
                Round = f.Round,
                IsAwarded = f.IsAwarded,
                //Year = f.SeasonStageMapping.Season.Year,
                IsComplete = f.IsComplete,
                HasVideos = f.HasVideos,
                HomeId = f.HomeId,
                AwayId = f.AwayId,

                Time = f.Time,
                SeasonStageId = f.SeasonStageId,
                HalfTime = f.HalfTime,
                FullTime = f.FullTime,
                ExtraTime = f.ExtraTime,
                PK = f.PK,
                TimeElapsed = f.TimeElapsed,
                LiveScore = f.LiveScore,
                AutoUrl = f.AutoUrl,

                HomeStats = new TeamMatchStatsDto
                {
                    Possession = f.Home.Possession,
                    ShotsGraph = f.Home.ShotsGraph,
                    Formation = f.Home.Formation,

                },
                AwayStats = new TeamMatchStatsDto
                {
                    Possession = f.Away.Possession,
                    ShotsGraph = f.Away.ShotsGraph,
                    Formation = f.Away.Formation,
                }
            })
            .ToListAsync(ct);
    }

    /// <summary>
    /// Nhánh 2: Head-To-Head Search với O(1) Dictionary Lookup & Fix Bug Copy-Paste
    /// </summary>
    private async Task<List<FixtureSearchResultDto>> ExecuteHeadToHeadSearchAsync(SearchFixturesRequest req, CancellationToken ct)
    {
        var fixtureQuery = _db.Fixtures.AsNoTracking();

        // Filter Date theo dạng [start, endExclusive)
        if (req.StartDate.HasValue)
        {
            var start = req.StartDate.Value.Date;
            var endExclusive = (req.EndDate?.Date ?? start).AddDays(1);
            fixtureQuery = fixtureQuery.Where(f => f.Time >= start && f.Time < endExclusive);
        }
        else if (req.EndDate.HasValue)
        {
            var endExclusive = req.EndDate.Value.Date.AddDays(1);
            fixtureQuery = fixtureQuery.Where(f => f.Time < endExclusive);
        }

        if (req.TeamId > 0 && req.TeamId2 > 0)
        {
            fixtureQuery = fixtureQuery.Where(f =>
                (f.HomeId == req.TeamId || f.HomeId == req.TeamId2) &&
                (f.AwayId == req.TeamId || f.AwayId == req.TeamId2));
        }

        if (req.OmittedId > 0)
        {
            fixtureQuery = fixtureQuery.Where(f => f.Id != req.OmittedId.Value);
        }

        var matchedFixtures = await fixtureQuery
            .Select(f => new
            {
                f.Id,
                f.HomeId,
                f.AwayId,
                f.Time,
                f.Round,
                HomePossession = f.Home.Possession,
                AwayPossession = f.Away.Possession
            })
            .ToListAsync(ct);

        if (matchedFixtures.Count == 0)
            return new List<FixtureSearchResultDto>();

        var fixtureIds = matchedFixtures.Select(f => f.Id).ToArray();  // toarray () for better performance in EF Core queries than toList () or IEnumerable
        //var targetTeamIds = new List<int> { req.TeamId, req.TeamId2 };
        var targetTeamIds = new[]
            {
                req.TeamId,
                req.TeamId2
            }
            .Where(x => x > 0)
            .ToArray();
        // Query Aggregated Stats từ DB
        var teamStatsList = await _db.FixtureStat
            .AsNoTracking()
            .Where(fs => fixtureIds.Contains(fs.FixtureId) && fs.Rating > 0 && targetTeamIds.Contains(fs.TeamOwnerId))
            .GroupBy(fs => new { fs.FixtureId, fs.TeamOwnerId })
            .Select(g => new
            {
                g.Key.FixtureId,
                g.Key.TeamOwnerId,
                Stats = new TeamMatchStatsDto
                {
                    Rating = Math.Round(g.Average(x => (double)x.Rating), 2),
                    YellowCards = g.Sum(x => x.YellowCard),
                    RedCards = g.Sum(x => x.RedCard),
                    PassAccuracy = Math.Round(g.Average(x => (double)x.AccPasses), 2),
                    Dribbles = g.Sum(x => x.Dribbles),
                    AerialsWon = g.Sum(x => x.AerialWon),
                    Assists = g.Sum(x => x.Assist),
                    Goals = g.Sum(x => x.Goal),
                    Fouls = g.Sum(x => x.Fouls),
                    Offsides = g.Sum(x => x.Offsided),
                    ShotsOnTarget = g.Sum(x => x.ShotsOnTarget),
                    Fouled = g.Sum(x => x.Fouled),
                    Dispossessed = g.Sum(x => x.Dispossessed),
                    Tackles = g.Sum(x => x.TotalTackles),
                    Interceptions = g.Sum(x => x.Interceptions),
                    BlockedShots = g.Sum(x => x.BlockedShots),
                    Clearances = g.Sum(x => x.Clearances)
                }
            })
            .ToListAsync(ct);

        // TỐI ƯU CỐT LÕI: Tạo Dictionary Tuple Key O(1) Lookup
        var statLookup = teamStatsList.ToDictionary(
            x => (x.FixtureId, x.TeamOwnerId),
            x => x.Stats
        );

        // Map kết quả trong bộ nhớ với O(1) Complexity
        return matchedFixtures.Select(f =>
        {
            statLookup.TryGetValue((f.Id, f.HomeId), out var homeStat);
            statLookup.TryGetValue((f.Id, f.AwayId), out var awayStat);

            // Gán Possession thuộc tính phẳng từ Fixture
            if (homeStat != null) homeStat.Possession = f.HomePossession;
            if (awayStat != null) awayStat.Possession = f.AwayPossession;

            return new FixtureSearchResultDto
            {
                Id = f.Id,
                HomeId = f.HomeId,
                AwayId = f.AwayId,
                Time = f.Time,
                Round = f.Round,

                // Trả về Complex Objects chuẩn, đã fix bug gán nhầm homeStat cho awayStat
                HomeStats = homeStat ?? new TeamMatchStatsDto { Possession = f.HomePossession },
                AwayStats = awayStat ?? new TeamMatchStatsDto { Possession = f.AwayPossession }
            };
        }).ToList();
    }
    public void GetTeamStatAsync(int teamId, string seasonStageIds,
                                          int lastMatches = 0, bool form = false,
                                          bool progress = false, int hasScore = 2,
                                          int filter = (int)ViewType.Overall)
    {
    }

    //private SeasonStageGrpc.SeasonStageGrpcClient GetSeasonStageGrpcClient()
    //{
    //    if (_seasonStageGrpcClient is not null)
    //    {
    //        return _seasonStageGrpcClient;
    //    }

    //    _channel = GrpcChannel.ForAddress("https://localhost:7220");

    //    _seasonStageGrpcClient = new SeasonStageGrpc.SeasonStageGrpcClient(_channel);

    //    return _seasonStageGrpcClient;
    //}

    //protected virtual void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        _channel?.Dispose();
    //    }
    //}

    //~FixtureService()
    //{
    //    Dispose(false);
    //}
}
