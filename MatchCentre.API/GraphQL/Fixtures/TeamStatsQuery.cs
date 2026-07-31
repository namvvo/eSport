using eSport.MatchCentre.API.Dto;

namespace eSport.MatchCentre.API.GraphQL.Fixtures;

[QueryType]
public static partial class TeamStatsQuery
{
    //public static Task<TopTeamStatDto> TopTeamStats(
    //    int categoryId,
    //    int seasonStageId,
    //    [Service] IFixtureService queries,
    //    CancellationToken ct)
    //    => queries.GetTopTeamStatsAsync(categoryId, seasonStageId, ct);
    public static async Task<TopTeamStatDto> GetTeamsStatsByLeague(FixtureContext db,
                                                           [Service] ITeamService service,
                                                           int categoryId,
                                                           int seasonStageId,
                                                           CancellationToken ct)
    {
        try
        {
            return await service.GetTopTeamStatsAsync(categoryId, seasonStageId, ct);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
