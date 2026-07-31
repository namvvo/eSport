using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSport.TeamPlayer.API.Infrastructure.EntityConfigurations;

class TeamCategoryEntityTypeConfiguration : IEntityTypeConfiguration<TeamCategory>
{
    public void Configure(EntityTypeBuilder<TeamCategory> builder)
    {
        builder.ToTable("Team_Category_Mapping");
        builder.HasKey(tc => new { tc.TeamId, tc.CategoryId, tc.SeasonStageId });
        builder.HasIndex(tcm => tcm.CategoryId);
        builder.HasOne(tc => tc.Team)
            .WithMany(t => t.TeamCategories)
            .HasForeignKey(tc => tc.TeamId);

    }
}
