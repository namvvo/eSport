using eSport.MatchCentre.API.Dto.League;
using System.Diagnostics;

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
       
    }
}
