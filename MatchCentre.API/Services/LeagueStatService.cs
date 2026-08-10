using eSport.MatchCentre.API.Dto.League;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static eSport.TeamPlayer.API.Grpc.TeamPlayerGrpc;
using static HotChocolate.Types.SpecScalarNames;

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
        var sw = Stopwatch.StartNew();


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
        //        _logger.LogInformation(
        //"GetLeaguePlayerStatsAsync-before paging: {Elapsed} ms",
        //sw.ElapsedMilliseconds);
        var page = await aggregate.ToPagedResultAsync(filter.PageIndex,
                                                      filter.PageSize,
                                                      ct);


        _logger.LogInformation(
"GetLeaguePlayerStatsAsync-ToPagedResultAsync: {Elapsed} ms",
sw.ElapsedMilliseconds);
        //var results = await aggregate.ToPagedResultAsync(filter.PageIndex,
        //                                          filter.PageSize);
        //var sql =aggregate.ToQueryString();
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
        //        _logger.LogInformation(
        //"GetLeaguePlayerStatsAsync-map: {Elapsed} ms",
        //sw.ElapsedMilliseconds);
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
            //         join player in _db.pl
            //on stat.PlayerId equals player.Id
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
        Rating = Math.Round(g.Average(x => x.Stat.Rating), 2),
        Apps = g.Count(x => x.Stat.MinPlayed > 10), // EF 10 dịch Count() trực tiếp tối ưu sang SQL
        Goals = g.Sum(x => x.Stat.Goal),
        //TeamGoals = g.Sum(x => x.Stat.TeamGoal),
        PenGoals = g.Sum(x => x.Stat.PenGoal),
        Assists = g.Sum(x => x.Stat.Assist),
        ShotsOT = Math.Round(g.Average(x => (double)x.Stat.ShotsOnTarget), 2),
        Dribbles = Math.Round(g.Average(x => (double)x.Stat.Dribbles), 2),
        ShotsPerGame = Math.Round(g.Average(x => (double)x.Stat.Shots), 2),
        KeyPasses = Math.Round(g.Average(x => (double)x.Stat.KeyPasses), 2),
        Fouled = Math.Round(g.Average(x => (double)x.Stat.Fouled), 2),
        Offsides = Math.Round(g.Average(x => (double)x.Stat.Offsided), 2),
        Yellow = g.Sum(x => x.Stat.YellowCard),
        Red = g.Sum(x => x.Stat.RedCard),
        PSPercentage = Math.Round(g.Average(x => x.Stat.Passes > 0 ? ((double)x.Stat.AccPasses / x.Stat.Passes) : 0), 2) * 100,
        Interceptions = g.Average(x => (double)x.Stat.Interceptions),
        Dispossessed = Math.Round(g.Average(x => (double)x.Stat.Dispossessed), 2),
        Blocks = Math.Round(g.Average(x => (double)x.Stat.BlockedShots), 2),
        Unstouch = Math.Round(g.Average(x => (double)x.Stat.UnsTouches), 2),

        Crosses = Math.Round(g.Average(x => (double)x.Stat.Crosses), 2),
        LongBalls = Math.Round(g.Average(x => (double)x.Stat.LongBall), 2),
        ThroughBalls = Math.Round(g.Average(x => (double)x.Stat.ThroughBall), 2),
        Tackles = Math.Round(g.Average(x => (double)x.Stat.TotalTackles), 2),
        MinPlayed = g.Sum(x => x.Stat.MinPlayed),
        OwnGoal = g.Sum(x => x.Stat.OwnGoal),
        Clearances = Math.Round(g.Average(x => (double)x.Stat.Clearances), 2),
        AvgP = Math.Round(g.Average(x => (double)x.Stat.Passes), 2),
        AccCrosses = Math.Round(g.Average(x => (double)x.Stat.AccCrosses), 2),
        Touches = Math.Round(g.Average(x => (double)x.Stat.Touches), 2),

        Passes = Math.Round(g.Average(x => (double)x.Stat.Passes), 2),

        AerielsWon = Math.Round(g.Average(x => (double)x.Stat.AerialWon), 2),



        Fouls = Math.Round(g.Average(x => (double)x.Stat.Fouls), 2),

        Subs = g.Count(x => x.Stat.SubInMinute > 0 && x.Stat.Position == "Sub"),
        Motm = g.Sum(x => x.Stat.Motm ? 1 : 0),
        Saves = Math.Round(g.Average(x => (double)x.Stat.GKSaves), 2),
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

    private static IQueryable<LeaguePlayerAggregateRow> ApplySorting(
 IQueryable<LeaguePlayerAggregateRow> query,
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
                         1.5 * (x.PSPercentage / 100),
                    filter.Direction),

            LeaguePlayerOrderBy.MarketValue =>
                query.OrderByDirection(x => x.MarketValue, filter.Direction),

            LeaguePlayerOrderBy.Apps =>
                      query.OrderByDirection(x => x.Apps, filter.Direction),

            LeaguePlayerOrderBy.Assists =>
                query.OrderByDirection(x => x.Assists, filter.Direction),

            LeaguePlayerOrderBy.Minutes =>
                query.OrderByDirection(x => x.MinPlayed, filter.Direction),

            LeaguePlayerOrderBy.Saves =>
                query.OrderByDirection(x => x.Saves, filter.Direction),

            _ =>
                query.OrderByDirection(x => x.Rating, filter.Direction)
        };
    }

}