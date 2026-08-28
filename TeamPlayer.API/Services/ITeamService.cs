using eSport.TeamPlayer.API.Dto.Teams;

namespace eSport.TeamPlayer.API.Services;

public interface ITeamService
{
    Task<IList<Team>> GetTeamsAsync(int categoryId, IList<int> seasonStageIds);
    Task<TeamStandingModel> GetTeamStandingsByCategoryAsync(int categoryId, int seasonStageId);
}
