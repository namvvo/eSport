using eSport.Catalog.API.Services;
using Grpc.Core;

namespace eSport.Catalog.API.Grpc;

public sealed class CategoryGrpcEndpoint : CategoryGrpc.CategoryGrpcBase
{
    private readonly ICatalogService _catalogService;
    public CategoryGrpcEndpoint(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }
    
    public override async Task<GetCategoryByIdResponse> GetCategoryById(GetCategoryByIdRequest request, ServerCallContext context)
    {
        var response = new GetCategoryByIdResponse();
        var category = await _catalogService.GetCategoryByIdAsync(request.CategoryId, context.CancellationToken);
        response.Category = new CategoryMapping
        {

            Id = category.Id,
            Name = category.Name,
            SeName = category.SeName,
            IsTournament = category.IsTournament,
            UefaC1 = category.UefaC1 ?? 0,
            UefaC1Qualifiers = category.UefaC1Qualifiers ?? 0,
            EuropaLeague = category.EuropaLeague ?? 0,
            EuropaLeagueQualifiers = category.EuropaLeagueQualifiers ?? 0,
            RelegationPlayOff = category.RelegationPlayOff ?? 0,
            Relegation = category.Relegation ?? 0

        };
        return response;
    }
}
