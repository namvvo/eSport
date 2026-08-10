namespace eSport.Catalog.API.Services;

public interface ICatalogService
{
    Task<Category> GetCategoryBySlugAsync(string slug);
    Task<Category> GetCategoryByIdAsync(int id);
    Task<IList<CategoryMenuDto>> GetMenuCategoriesAsync(CancellationToken ct);
    Task<IList<Category>> GetCategoriesAsync(bool isData = false);
    
}
