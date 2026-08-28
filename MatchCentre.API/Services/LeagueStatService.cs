using eSport.MatchCentre.API.Dto.League;
using System.Diagnostics;
using static eSport.TeamPlayer.API.Grpc.TeamPlayerGrpc;

namespace eSport.MatchCentre.API.Services;
/// <summary>
///                          MatchCentre
//               │
//      ┌────────▼────────┐
//      │ FixtureStats EF │
//      └────────┬────────┘
//               │
//             WHERE
//               │
//           GROUP BY
//               │
//      ┌────────▼──────────┐
//      │ AggregateRow      │
//      │ (flat SQL model)  │
//      └────────┬──────────┘
//               │
//       Sorting / Paging
//               │
//          n rows
//               │
//      ┌────────▼──────────┐
//      │ Mapper            │
//      └────────┬──────────┘
//               │
//┌──────────────▼──────────────┐
//│ LeaguePlayerAggregateDto    │
//│                             │
//│ General                     │
//│ Attacking                   │
//│ Passing                     │
//│ Defending                   │
//│ Goalkeeping                 │
//└──────────────┬──────────────┘
//               │
//      GraphQL / Fusion
//        │             │
//     player         team
//       ↓               ↓
//  Player Lookup    Team Lookup
//  DataLoader       DataLoader
/// </summary>
public class LeagueStatService : ILeagueStatService
{
    private readonly ILogger _logger;
    private readonly TeamPlayerGrpcClient _teamPlayerGrpcClient;
    private readonly FixtureContext _db;
    public LeagueStatService(FixtureContext db,
        ILogger<TeamService> logger,
        TeamPlayerGrpcClient teamPlayerGrpcClient)
    {
        _db = db;
        _logger = logger;
        _teamPlayerGrpcClient = teamPlayerGrpcClient;
    }
    public async Task<PagedResult<LeaguePlayerAggregateDto>> GetLeaguePlayerStatsAsync(LeaguePlayerStatsFilter filter, CancellationToken ct)
    {
        //var sw = Stopwatch.StartNew();


        var query = BuildQuery();

        query = await ApplyFilters(query, filter);

        var aggregate = Aggregate(query);

        if (filter.CountApp > 0)
        {
            aggregate = await ApplyCountAppAsync(
                aggregate,
                ct);
        }

        aggregate = ApplySorting(aggregate, filter);
        var page = await aggregate.ToPagedResultAsync(filter.PageIndex,
                                                      filter.PageSize,
                                                      ct);
        var teamGoalsDict = await ComputeTeamGoals(page, filter);
        foreach (var player in page.Items)
        {
            if (teamGoalsDict.TryGetValue(
            (
            player.TeamOwnerId,
            player.CategoryId,
            filter.SeasonStageIds
            ),
            out var teamGoals))
            {
                player.TeamGoals = teamGoals;
            }
        }

        var results = new PagedResult<LeaguePlayerAggregateDto>
        {
            Items = page.Items
                        .Select(LeaguePlayerAggregateMapper.Map)
                        .ToList(),

            HasNextPage = page.HasNextPage,
            HasPreviousPage = page.HasPreviousPage,
            PageIndex = page.PageIndex,
            PageSize = page.PageSize
        };

        return results;
    }
    private IQueryable<PlayerStatQueryDto> BuildQuery()
    {
        return
            from stat in _db.FixtureStat.AsNoTracking()
            join fixture in _db.Fixtures.AsNoTracking()
                on stat.FixtureId equals fixture.Id
            join category in _db.FixtureCategories.AsNoTracking()
                on fixture.Id equals category.FixtureId

            select new PlayerStatQueryDto
            {
                Stat = stat,
                Fixture = fixture,
                FixtureCategory = category
            };
    }

    private async Task<IQueryable<PlayerStatQueryDto>> ApplyFilters(IQueryable<PlayerStatQueryDto> query,
                                                                    LeaguePlayerStatsFilter filter)
    {
        if (filter.CategoryIds is { Count: > 0 })
        {
            query = query.Where(x =>
                filter.CategoryIds.Contains(x.FixtureCategory.CategoryId));
        }
        if (filter.SeasonStageIds > 0)
            query = query.Where(s => filter.SeasonStageIds == s.Fixture.SeasonStageId);

        if (filter.TeamId > 0)
            query = query.Where(s => s.Stat.TeamOwnerId == filter.TeamId);
        // Áp dụng nhánh điều kiện PartitionBySeasonStageId
        if (filter.OnlyRegisteredPlayers && filter.SeasonStageIds > 0)
        {

            //query = query.Where(s => s.Player.TeamPlayerMappings.Any(tpm => tpm.Status == 1));
            var response = await _teamPlayerGrpcClient.GetRegisteredPlayerIdsBySeasonStagesAsync(new GetRegisteredPlayerIdsRequest()
            {
                SeasonStageIds = filter.SeasonStageIds

            });

            var registeredPlayerIds = response.RegisteredPlayerIds;
            query = query.Where(x => registeredPlayerIds.Contains(x.Stat.PlayerId));
        }
        else
        {
            query = query.Where(s => s.Stat.Passes > 0);
        }
        if (filter.FixtureId > 0)
            query = query.Where(s => s.Fixture.Id == filter.FixtureId);

        if (filter.MinPlayed > 0)
            query = query.Where(s => s.Stat.MinPlayed >= filter.MinPlayed);

        if (filter.PlayerIds.Count > 0)
            query = query.Where(s => filter.PlayerIds.Contains(s.Stat.PlayerId));

        //if (!string.IsNullOrWhiteSpace(filter.PlayerName))
        //    query = query.Where(s => s.Player.Name.Contains(filter.PlayerName));



        //if (!string.IsNullOrWhiteSpace(filter.TeamPosition))
        //    query = query.Where(s => s.Stat..TeamPosition.Contains(filter.TeamPosition));



        //if (filter.PriceMin > 0 || filter.PriceMax > 0)
        //    query = query.Where(x =>
        //        x..p.MarketValue >= priceMin &&
        //        x.p.MarketValue <= priceMax);
        if (filter.TimeLimit.HasValue)
            query = query.Where(s => s.Fixture.Time <= filter.TimeLimit.Value);

        if (filter.TimeFrom.HasValue)
            query = query.Where(s => s.Fixture.Time >= filter.TimeFrom.Value);

        if (filter.TimeTo.HasValue)
            query = query.Where(s => s.Fixture.Time <= filter.TimeTo.Value);

        //if (filter.PriceMin > 0 || filter.PriceMax > 0)
        //    query = query.Where(s => s.Player.MarketValue >= filter.PriceMin && s.Player.MarketValue <= filter.PriceMax);



        query = query.Where(s => s.Stat.Rating > 0);
        return query;
    }

    /// <summary>
    /// keeps data in raw, only round value after sorting
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    private IQueryable<LeaguePlayerAggregateRow> Aggregate(
                         IQueryable<PlayerStatQueryDto> query) => query

    .GroupBy(x => new
    {
        x.Stat.PlayerId,
        x.Stat.TeamOwnerId,
        x.FixtureCategory.CategoryId
    })
    .Select(g => new LeaguePlayerAggregateRow
    {
        PlayerId = g.Key.PlayerId,
        TeamOwnerId = g.Key.TeamOwnerId,
        CategoryId = g.Key.CategoryId,
        Rating = g.Average(x => x.Stat.Rating),
        Apps = g.Count(x => x.Stat.MinPlayed > 10), // EF 10 dịch Count() trực tiếp tối ưu sang SQL
        Goals = g.Sum(x => x.Stat.Goal),
        //TeamGoals = ComputeTeamGoals(g.,g.Key.TeamOwnerId),
        PenGoals = g.Sum(x => x.Stat.PenGoal),
        Assists = g.Sum(x => x.Stat.Assist),
        ShotsOT = g.Average(x => (double)x.Stat.ShotsOnTarget),
        Dribbles = g.Average(x => (double)x.Stat.Dribbles),
        ShotsPerGame = g.Average(x => (double)x.Stat.Shots),
        KeyPasses = g.Average(x => (double)x.Stat.KeyPasses),
        Fouled = g.Average(x => (double)x.Stat.Fouled),
        Offsides = g.Average(x => (double)x.Stat.Offsided),
        Yellow = g.Sum(x => x.Stat.YellowCard),
        Red = g.Sum(x => x.Stat.RedCard),
        AccuratePassingPercentage = g.Average(x => x.Stat.Passes > 0 ? ((double)x.Stat.AccPasses / x.Stat.Passes) : 0) * 100,
        AccPasses = g.Average(x => x.Stat.AccPasses),
        Interceptions = g.Average(x => (double)x.Stat.Interceptions),
        Dispossessed = g.Average(x => (double)x.Stat.Dispossessed),
        Blocks = g.Average(x => (double)x.Stat.BlockedShots),
        Unstouch = g.Average(x => (double)x.Stat.UnsTouches),

        Crosses = g.Average(x => (double)x.Stat.Crosses),
        LongBalls = g.Average(x => (double)x.Stat.LongBall),
        ThroughBalls = g.Average(x => (double)x.Stat.ThroughBall),
        Tackles = g.Average(x => (double)x.Stat.TotalTackles),
        MinPlayed = g.Sum(x => x.Stat.MinPlayed),
        OwnGoals = g.Sum(x => x.Stat.OwnGoal),
        Clearances = g.Average(x => (double)x.Stat.Clearances),
        AvgP = g.Average(x => (double)x.Stat.Passes),
        AccCrosses = g.Average(x => (double)x.Stat.AccCrosses),
        Touches = g.Average(x => (double)x.Stat.Touches),
        Passes = g.Average(x => (double)x.Stat.Passes),
        AerielsWon = g.Average(x => (double)x.Stat.AerialWon),
        Fouls = g.Average(x => (double)x.Stat.Fouls),
        Subs = g.Count(x => x.Stat.SubInMinute > 0 && x.Stat.Position == "Sub"),
        Motm = g.Sum(x => x.Stat.Motm ? 1 : 0),
        Saves = g.Average(x => (double)x.Stat.GKSaves),
    });
    private async Task<IQueryable<LeaguePlayerAggregateRow>> ApplyCountAppAsync(
    IQueryable<LeaguePlayerAggregateRow> query,
    CancellationToken cancellationToken)
    {


        var maxApps = await query.MaxAsync(
            x => (int?)x.Apps,
            cancellationToken) ?? 0;

        if (maxApps == 0)
            return query.Where(_ => false);

        var minApps = maxApps / 2.0;

        return query.Where(x => x.Apps > minApps);
    }
    private async Task<Dictionary<(int TeamId, int CategoryId, int SeasonStageId), int>> ComputeTeamGoals(PagedResult<LeaguePlayerAggregateRow> page, LeaguePlayerStatsFilter filter)
    {
        var teamKeys = page.Items
                           .Select(x => x.TeamOwnerId)
                           .Distinct()
                           .ToList();
        var response = await _teamPlayerGrpcClient.GetTeamCategoryAsync(new GetTeamCategoryRequest()
        {
            TeamIds = { teamKeys },
            CategoryId = filter.CategoryIds.FirstOrDefault(),
            SeasonStageId = filter.SeasonStageIds
        });
        return response.Items.ToDictionary(item => (item.TeamId, item.CategoryId, item.SeasonStageId), item => item.TeamGoals);
    }
    private static IQueryable<LeaguePlayerAggregateRow> ApplySorting(IQueryable<LeaguePlayerAggregateRow> query,
                                                                     LeaguePlayerStatsFilter filter)
    {
        //bool desc = filter.Direction == SortDirection.Desc;

        return filter.OrderBy switch
        {

            LeaguePlayerOrderBy.Goals =>
                  query.OrderByDirection(
                        x => x.Goals + x.PenGoals,
                                 filter.Direction),

            LeaguePlayerOrderBy.Defense =>
       query.OrderByDirection(
           x => 1.5 * x.Tackles +
                2 * x.Clearances +
                x.Blocks +
                x.Interceptions -
                2 * x.OwnGoals -
                1.5 * x.Fouls,
           filter.Direction),

            LeaguePlayerOrderBy.Offense =>
                query.OrderByDirection(
                    x => 3 * x.Goals +
                         2 * x.Assists +
                         x.ShotsPerGame +
                         1.5 * x.KeyPasses +
                         1.5 * x.Dribbles,
                    filter.Direction),

            LeaguePlayerOrderBy.Passing =>
                query.OrderByDirection(
                    x => 3 * x.Assists +
                         1.5 * x.KeyPasses +
                         x.AccCrosses +
                         1.5 * (x.AccuratePassingPercentage / 100),
                    filter.Direction),

            LeaguePlayerOrderBy.MarketValue =>
                query.OrderByDirection(x => x.MarketValue, filter.Direction),

            LeaguePlayerOrderBy.Apps =>
                      query.OrderByDirection(x => x.Apps, filter.Direction),

            LeaguePlayerOrderBy.Assists =>
                query.OrderByDirection(x => x.Assists, filter.Direction),

            LeaguePlayerOrderBy.ShotsPerGame => query.OrderByDirection(x => x.ShotsPerGame, filter.Direction),
            LeaguePlayerOrderBy.Dribbles => query.OrderByDirection(x => x.Dribbles, filter.Direction),
            LeaguePlayerOrderBy.PassAccuracy => query.OrderByDirection(x => x.AccPasses, filter.Direction),
            LeaguePlayerOrderBy.Aggression => query.OrderByDirection(x => x.Yellow + x.Red * 2, filter.Direction),

            LeaguePlayerOrderBy.Minutes =>
                query.OrderByDirection(x => x.MinPlayed, filter.Direction),

            LeaguePlayerOrderBy.Saves =>
                query.OrderByDirection(x => x.Saves, filter.Direction),

            _ =>
                query.OrderByDirection(x => x.Rating, filter.Direction)
        };
    }

}