
namespace Media.API.Infrastructure;

public class MediaDbContext: DbContext
{
    public MediaDbContext(DbContextOptions<MediaDbContext> options, IConfiguration configuration) : base(options)
    {
    }
    public DbSet<MediaAsset> MediaAssets { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MediaAssetEntityTypeConfiguration());
    }
}
