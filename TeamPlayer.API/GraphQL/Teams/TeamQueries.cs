using HotChocolate.Caching;

namespace eSport.TeamPlayer.API.GraphQL.Teams;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class TeamQueries
{
    [CacheControl(MaxAge = 900)]
    [UseProjection]
    public IQueryable<Team> GetTeams(
         [Service] TeamPlayerContext db,
          CancellationToken cancellationToken)
         => db.Teams.AsNoTracking().OrderBy(c => c.Id);

    

    [Lookup]
    public async Task<Team?> GetTeamById(TeamPlayerContext db,
                                             int id,
                                             CancellationToken ct)
   => await db.Teams.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);


}

