
namespace eSport.TeamPlayer.API.Infrastructure;

/// <remarks>
/// Add migrations using the following command inside the 'TeamPlayer.API' project directory:
///
/// dotnet ef migrations add --context CatalogContext [migration-name]
/// </remarks>
public class TeamPlayerContext : DbContext
{
    public TeamPlayerContext(DbContextOptions<TeamPlayerContext> options, IConfiguration configuration) : base(options)
    {

    }

    public DbSet<Team> Teams { get; set; } = null!;
    public  DbSet<Player> Players { get; set; } = null!;
    public  DbSet<Models.TeamPlayer> TeamPlayers { get; set; } = null!;

    public DbSet<TeamCategory> TeamCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfiguration(new TeamEntityTypeConfiguration());
        builder.ApplyConfiguration(new TeamPlayerEntityTypeConfiguration());
        builder.ApplyConfiguration(new PlayerEntityTypeConfiguration());
        builder.ApplyConfiguration(new TeamCategoryEntityTypeConfiguration());


        // Add the outbox table to this context
        //builder.UseIntegrationEventLogs();
    }
}
