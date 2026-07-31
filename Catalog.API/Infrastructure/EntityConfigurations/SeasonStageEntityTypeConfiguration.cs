using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.Catalog.API.Infrastructure.EntityConfigurations;

class SeasonStageEntityTypeConfiguration : IEntityTypeConfiguration<SeasonStage>
{
    public void Configure(EntityTypeBuilder<SeasonStage> builder)
    {
        builder.ToTable("Season_Stage_Mapping");

        builder.HasKey(cs => new { cs.SeasonId, cs.StageId });
        builder.HasOne(cs => cs.Stage)
            .WithMany(s => s.SeasonStages)
            .HasForeignKey(cs => cs.StageId);

        builder.HasOne(cs => cs.Season)
                .WithMany(c => c.SeasonStages)
                .HasForeignKey(cs => cs.SeasonId);
    }
}
