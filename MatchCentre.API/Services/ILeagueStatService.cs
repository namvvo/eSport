using eSport.MatchCentre.API.Dto.League;

namespace eSport.MatchCentre.API.Services
{
    public interface ILeagueStatService
    {
        Task<PagedResult<LeaguePlayerAggregateDto>>
        GetLeaguePlayerStatsAsync(
            LeaguePlayerStatsFilter filter,
            CancellationToken ct);
    }
}
