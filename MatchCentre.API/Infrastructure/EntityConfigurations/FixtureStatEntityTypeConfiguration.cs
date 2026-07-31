namespace eSport.MatchCentre.API.Infrastructure.EntityConfigurations;

class FixtureStatEntityTypeConfiguration : IEntityTypeConfiguration<FixtureStat>
{
    public void Configure(EntityTypeBuilder<FixtureStat> builder)
    {
        builder.ToTable("FixtureStats");
    }
}