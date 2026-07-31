using eSport.Catalog.API.Services;
using Grpc.Core;

namespace eSport.Catalog.API.Grpc;

public sealed class SeasonStageGrpcEndpoint : SeasonStageGrpc.SeasonStageGrpcBase
{
    private readonly ISeasonStageService _seasonStageService;
    public SeasonStageGrpcEndpoint(ISeasonStageService seasonStageService)
    {
        _seasonStageService = seasonStageService;
    }
    public override async Task<GetSeasonStagesResponse> GetSeasonStages(GetSeasonStagesRequest request, ServerCallContext ct)
    {
        var response = new GetSeasonStagesResponse();
        var data = await _seasonStageService.GetSeasonStagesAsync(
           ct.CancellationToken,
           request.SeasonId,
           request.StageIds.ToList()
           );
        return MapToSeasonStagesResponse(data);
    }
    public override async Task<GetTournamentStagesResponse> GetTournamentStages(GetTournamentStagesRequest request, ServerCallContext ct)
    {
        var response = new GetTournamentStagesResponse();
        var data = await _seasonStageService.GetTournamentStages(

           request.SeasonStageId,
            ct.CancellationToken
           );
        return MapToTournamentStagesResponse(data);
    }
    private static GetSeasonStagesResponse MapToSeasonStagesResponse(IList<SeasonStage> seasonStages)
    {
        var response = new GetSeasonStagesResponse();

        foreach (var seasonStage in seasonStages)
        {
            response.Items.Add(new GetSeasonStageMapping
            {
                Id = seasonStage.Id,
                SeasonId = seasonStage.SeasonId,
                StageId = seasonStage.StageId
            });
        }
        return response;
    }
    private static GetTournamentStagesResponse MapToTournamentStagesResponse(IList<Stage> stages)
    {
        var response = new GetTournamentStagesResponse();

        foreach (var stage in stages)
        {
            response.Items.Add(new GetTournamentStagesMapping
            {
                Id = stage.Id,
                C1WhoscoredName=stage.C1WhoscoredName ?? string.Empty
            });
        }
        return response;
    }
}
