using eSport.TeamPlayer.API.Dto.Teams;
using eSport.TeamPlayer.API.GraphQL.Queries.Teams;
using GreenDonut.Data;
using HotChocolate.Caching;

namespace eSport.TeamPlayer.API.GraphQL.Teams;

//[ExtendObjectType(OperationTypeNames.Query)]
[QueryType]
public static partial class TeamQueries
{
    [CacheControl(MaxAge = 900)]
    [UseProjection]
    public static IQueryable<Team> GetTeams(
         [Service] TeamPlayerContext db,
          CancellationToken cancellationToken)
         => db.Teams.AsNoTracking().OrderBy(c => c.Id);





    [Lookup]
    public static async Task<Team?> GetTeamByIdAsync(ITeamsByIdDataLoader loader,
                                             int id,
                                             CancellationToken ct)
   => await loader.LoadAsync(id, ct);
    [UseProjection]
    public static async Task<TeamStandingModel> GetTeamStandingsByCategoryAsync(
        [Service] ITeamService service,
        int categoryId,
        int seasonStageId,
        CancellationToken cancellationToken)
    {
         return await service.GetTeamStandingsByCategoryAsync(categoryId, seasonStageId);
    }
}

