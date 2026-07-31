using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.TeamPlayer.API.Infrastructure.EntityConfigurations;

public class TeamPlayerEntityTypeConfiguration
   : IEntityTypeConfiguration<Models.TeamPlayer>
{
    public void Configure(EntityTypeBuilder<Models.TeamPlayer> builder)
    {
        builder.ToTable("Team_Player_Mapping");

        builder.HasKey(tp => new { tp.TeamId, tp.PlayerId, tp.SeasonStageId });

        builder.HasIndex(tp => tp.PlayerId);

        builder.HasOne(tp => tp.Team)
            .WithMany(t => t.TeamPlayers)
            .HasForeignKey(tp => tp.TeamId);
        builder.HasOne(tp => tp.Player)
                .WithMany(p => p.TeamPlayers)
                .HasForeignKey(tp => tp.PlayerId);
        //builder.HasOne(tp => tp.SeasonStage)
        //        .WithMany(s => s.TeamPlayers)
        //        .HasForeignKey(tp => tp.SeasonStageId);
    }
}
