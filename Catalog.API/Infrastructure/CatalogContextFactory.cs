using Microsoft.EntityFrameworkCore.Design;

namespace eSport.Catalog.API.Infrastructure;

//public class CatalogContextFactory
//    : IDesignTimeDbContextFactory<CatalogContext>
//{
//    public CatalogContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder =
//            new DbContextOptionsBuilder<CatalogContext>();

//        optionsBuilder.UseNpgsql(
//            "Host=localhost;Port=5432;Database=CatalogDb;Username=postgres;Password=postgres",
//            o=>o.UseVector());

//        var configuration =
//           new ConfigurationBuilder().Build();
//        return new CatalogContext(optionsBuilder.Options, configuration);
//    }
//}
