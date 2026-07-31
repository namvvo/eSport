using eSport.Catalog.API.Grpc;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Catalog.IntegrationTests;

public sealed class GrpcFixture : IAsyncLifetime
{
    public SeasonStageGrpc.SeasonStageGrpcClient Client { get; private set; } = null!;

    public ValueTask InitializeAsync()
    {
        var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
        var address = config["CatalogGrpc:Address"]!;
        var channel = GrpcChannel.ForAddress(address);

        Client = new SeasonStageGrpc.SeasonStageGrpcClient(channel);

        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}