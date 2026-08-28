namespace eSport.MatchCentre.API.GraphQL.Queries.Fixtures
{
    [QueryType]
    public static partial class LeagueQueries
    {
        /// <summary>
        /// Client
        ///   │
        ///    ▼
        ///Gateway(Fusion) v16
        /// │
        ///├──────────────► MatchCentre
        ///│                  leaguePlayerStats()
        ///│
        ///└──────────────► TeamPlayer
        ///                  _entities(Player)
        /// </summary>
        public static async Task<PagedResult<LeaguePlayerAggregateDto>> GetLeaguePlayerStatsAsync(LeaguePlayerStatsFilter filter,
                                                                                                  ILeagueStatService service,
                                                                                                  CancellationToken ct)
        {

            return await service.GetLeaguePlayerStatsAsync(filter, ct);
        }
        /// <summary>
        /// for league and team stats
        /// </summary>
        public static async Task<TopPlayerStatsModel> GetTopLeaguePlayersStats(LeaguePlayerStatsFilter filter,
                                                                        ILeagueStatService service,
                                                                        CancellationToken ct)
        {
            if (!filter.CategoryIds.Any() && filter.TeamId > 0) throw new ArgumentOutOfRangeException("must have either team or category");
            if (filter.CategoryIds.Any())
            {
                var model = new TopPlayerStatsModel();
                filter.PageSize = 5;
                filter.OrderBy = LeaguePlayerOrderBy.ShotsPerGame;

                model.ShotsPerGame = await BuildTopStats(service, filter,
                    x => x.Attacking.ShotsPerGame > 0,
                    x => Math.Round(x.Attacking.ShotsPerGame, 2, MidpointRounding.AwayFromZero).ToString(),
                    x => string.Empty,
                    
                    ct);

                filter.OrderBy = LeaguePlayerOrderBy.Assists;
                model.Assist = await BuildTopStats(service, filter,
                    x => x.Attacking.Assists > 0,
                    x => x.Attacking.Assists.ToString(),
                   x => string.Empty,                    
                    ct);

                filter.OrderBy = LeaguePlayerOrderBy.Rating;
                model.Ratings = await BuildTopStats(service, filter,
                   x => x.Rating > 0,
                   x => Math.Round(x.Rating, 2, MidpointRounding.AwayFromZero).ToString(),
                   x => string.Empty,                   
                   ct);

                filter.OrderBy = LeaguePlayerOrderBy.PassAccuracy;
                model.PassAccuracy = await BuildTopStats(service, filter,
                   x => x.Passing.PassAccuracy > 0,
                   x => Math.Round(x.Passing.PassAccuracy, 2, MidpointRounding.AwayFromZero).ToString(),
                   x => string.Empty,                   
                   ct);

                filter.OrderBy = LeaguePlayerOrderBy.Aggression;
                model.Aggression = await BuildTopStats(service, filter,
                   x => x.General.Yellow > 0 || x.General.Red > 0,
                   x => x.General.Yellow.ToString(),
                   x => x.General.Red.ToString(),
                   
                   ct);

                filter.OrderBy = LeaguePlayerOrderBy.Dribbles;
                model.Dribble = await BuildTopStats(service, filter,
                   x => x.Attacking.Dribbles > 0,
                   x => Math.Round(x.Attacking.Dribbles, 2, MidpointRounding.AwayFromZero).ToString(),
                   x => string.Empty,                   
                   ct);
                return model;
            }
            throw new ArgumentOutOfRangeException("must have team or category");
        }
        private static async Task<List<StatInfo>> BuildTopStats(ILeagueStatService service,
                                                          LeaguePlayerStatsFilter filter,
                                                          Func<LeaguePlayerAggregateDto, bool> orderSelector,
                                                          Func<LeaguePlayerAggregateDto, string> infoSelector,
                                                          Func<LeaguePlayerAggregateDto, string> infoSelector2,
                                                          CancellationToken ct)
        {
            var source = await service.GetLeaguePlayerStatsAsync(filter, ct);
            return source.Items.Where(orderSelector)
                //.Take(5)
                .Select(x => new StatInfo
                {
                    PlayerId = x.PlayerId,
                    //Name = x.PlayerName,
                    Info = infoSelector(x),
                    Info2 = infoSelector2(x),
                    //SeName = playerSeName
                }).ToList();
        }
    }
}
