namespace eSport.MatchCentre.API.Infrastructure.EntityConfigurations;

class RankingTemplateEntityTypeConfiguration
    : IEntityTypeConfiguration<RankingTemplate>

{
    public void Configure(EntityTypeBuilder<RankingTemplate> builder)
    {
        builder.ToTable("RankingTemplate");
        builder.HasKey(r => new { r.CategoryId, r.SeasonStageId });
    }
}