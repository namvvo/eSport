using StackExchange.Redis;
using System.Diagnostics;
using System.Text.Json;

namespace eSport.MatchCentre.API.Services
{
    public class TeamService : ITeamService
    {
        private readonly SeasonStageGrpc.SeasonStageGrpcClient _seasonStageGrpcClient;
        private readonly TeamPlayerGrpc.TeamPlayerGrpcClient _teamPlayerGrpcClient;
        private readonly FixtureContext _db;       
        private readonly RedisCache _cached;
        private readonly ILogger _logger;
        public TeamService(FixtureContext db,
            RedisCache cached,
            ILogger<TeamService> logger,
            SeasonStageGrpc.SeasonStageGrpcClient seasonStageGrpcClient,
            TeamPlayerGrpc.TeamPlayerGrpcClient teamPlayerGrpcClient
             )
        {

            //_fixtureService = fixtureService;
            _db = db;
            _cached = cached;
            _logger = logger;
            _seasonStageGrpcClient = seasonStageGrpcClient;
            _teamPlayerGrpcClient = teamPlayerGrpcClient;
        }
        public async Task<TopTeamStatDto> GetTopTeamStatsAsync(int categoryId, int seasonStageId, CancellationToken ct)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var seasonStageIds = new List<int>();

                var stages = await GetTournamentStagesCachedAsync(seasonStageId);
                _logger.LogInformation("GetSeasonStageGrpcClient {Elapsed}", sw.ElapsedMilliseconds);
                sw.Restart();
                if (stages is not null && stages.Items.Any())
                {
                    //var seasonStage = await _commonService.GetSeasonStageMappingByIdAsync(seasonStageId);
                    var stageIds = new List<int>();
                    stages.Items.ToList().ForEach(x => stageIds.Add(x.Id));
                    //var seasonStages = await _commonService.GetSeasonStagesAsync(seasonStage.SeasonId, stageIds);
                    //seasonStages.ToList().ForEach(x => seasonStageIds.Add(x.Id));
                }
                else
                {
                    seasonStageIds.Add(seasonStageId);
                }


                var model = new TopTeamStatDto();
                var teamsStats = await GetTeamStatsAsync(new List<int> { categoryId }, seasonStageIds, hasScore: 1);
                _logger.LogInformation("teamsStats {Elapsed}", sw.ElapsedMilliseconds);
                sw.Restart();
                if (teamsStats is not null && teamsStats.Count > 0)
                {
                    var teams = await GetTeamsCachedAsync(categoryId, seasonStageIds);
                    _logger.LogInformation("teams {Elapsed}", sw.ElapsedMilliseconds);
                    sw.Restart();
                    var teamLookup = teams.ToDictionary(t => t.Id);

                    model.Possession = BuildTopStats(teamsStats,
                                                     x => x.TeamPossession,
                                                     x => Math.Round(x.TeamPossession, 2).ToString(),
                                                     teamLookup);

                    model.Ratings = BuildTopStats(teamsStats,
                                                  x => x.Ratings,
                                                  x => Math.Round(x.Ratings, 2).ToString(),
                                                  teamLookup);

                    model.PassAccuracy = BuildTopStats(teamsStats,
                                                       x => x.PassAcc,
                                                       x => Math.Round(x.PassAcc, 2).ToString(),
                                                       teamLookup);

                    model.ShotsPerGame = BuildTopStats(
                        teamsStats,
                        x => x.ShotsPerGame,
                        x => Math.Round(x.ShotsPerGame, 2).ToString(),
                        teamLookup);

                    model.AerialDuels = BuildTopStats(
                        teamsStats,
                        x => x.AerialWonPS,
                        x => Math.Round(x.AerialWonPS, 2).ToString(),
                        teamLookup);

                    //model.Aggression = BuildTopStats(
                    //    teamsStats,
                    //    x => x.AggressionY + x.AggressionR * 2,
                    //    x => (x.AggressionY + x.AggressionR * 2).ToString(),
                    //    teamLookup);
                    model.Aggression = teamsStats.OrderByDescending(o => (o.AggressionY + o.AggressionR * 2))
                              .Take(5)
                              .Select(x =>
                              {
                                  var team = teamLookup[x.TeamId];
                                  return new StatInfo
                                  {
                                      Name = team.Name,
                                      Info = x.AggressionR.ToString(),
                                      Info2 = x.AggressionY.ToString(),
                                      SeName = team.SeName
                                  };
                              }).ToList();

                    _logger.LogInformation("stat groups {Elapsed}", sw.ElapsedMilliseconds);
                    sw.Restart();
                }
                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IList<TeamStatDto>> GetTeamStatsAsync(List<int> categoryIds,
                                                         List<int> seasonStageIds,
                                                         int lastMatches = 0, bool form = false,
                                              bool progress = false, int hasScore = 2,
                                              int filter = (int)ViewType.Overall)
        {
            var query = _db.Fixtures.AsQueryable();

            if (seasonStageIds is { Count: > 0 })
            {
                query = query.Where(f => seasonStageIds.Contains(f.SeasonStageId));
            }
            if (categoryIds is { Count: > 0 })
            {
                query = query.Where(f => f.FixtureCategories.Any(cm => categoryIds.Contains(cm.CategoryId)));
            }
            if (hasScore == 1)
            {
                query = query.Where(f => f.IsComplete);
            }
            else if (hasScore == 0)
            {
                query = query.Where(f => f.FullTime == null || f.FullTime == "vs");
            }
            var home = query.Select(f => new TeamStatRow
            {
                TeamId = f.HomeId,
                Possession = f.Home.Possession,
                PassAccuracy = f.Home.PassAccuracy,
                Rating = f.Home.Rating,
                Shots = f.Home.Shots,
                AggressionR = f.Home.AggressionR,
                AggressionY = f.Home.AggressionY,
                AerielWonPS =
        (f.Home.AerielWon + f.Away.AerielWon) == 0
            ? 0
            : (float)f.Home.AerielWon * 100 /
              (f.Home.AerielWon + f.Away.AerielWon)
                //SeName = f.Home.SeName
            });

            var away = query.Select(f => new TeamStatRow
            {
                TeamId = f.AwayId,
                Possession = f.Away.Possession,
                PassAccuracy = f.Away.PassAccuracy,
                Rating = f.Away.Rating,
                Shots = f.Away.Shots,
                AggressionR = f.Away.AggressionR,
                AggressionY = f.Away.AggressionY,
                AerielWonPS =
        (f.Home.AerielWon + f.Away.AerielWon) == 0
            ? 0
            : (float)f.Away.AerielWon * 100 /
              (f.Home.AerielWon + f.Away.AerielWon)
            });
            var stats = await home
    .Concat(away)
    .GroupBy(x => x.TeamId)
    .Select(g => new TeamStatDto
    {
        TeamId = g.Key,
        TeamPossession = g.Average(x => x.Possession),
        PassAcc = g.Average(x => x.PassAccuracy),
        Ratings = (float)g.Average(x => x.Rating),
        ShotsPerGame = (float)g.Average(x => x.Shots),
        AerialWonPS = (float)g.Average(x => x.AerielWonPS), // Placeholder for AerialWonPS
        AggressionR = (float)g.Sum(x => x.AggressionR),
        AggressionY = (float)g.Sum(x => x.AggressionY)
    })
    .ToListAsync();

            return stats;
        }
        public Task<List<TeamDto>> GetTeamAsync(int teamId, int seasonStageId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
        private async Task<GetTournamentStagesResponse> GetTournamentStagesCachedAsync(int seasonStageId)
        {
            var key = $"seasonstages:gettournamentstages:{seasonStageId}";

            var cached = await _cached.GetAsync(key);

            if (cached.HasValue )
            {
                return JsonSerializer.Deserialize<GetTournamentStagesResponse>((string)cached)!;
            }

            var response = await _seasonStageGrpcClient.GetTournamentStagesAsync(new GetTournamentStagesRequest { SeasonStageId = seasonStageId });

            await _cached.SetAsync(
                key,
                JsonSerializer.Serialize(response),
                TimeSpan.FromMinutes(30));

            return response;
        }
        private async Task<List<GetTeamsMapping>> GetTeamsCachedAsync(int categoryId,
                                                                      List<int> seasonStageIds)
        {
            var key = $"teams:get:{categoryId}-{string.Join(",", seasonStageIds)}";

            var cached = await _cached.GetAsync(key);

            if (cached.HasValue)
            {
                return JsonSerializer.Deserialize<List<GetTeamsMapping>>((string)cached)!;
            }

            var response = await _teamPlayerGrpcClient.GetTeamsAsync(
                new GetTeamsRequest
                {
                    Categoryid = categoryId,
                    SeasonStageIds = { seasonStageIds }
                });

            var teams = response.Items.ToList();

            await _cached.SetAsync(
                key,
                JsonSerializer.Serialize(teams),
                TimeSpan.FromMinutes(30));

            return teams;
        }
        private List<StatInfo> BuildTopStats(
                                      IEnumerable<TeamStatDto> source,
                                      Func<TeamStatDto, double> orderSelector,
                                       Func<TeamStatDto, string> infoSelector,
                                      Dictionary<int, GetTeamsMapping> lookup)
        {
            return source.OrderByDescending(orderSelector)
                         .Take(5)
                         .Select(x =>
                         {
                             var team = lookup[x.TeamId];
                             return new StatInfo
                             {
                                 Name = team.Name,
                                 Info = infoSelector(x),
                                 SeName = team.SeName
                             };
                         }).ToList();
        }


    }

}