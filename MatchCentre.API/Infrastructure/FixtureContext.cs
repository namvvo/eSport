using eSport.MatchCentre.API.Infrastructure.EntityConfigurations;

namespace eSport.MatchCentre.API.Infrastructure;

public class FixtureContext : DbContext
{
    public FixtureContext(DbContextOptions<FixtureContext> options, IConfiguration configuration) : base(options)
    {

    }

    public DbSet<Fixture> Fixtures { get; set; } = null!;
    public DbSet<RankingTemplate> RankingTemplates { get; set; } = null!;
    public DbSet<FixtureCategory> FixtureCategories { get; set; } = null!;

    //public DbSet<FixtureComment> FixtureComment { get; set; } = null!;
    public DbSet<FixtureStat> FixtureStat { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new FixtureEntityTypeConfiguration());
        builder.ApplyConfiguration(new FixtureCategoryEntityTypeConfiguration());
        builder.ApplyConfiguration(new FixtureStatEntityTypeConfiguration());
        //builder.ApplyConfiguration(new FixtureCommentEntityTypeConfiguration());
        builder.ApplyConfiguration(new RankingTemplateEntityTypeConfiguration());
        // Add the outbox table to this context
        //builder.UseIntegrationEventLogs();
    }
}
