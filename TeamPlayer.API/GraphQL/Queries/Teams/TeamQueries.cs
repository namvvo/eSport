using eSport.TeamPlayer.API.Dto.Teams;
using eSport.TeamPlayer.API.GraphQL.Queries.Teams;
using GreenDonut.Data;

namespace eSport.TeamPlayer.API.GraphQL.Teams;

//[ExtendObjectType(OperationTypeNames.Query)]
[QueryType]
public static partial class TeamQueries
{
    //[CacheControl(MaxAge = 900)]
    //[UseProjection]
    //public static IQueryable<Team> GetTeams2(
    //      TeamPlayerContext db,
    //      CancellationToken cancellationToken)
    //     => db.Teams.AsNoTracking().OrderBy(c => c.Id);
    [UseProjection]
    public static IQueryable<Team> GetTeams(TeamPlayerContext db,
        int categoryId,
                                            int seasonStageId,
                                            int pageIndex,
                                            int pageSize)
    {
        var query = from t in db.Teams
                    join tc in db.TeamCategories
                    on t.Id equals tc.TeamId
                    where tc.SeasonStageId == seasonStageId && tc.CategoryId == categoryId
                    orderby tc.Rank 
                    select t;
        return query.Skip((pageIndex - 1) * pageSize)
                     .Take(pageSize);
       

    }



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

