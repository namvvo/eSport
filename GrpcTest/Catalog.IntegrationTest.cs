using Catalog.IntegrationTests;
using eSport.Catalog.API.Grpc;
using Grpc.Net.Client;
using Xunit;

[Collection(nameof(CatalogGrpcCollection))]
public sealed class SeasonStageGrpcTests
{
    private readonly GrpcFixture _catalog;

    public SeasonStageGrpcTests(GrpcFixture catalog)
    {
        _catalog = catalog;
    }

    [Fact]
    public async Task GetSeasonStages_Should_Return_Data()
    {
        var response = await _catalog.Client.GetSeasonStagesAsync(
            new GetSeasonStagesRequest 
            {
                StageIds = { 1, 2, 3 }
            });

        Assert.NotNull(response);
        Assert.NotEmpty(response.Items);
    }
}
[CollectionDefinition(nameof(CatalogGrpcCollection))]
public sealed class CatalogGrpcCollection
    : ICollectionFixture<GrpcFixture>
{
}