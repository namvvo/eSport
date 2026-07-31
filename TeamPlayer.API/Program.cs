using eSport.TeamPlayer.API.Grpc;

[assembly: Module("TeamPlayerTypes")]
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGrpcService<TeamGrpcEndpoint>();
app.MapGraphQL();

app.Run();
//await app.RunWithGraphQLCommandsAsync(args);