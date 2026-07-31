namespace eSport.MatchCentre.API.Infrastructure.EntityConfigurations;

class FixtureCategoryEntityTypeConfiguration : IEntityTypeConfiguration<FixtureCategory>
{
    public void Configure(EntityTypeBuilder<FixtureCategory> builder)
    {
        builder.ToTable("Fixture_Category_Mapping");
    }
}