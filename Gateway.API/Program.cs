using System.Net;
using Gateway.API;
using HotChocolate.PersistedOperations.Redis;
var builder = WebApplication.CreateBuilder(args);
// 1. Đăng ký Redis Distributed Cache từ .NET Aspire
// Hàm này tự động đẩy IDistributedCache vào DI Container
builder.AddRedisDistributedCache("redis");

builder.Services
    .AddHttpClient("fusion")
    .ConfigureHttpClient(client =>
    {
        // Ép HttpClient sử dụng HTTP/2 để giữ kết nối cực nhanh sang Subgraph
        client.DefaultRequestVersion = HttpVersion.Version20;
        client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
    }); ;

var connectionString = builder.Configuration.GetConnectionString("redis");

// 2. Sử dụng hàm chuẩn của Microsoft.Extensions.Caching.StackExchangeRedis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = connectionString;
});

var gatewayBuilder = builder
    .AddGraphQLGateway()
    .AddFileSystemConfiguration("./gateway.far")
    .ModifyRequestOptions(o => o.CollectOperationPlanTelemetry = true)
      .ModifyServerOptions(options =>
      {
          options.MaxConcurrentExecutions = 128;
      }); ;
if (builder.Environment.IsProduction())
{
    gatewayBuilder.UseAutomaticPersistedOperationPipeline();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGraphQL();
}
else
//app.MapGraphQLHttp();
{
    app.UseMiddleware<GraphQLResponseCacheMiddleware>();
    app.MapGraphQLPersistedOperations();
   
}
///// 1. Map Persisted Operations first (High Priority)
// This will intercept the request, check if it's a valid ID, 
// and execute the pre-compiled plan.
//app.MapGraphQLPersistedOperations();

// 2. Map standard HTTP (Fallback/Dev)
// This remains active for your IDEs, ad-hoc queries, 
// and as a fallback if a Persisted ID is not found.
//app.MapGraphQLHttp();


app.Run();