using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.Catalog.API.Infrastructure.EntityConfigurations;

class SeasonEntityTypeConfiguration
   : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable("Season");

        builder.HasMany(s => s.Stages)
            .WithMany(s => s.Seasons)
            .UsingEntity<SeasonStage>();

    }
}


