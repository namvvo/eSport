using eSport.Catalog.API.GraphQL.Categories;
//using eSport.Catalog.API.GraphQL.SeasonStages;
using eSport.Catalog.API.Services;
using eSport.ServiceDefaults.APIExtensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Avoid loading full database config and migrations if startup
        // is being invoked from build-time OpenAPI generation
        if (builder.Environment.IsBuild())
        {
            builder.Services.AddDbContext<CatalogContext>();
            return;
        }

        builder.AddNpgsqlDbContext<CatalogContext>("catalogdb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(builder =>
            {
                builder.UseVector();
            });

        });
       //builder.AddRedisClient("redis");
        builder.AddRedisDistributedCache("redis");
        builder.Services
    .AddHttpClient("fusion")
    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
    {
        EnableMultipleHttp2Connections = true,
    });
        builder.Services.AddGraphQLServer("Catalog")
            .AddQueryType()
            .AddCatalogTypes()
            .AddCacheControl()
            .AddDbContextCursorPagingProvider()
        //    .UseAutomaticPersistedOperationPipeline()
        //    .AddRedisOperationDocumentStorage(_ =>
        //ConnectionMultiplexer.Connect(
        //    builder.Configuration.GetConnectionString("redis")!)
        //.GetDatabase())
            .AddPagingArguments()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
        //.AddGlobalObjectIdentification()  // relay
        //.AddMutationType<Mutations>()

   
        builder.Services.AddSingleton<RedisCache>();
        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<ISeasonStageService, SeasonStageService>();
        //// REVIEW: This is done for development ease but shouldn't be here in production
        builder.Services.AddMigration<CatalogContext>();

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

