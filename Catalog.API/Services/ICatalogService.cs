using eSport.Catalog.API.Dto;

namespace eSport.Catalog.API.Services;

public interface ICatalogService
{
    Task<Category> GetCategoryBySlugAsync(string slug);
    Task<Category> GetCategoryByIdAsync(int id, CancellationToken ct);
    Task<IList<CategoryMenuDto>> GetMenuCategoriesAsync(CancellationToken ct);
    Task<IList<Category>> GetCategoriesAsync(bool isData = false);
    Task<LeagueCalendarDto> GetCurrentRoundLeagueCalendar(int seasonStageId, int categoryId, CancellationToken ct = default);
}
