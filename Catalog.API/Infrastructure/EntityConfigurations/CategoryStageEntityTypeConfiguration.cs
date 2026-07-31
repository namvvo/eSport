using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.Catalog.API.Infrastructure.EntityConfigurations;

class CategoryStageEntityTypeConfiguration : IEntityTypeConfiguration<CategoryStage>
{
    public void Configure(EntityTypeBuilder<CategoryStage> builder)
    {
        builder.ToTable("Category_Stage_Mapping");

        builder.HasKey(cs => new { cs.CategoryId, cs.StageId });
        builder.HasOne(cs => cs.Stage)
            .WithMany(s => s.CategoryStages)
            .HasForeignKey(cs => cs.StageId);

        builder.HasOne(cs => cs.Category)
                .WithMany(c => c.CategoryStages)
                .HasForeignKey(cs => cs.CategoryId);
    }
}

