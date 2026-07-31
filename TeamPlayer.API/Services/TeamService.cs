namespace eSport.TeamPlayer.API.Services;

public partial class TeamService : ITeamService
{
    private readonly TeamPlayerContext _db;
    public TeamService(TeamPlayerContext db) { _db = db; }
    public async Task<IList<Team>> GetTeamsAsync(int categoryId, IList<int> seasonStageIds)
    {
        var teamCategories = _db.TeamCategories
                 .AsNoTracking()
                 .Where(tc => tc.CategoryId == categoryId);

        if (seasonStageIds.Any() && seasonStageIds[0] > 0)
            teamCategories = teamCategories.Where(q => seasonStageIds.Contains(q.SeasonStageId));

        return await teamCategories.Select(x => x.Team).Distinct().ToListAsync();
    }
}
