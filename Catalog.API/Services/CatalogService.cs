

using System.Text.Json;

namespace eSport.Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogContext _db;
        private readonly RedisCache _cached;
        private readonly ISeasonStageService _seasonStageService;

        public CatalogService(CatalogContext db,
            RedisCache cache,
            ISeasonStageService seasonStageService
            )
        {
            _db = db;
            _cached = cache;
            _seasonStageService = seasonStageService;

        }
        public async Task<LeagueCalendarDto> GetCurrentRoundLeagueCalendar(int seasonStageId, int categoryId, CancellationToken ct = default)
        {
            var data = await _db.CategorySeasonStages
        .AsNoTracking()
        .Where(c =>
            c.CategoryId == categoryId &&
            c.SeasonStageId == seasonStageId)
        .Select(c => new
        {
            c.CategoryId,
            CurrentRound = new RoundOfFixture
            {
                Round = c.CurrentRound.Round,
                Start = c.CurrentRound.Start.ToLocalTime(),
                End = c.CurrentRound.End.ToLocalTime()
            },
            c.LeagueRound
        })
        .FirstOrDefaultAsync(ct);
            if (data is null)
                return null;

            return new LeagueCalendarDto
            {
                CategoryId = data.CategoryId,
                CurrentRound = data.CurrentRound,
                Rounds = EnumerateLeagueRound(data.LeagueRound).ToList(),
                LeagueEndRound = data.LeagueRound
            };

        }

        private IEnumerable<int> EnumerateLeagueRound(int leagueRound)
        {
            // enumerate all rounds
            for (int i = 1; i <= leagueRound; i++)
                yield return i;
        }
        public async Task<Category> GetCategoryBySlugAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                throw new ArgumentException("Slug cannot be null or empty", nameof(slug));

            return await _db.Categories.FirstOrDefaultAsync(c => c.SeName == slug)
                   ?? throw new System.Collections.Generic.KeyNotFoundException($"Category with slug {slug} not found.");
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken ct)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
            return await _db.Categories.FindAsync(id, ct)
                   ?? throw new System.Collections.Generic.KeyNotFoundException($"Category {id} not found.");
        }
        /// <summary>
        /// url = slug/currentSeasonStageId
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<CategoryMenuDto>> GetMenuCategoriesAsync(CancellationToken ct)
        {
            var cached = await _cached.GetAsync("categories:menu");

            if (cached.HasValue)
            {
                return JsonSerializer.Deserialize<List<CategoryMenuDto>>(
                   (string)cached!)!;
            }

            try
            {
                var result = await _db.Categories
                        .AsNoTracking().Where(c => c.Published && !c.Deleted && c.IncludeInTopMenu)
                        .Select(c => new CategoryMenuDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            SeName = c.SeName,
                            Flag = !string.IsNullOrWhiteSpace(c.CountryCSS) ? $"fflag fflag-{c.CountryCSS.ToUpper()} ff-sm" : string.Empty,
                            CountryId = c.CountryId,
                            IsSelected = !c.IsTournament, // domestic league takes first priority
                            IsData = c.IsData,
                            IsTournament = c.IsTournament
                        }).ToListAsync(ct);
                foreach (var c in result)
                {
                    var currentSeasonStage = await _seasonStageService.GetCurrentSeasonStageByCategoryAsync(c.Id, ct);
                    if (currentSeasonStage != null)
                        c.CurrentSeasonUrl = currentSeasonStage.SeasonId <= 0 || currentSeasonStage.StageId <= 0 ? String.Empty : c.IsTournament ? $"/du-lieu/tong-quan/{c.SeName}/0" : $"/du-lieu/tong-quan/{c.SeName}/{currentSeasonStage.Id}";

                }
                await _cached.SetAsync(
                        "categories:menu",
                        JsonSerializer.Serialize(result),
                        TimeSpan.FromHours(12));

                return result;
            }
            catch (Exception e)
            {

                throw new Exception("lỗi menu");
            }
        }

        public async Task<IList<Category>> GetCategoriesAsync(bool isData = false)
        {

            return isData ? await _db.Categories.Where(w => w.IsData).ToListAsync() :
                            await _db.Categories.ToListAsync();

        }



    }
}
