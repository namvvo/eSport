using eSport.Catalog.API.Services;
using HotChocolate.Caching;
using Pgvector;
using System.Text.Json;

namespace eSport.Catalog.API.GraphQL.Categories;

//[ExtendObjectType(OperationTypeNames.Query)]
[QueryType]
public static partial class CategoryQueries
{
    [CacheControl(MaxAge = 900)]
    //[UsePaging]       // Step 1: Handle Pagination (if used)
    [UseProjection]   // Step 2: Project required fields
    //[UseFiltering]    // Step 3: Apply filter arguments
    //                  //[UseSorting]      // Step 4: Apply sort order

    public static IQueryable<Category> GetCategories(
          CatalogContext db)
         => db.Categories.AsNoTracking().OrderBy(c => c.Id);

    //public static Category? GetCategoryById(int id, CatalogContext db)
    //    => db.Categories.Find(id);
    //[UseFirstOrDefault]
    //[UseProjection]

    //public static IQueryable<Category> GetCategoryById(CatalogContext db,
    //                                            int id)
    //=> db.Categories.AsNoTracking().Where(c => c.Id == id);

    [Lookup]
    public static async Task<Category?> GetCategoryById(CatalogContext db,
                                                 int id,
                                                 CancellationToken ct)
     => await db.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);


    public static async Task<IList<CategoryMenuDto>> GetMenuCategories(
        [Service] ICatalogService service,
        CancellationToken ct)
    {
        return await service.GetMenuCategoriesAsync(ct);
    }
    public static async Task<LeagueCalendarDto> GetCurrentRoundLeagueCalendar(int seasonStageId,
                                                                              int categoryId,
                                                                              [Service] ICatalogService service,
                                                                              CancellationToken ct)
    { 
        return await service.GetCurrentRoundLeagueCalendar(seasonStageId,categoryId, ct);
    }
}
