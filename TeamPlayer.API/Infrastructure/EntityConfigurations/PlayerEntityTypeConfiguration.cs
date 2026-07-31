using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.TeamPlayer.API.Infrastructure.EntityConfigurations;

class PlayerEntityTypeConfiguration
   : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Player");

        builder.Property(ci => ci.Name)
            .HasMaxLength(50);
        builder.HasIndex(ci => ci.Name);

    }
}


