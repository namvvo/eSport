namespace eSport.TeamPlayer.API.Services;

public interface ITeamService
{
    Task<IList<Team>> GetTeamsAsync(int categoryId, IList<int> seasonStageIds);
   
}
