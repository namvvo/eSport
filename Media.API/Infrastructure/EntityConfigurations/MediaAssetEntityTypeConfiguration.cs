namespace Media.API.Infrastructure.EntityConfigurations;

internal sealed class MediaAssetEntityTypeConfiguration
    : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> builder)
    {
        builder.ToTable("MediaAsset");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StorageKey).IsRequired().HasMaxLength(512);
        builder.HasIndex(x => x.StorageKey).IsUnique();
        builder.HasIndex(x=> new { x.OwnerId, x.OwnerType, x.AssetName }).IsUnique();

        builder.Property(x => x.OriginalFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(128);
        builder.Property(x => x.ETag).HasMaxLength(128);
        builder.Property(x => x.CreatedOnUtc).IsRequired();
    }
}
