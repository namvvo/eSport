
namespace eSport.Catalog.API.Infrastructure;

/// <remarks>
/// Add migrations using the following command inside the 'Catalog.API' project directory:
///
/// dotnet ef migrations add --context CatalogContext [migration-name]
/// </remarks>
public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; } = null!;
    public  DbSet<Stage> Stages { get; set; } = null!;
    public  DbSet<CategoryStage> CategoryStages { get; set; } = null!;

    public DbSet<Season> Seasons { get; set; } = null!;
    public DbSet<SeasonStage> SeasonStages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.HasPostgresExtension("vector");
        builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        builder.ApplyConfiguration(new CategoryStageEntityTypeConfiguration());
        builder.ApplyConfiguration(new StageEntityTypeConfiguration());

        builder.ApplyConfiguration(new SeasonEntityTypeConfiguration());
        builder.ApplyConfiguration(new SeasonStageEntityTypeConfiguration());


        // Add the outbox table to this context
        //builder.UseIntegrationEventLogs();
    }
}
