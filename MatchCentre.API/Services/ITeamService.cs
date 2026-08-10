

using eSport.MatchCentre.API.Dto.Stats;

namespace eSport.MatchCentre.API.Services;

public interface ITeamService
{
    Task<IList<TeamStatDto>> GetTeamStatsAsync(List<int> categoryIds, List<int> seasonStageIds,
                                        int lastMatches = 0, bool form = false,
                                              bool progress = false, int hasScore = 2,
                                              int filter = (int)ViewType.Overall);
    Task<TopTeamStatDto> GetTopTeamStatsAsync(int categoryId, int seasonStageId, CancellationToken ct);
    Task<List<TeamDto>> GetTeamAsync(int teamId, int seasonStageId, CancellationToken ct);
}
