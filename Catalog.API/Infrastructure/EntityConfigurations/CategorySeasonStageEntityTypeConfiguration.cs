using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.Catalog.API.Infrastructure.EntityConfigurations;

class CategorySeasonStageEntityTypeConfiguration : IEntityTypeConfiguration<CategorySeasonStage>
{
    public void Configure(EntityTypeBuilder<CategorySeasonStage> builder)
    {
        builder.ToTable("Category_SSM_Mapping");

        builder.HasKey(cs => new {  cs.CategoryId, cs.SeasonStageId });
        builder.HasOne(cs => cs.Category)
            .WithMany(s => s.CategorySeasonStages)
            .HasForeignKey(cs => cs.CategoryId);

        builder.HasOne(cs => cs.SeasonStage)
            .WithMany(s => s.CategorySeasonStages)
            .HasForeignKey(cs => cs.SeasonStageId);

        builder.Property(cs => cs.CompleteRound)
            .HasDefaultValue(0);
        builder.Property(cs => cs.FromMonth)
          .HasDefaultValue(0);
        builder.Property(cs => cs.ToMonth)
          .HasDefaultValue(0);
        builder.Property(cs => cs.FixtureCount)
          .HasDefaultValue(0);
       
    }
}

