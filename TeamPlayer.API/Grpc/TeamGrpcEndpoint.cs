using Grpc.Core;

namespace eSport.TeamPlayer.API.Grpc;

public sealed class TeamGrpcEndpoint : TeamPlayerGrpc.TeamPlayerGrpcBase
{
    private readonly ITeamService _teamService;
    public TeamGrpcEndpoint(ITeamService teamService)
    {
        _teamService = teamService;
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
}
