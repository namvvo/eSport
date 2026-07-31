using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.Catalog.API.Infrastructure.EntityConfigurations;

public class StageEntityTypeConfiguration
   : IEntityTypeConfiguration<Stage>
{
    public void Configure(EntityTypeBuilder<Stage> builder)
    {
        builder.ToTable("Stage");

        builder.Property(ci => ci.Name)
            .HasMaxLength(50);

    
        builder.HasIndex(ci => ci.Name);
    }
}
