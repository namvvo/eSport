using Grpc.Core;

namespace eSport.TeamPlayer.API.Grpc;

public sealed class TeamGrpcEndpoint : TeamPlayerGrpc.TeamPlayerGrpcBase
{
    private readonly ITeamService _teamService;
    private readonly TeamPlayerContext _db;
    public TeamGrpcEndpoint(ITeamService teamService, TeamPlayerContext db)
    {
        _teamService = teamService;
        _db = db;
    }
    public override async Task<GetTeamsResponse> GetTeams(GetTeamsRequest request, ServerCallContext context)
    {
        var response = new GetTeamsResponse();
        var teams = await _teamService.GetTeamsAsync(request.Categoryid,
            request.SeasonStageIds.ToList());
        // Populate the response with the retrieved data
        foreach (var team in teams)
        {
            response.Items.Add(new GetTeamsMapping
            {
                Id = team.Id,
                Name = team.Name,
                SeName = team.SeName
            });
        }
        return response;
    }
    /// <summary>
    /// teamcategories contains the performance records of teams in different leagues and seasons. 
    /// It carries seasonstageid, categoryid,  teamid 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GetSeasonStageIdsByCategoryResponse> GetSeasonStageIdsByCategory(
        GetSeasonStageIdsByCategoryRequest request,
        ServerCallContext context)
    {
        var seasonStageIds = await _db.TeamCategories
            .AsNoTracking()
            .Where(x => x.CategoryId == request.CategoryId)
            .Select(x => x.SeasonStageId)
            .Distinct()
            .ToListAsync(context.CancellationToken);

        var response = new GetSeasonStageIdsByCategoryResponse();
        response.SeasonStageIds.AddRange(seasonStageIds);

        return response;
    }
    /// <summary>
    /// GetRegisteredPlayerIdsBySeasonStages retrieves the registered player IDs for the specified season stages and player IDs.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GetRegisteredPlayerIdsResponse> GetRegisteredPlayerIdsBySeasonStages(GetRegisteredPlayerIdsRequest request,
        ServerCallContext context)
    {
        var playerIds = await _db.TeamPlayers
    .AsNoTracking()
    .Where(x => x.Status &&
                request.SeasonStageIds == x.SeasonStageId)
    .Select(x => x.PlayerId)
    .Distinct()
    .ToListAsync(context.CancellationToken);
        var response = new GetRegisteredPlayerIdsResponse();
      
        response.RegisteredPlayerIds.AddRange(playerIds);
        return response;
    }
}
