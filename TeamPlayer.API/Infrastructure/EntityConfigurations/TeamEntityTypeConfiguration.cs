using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.TeamPlayer.API.Infrastructure.EntityConfigurations;

class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team");
        builder.Property(t => t.Name)
           .HasMaxLength(50);

        builder.HasIndex(t => t.Name);
        builder.HasMany(t => t.Players)
            .WithMany(t => t.Teams)
            .UsingEntity<Models.TeamPlayer>();

    }
}

