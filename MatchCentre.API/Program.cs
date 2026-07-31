[assembly: Module("MatchCentreTypes")]
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddApplicationServices();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGraphQL();
app.Run();
//await app.RunWithGraphQLCommandsAsync(args);