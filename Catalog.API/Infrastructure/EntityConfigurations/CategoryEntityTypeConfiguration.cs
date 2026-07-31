using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.Catalog.API.Infrastructure.EntityConfigurations;

class CategoryEntityTypeConfiguration
   : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.Property(ci => ci.Name)
            .HasMaxLength(50);

        builder.Property(ci => ci.SeName)
            .HasMaxLength(100);

        builder.Property(ci => ci.Embedding)
            .HasColumnType("vector(384)");


        builder.HasIndex(ci => ci.Name);

        builder.HasMany(c => c.Stages)
            .WithMany(c => c.Categories)  // ef core biết chiều ngược lại, ko cần làm bên stages
            .UsingEntity<CategoryStage>();
    }
}


