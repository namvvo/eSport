[assembly: Module("MediaTypes")]

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddApplicationServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();
//await app.RunWithGraphQLCommandsAsync(args);
