
using eSport.ServiceDefaults.APIExtensions;
using eSport.ServiceDefaults.Infrastructure;
using StackExchange.Redis;

namespace eSport.MatchCentre.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Avoid loading full database config and migrations if startup
        // is being invoked from build-time OpenAPI generation
        if (builder.Environment.IsBuild())
        {
            builder.Services.AddDbContext<FixtureContext>();
            return;
        }

        //builder.AddNpgsqlDbContext<FixtureContext>("matchCentredb", configureDbContextOptions: dbContextOptionsBuilder =>
        //{
        //    dbContextOptionsBuilder.UseNpgsql(builder =>
        //    {
        //        builder.UseVector();
        //    });
        //});
        builder.AddRedisDistributedCache("redis");

        builder.AddNpgsqlDbContext<FixtureContext>("matchCentredb");
        builder.Services
    .AddHttpClient("fusion")
    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
    {
        EnableMultipleHttp2Connections = true,
    });
        var graphQLBuilder = builder.Services
               .AddGraphQLServer("MatchCentre")
               .AddQueryType(d => d.Name("Query"))
               .AddMatchCentreTypes()
               //.UseAutomaticPersistedOperationPipeline()
        //      .AddRedisOperationDocumentStorage(_ =>
        //ConnectionMultiplexer.Connect(
        //    builder.Configuration.GetConnectionString("redis")!).GetDatabase())
               .AddCacheControl()
               .AddProjections()
               .AddFiltering()
               .AddSorting();

        //// REVIEW: This is done for development ease but shouldn't be here in production
        //builder.Services.AddMigration<FixtureContext>();  // REVIEW: This is done for development ease but shouldn't be here in production, and when run dotnet run --project . -- schema export
        builder.Services.AddSingleton<RedisCache>();

        builder.Services.AddScoped<IFixtureService, FixtureService>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<ILeagueStatService, LeagueStatService>();
        builder.Services.AddGrpcClient<TeamPlayerGrpc.TeamPlayerGrpcClient>(o =>
        {
            o.Address = new Uri("https://localhost:7167"); // hoặc tên service trong .NET Aspire / Service Discovery
        });
        builder.Services.AddGrpcClient<SeasonStageGrpc.SeasonStageGrpcClient>(o =>
        {
            o.Address = new Uri("https://localhost:7220"); // hoặc tên service trong .NET Aspire / Service Discovery
        });
        //// Add the integration services that consume the DbContext
        //builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<CatalogContext>>();

        //builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

        //builder.AddRabbitMqEventBus("eventbus")
        //       .AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
        //       .AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();

        //builder.Services.AddOptions<CatalogOptions>()
        //    .BindConfiguration(nameof(CatalogOptions));

        //if (builder.Configuration["OllamaEnabled"] is string ollamaEnabled && bool.Parse(ollamaEnabled))
        //{
        //    builder.AddOllamaApiClient("embedding")
        //        .AddEmbeddingGenerator();
        //}
        //else if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("textEmbeddingModel")))
        //{
        //    builder.AddOpenAIClientFromConfiguration("textEmbeddingModel")
        //        .AddEmbeddingGenerator();
        //}

        //builder.Services.AddScoped<ICatalogAI, CatalogAI>();
    }
}

