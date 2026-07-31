
namespace eSport.MatchCentre.API.Infrastructure.EntityConfigurations;

class FixtureEntityTypeConfiguration
   : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.ToTable("Fixture");

        builder.Property(o => o.Status)
           .HasConversion<string>()
           .HasMaxLength(10);

        builder.ComplexProperty(f => f.Home);
        builder.ComplexProperty(f => f.Away);

        builder.HasMany(f => f.FixtureStats)
            .WithOne(f => f.Fixture)
            .HasForeignKey(f => f.FixtureId)
            .IsRequired();

        builder.HasMany(f => f.FixtureCategories)
            .WithOne(fc => fc.Fixture)
            .HasForeignKey(fc => fc.FixtureId)
            .IsRequired();


        builder.HasMany(f=>f.FixtureComments)
            .WithOne(fc => fc.Fixture)
            .HasForeignKey(fc => fc.FixtureId)
            .IsRequired();
    }
}


