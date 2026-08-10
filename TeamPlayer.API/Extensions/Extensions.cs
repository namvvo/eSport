using eSport.ServiceDefaults.APIExtensions;

using StackExchange.Redis;
namespace eSport.TeamPlayer.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Avoid loading full database config and migrations if startup
        // is being invoked from build-time OpenAPI generation
        if (builder.Environment.IsBuild())
        {
            builder.Services.AddDbContext<TeamPlayerContext>();
            return;
        }
        //builder.AddRedisClient("redis");
        builder.AddRedisDistributedCache("redis");

        builder.AddNpgsqlDbContext<TeamPlayerContext>("teamplayerdb");


        builder.Services
    .AddHttpClient("fusion")
    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
    {
        EnableMultipleHttp2Connections = true,
    });
        var graphQLBuilder = builder.Services
               .AddGraphQLServer("TeamPlayer")
               .AddQueryType()
               .AddTeamPlayerTypes()
                //.AddDbContextCursorPagingProvider()
               //.UseAutomaticPersistedOperationPipeline()
               //      .AddRedisOperationDocumentStorage(_ =>
               //ConnectionMultiplexer.Connect(
               //    builder.Configuration.GetConnectionString("redis")!)
               //.GetDatabase())
               .AddCacheControl()
               .AddProjections()
               .AddFiltering()
               .AddSorting();


        //    graphQLBuilder.AddRedisOperationDocumentStorage(sp =>
        //sp.GetRequiredService<StackExchange.Redis.IConnectionMultiplexer>().GetDatabase()); ;
        // REVIEW: This is done for development ease but shouldn't be here in production
        builder.Services.AddMigration<TeamPlayerContext>();
        builder.Services.AddSingleton<RedisCache>();

        builder.Services.AddScoped<ITeamService, TeamService>();
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

