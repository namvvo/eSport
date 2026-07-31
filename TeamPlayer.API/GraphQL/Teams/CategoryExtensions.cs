
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace eSport.TeamPlayer.API.GraphQL.Teams;

using HotChocolate.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static HotChocolate.Fusion.Rewriters.InlineFragmentOperationRewriter;

//public class TeamByIdDataLoader : BatchDataLoader<int, Team>
//{
//    private readonly IDbContextFactory<TeamPlayerContext> _dbContextFactory;

//    public TeamByIdDataLoader(
//        IDbContextFactory<TeamPlayerContext> dbContextFactory,
//        IBatchScheduler batchScheduler,
//        DataLoaderOptions? options = null)
//        : base(batchScheduler, options)
//    {
//        _dbContextFactory = dbContextFactory;
//    }

//    protected override async Task<IReadOnlyDictionary<int, Team>> LoadBatchAsync(
//        IReadOnlyList<int> keys,
//        CancellationToken cancellationToken)
//    {
//        // 1. Tạo DbContext mới cho mỗi Batch
//        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

//        // 2. Query 1 lần duy nhất lấy tất cả Team có ID nằm trong list keys
//        var teams = await dbContext.Teams
//            .Where(t => keys.Contains(t.Id))
//            .ToListAsync(cancellationToken);

//        // 3. Trả về Dictionary để DataLoader map đúng Key-Value
//        return teams.ToDictionary(t => t.Id, t => t);
//    }
//}
[ExtendObjectType("Category")] // <--- SỬA DÒNG NÀY: Báo rằng đây là phần mở rộng cho type Category
public static partial class CategoryExtensions
{

    public static async Task<IEnumerable<TeamDTO>> GetTeamsAsync(
        [Parent] CategoryStub category,
        CategoryByTeamsDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(category.Id) ?? Array.Empty<TeamDTO>();

    }
    #region temp
    //[DataLoader]
    //public static async Task<IReadOnlyDictionary<int, Team[]>> GetCategoryByTeamsAsync(
    //IReadOnlyList<int> categoryIds,
    //TeamPlayerContext db,
    //[Service] IDistributedCache redisCache, // Tiêm Redis vào DataLoader thay vì Resolver
    //CancellationToken cancellationToken)
    //{
    //    var resultDict = new Dictionary<int, Team[]>();
    //    var missingCategoryIds = new List<int>();

    //    // 1. Thử check Redis hàng loạt cho các CategoryId (Hoặc duyệt nhanh qua từng key)
    //    foreach (var id in categoryIds)
    //    {
    //        string cacheKey = $"cat_teams_ids:{id}";
    //        var cachedIds = await redisCache.GetStringAsync(cacheKey, cancellationToken);

    //        if (!string.IsNullOrEmpty(cachedIds))
    //        {
    //            // Thay vì cache nguyên object Team, ta chỉ cache danh sách ID của các Team thuộc Category đó! (Ví dụ: "1,2,35,42")
    //            var teamIds = cachedIds.Split(',').Select(int.Parse).ToList();

    //            // Lấy thông tin chi tiết các Team này (Nên có 1 TeamDataLoader riêng để cache từng Team theo ID, hoặc tạm thời chọc DB)
    //            var teams = await db.Teams.Where(t => teamIds.Contains(t.Id)).ToArrayAsync(cancellationToken);
    //            resultDict[id] = teams;
    //        }
    //        else
    //        {
    //            missingCategoryIds.Add(id); // Category nào chưa có cache thì gom lại tí nữa chọc DB 1 thể
    //        }
    //    }

    //    // 2. Nếu có Category nào hụt cache, bắn 1 câu SQL duy nhất lấy phần còn thiếu
    //    if (missingCategoryIds.Any())
    //    {
    //        var dbRelations = await db.TeamCategories
    //            .Where(tc => missingCategoryIds.Contains(tc.CategoryId))
    //            .Select(tc => new { tc.CategoryId, tc.Team })
    //            .ToArrayAsync(cancellationToken);

    //        var missingDict = dbRelations
    //            .GroupBy(r => r.CategoryId)
    //            .ToDictionary(g => g.Key, g => g.Select(x => x.Team).ToArray());

    //        foreach (var kvp in missingDict)
    //        {
    //            resultDict[kvp.Key] = kvp.Value;

    //            // 3. Lưu ngược lại vào Redis: CHỈ LƯU MẢNG ID (dạng chuỗi rút gọn, siêu nhẹ)
    //            string cacheKey = $"cat_teams_ids:{kvp.Key}";
    //            string idsString = string.Join(",", kvp.Value.Select(t => t.Id));

    //            await redisCache.SetStringAsync(cacheKey, idsString, new DistributedCacheEntryOptions
    //            {
    //                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
    //            }, cancellationToken);
    //        }
    //    }

    //    return resultDict;
    //}
    #endregion
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, TeamDTO>> GetTeamByIdAsync(
    IReadOnlyList<int> keys,
    TeamPlayerContext db,
    [Service] RedisCache cache,
    CancellationToken cancellationToken)
    {
        //var dbRedis = redis.GetDatabase();
        var resultDict = new Dictionary<int, TeamDTO>();

        var redisKeys = keys.Select(id => (RedisKey)$"team_dto:{id}").ToArray();
        var cachedValues = await cache.GetAsync(redisKeys);
      
        var missingIds = new List<int>();


           
        // 2. Điền kết quả từ Redis
        for (int i = 0; i < keys.Count; i++)
        {
            if (cachedValues[i].HasValue)
            {

                // Convert RedisValue to byte array
                //byte[] bytes = cachedValues[i];
                //resultDict[keys[i]] = JsonSerializer.Deserialize<TeamDTO>(bytes);
                resultDict[keys[i]] = JsonSerializer.Deserialize<TeamDTO>(cachedValues[i].ToString()) ?? throw new Exception($"Cache corrupted for Team ID {keys[i]}");

            }
            else
            {
                missingIds.Add(keys[i]);
            }
        }
        if (missingIds.Any())
        {
            var dbTeams = await db.Teams
                .AsNoTracking()
                .Where(t => missingIds.Contains(t.Id))
                .Select(t => new TeamDTO(t.Id, t.Name))// <--- Cực nhanh
                .ToListAsync(cancellationToken);

            foreach (var team in dbTeams)
            {
                resultDict[team.Id] = team;
                // 4. Lưu lại vào Redis
                await cache.SetAsync($"team_dto:{team.Id}", JsonSerializer.Serialize(team), TimeSpan.FromHours(1));
                //await dbRedis.StringSetAsync($"team_dto:{team.Id}", JsonSerializer.Serialize(team), TimeSpan.FromHours(1));
            }
        }
       

        return resultDict;
    }
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, TeamDTO[]>> GetCategoryByTeamsAsync(
    IReadOnlyList<int> categoryIds,
    [Service] IConnectionMultiplexer redis, // Dùng thẳng Multiplexer để lấy pipeline
    TeamPlayerContext db,
    TeamByIdDataLoader teamByIdLoader, // Inject thêm loader này
    CancellationToken cancellationToken)
    {
        var dbRedis = redis.GetDatabase();
        var resultDict = new Dictionary<int, TeamDTO[]>();

        // 1. Lấy tất cả cache trong 1 lần duy nhất (Batch GET)
        var keys = categoryIds.Select(id => (RedisKey)$"cat_teams_ids:{id}").ToArray();
        var cachedValues = await dbRedis.StringGetAsync(keys);

        var missingCategoryIds = new List<int>();

        // 2. Xử lý kết quả từ Redis
        for (int i = 0; i < categoryIds.Count; i++)
        {
            if (cachedValues[i].HasValue)
            {

                var teamIds = cachedValues[i].ToString().Split(',').Select(int.Parse).ToArray();
                // Lấy Team qua DataLoader (tối ưu hóa bởi DataLoader thứ 2)
                var teams = await teamByIdLoader.LoadAsync(teamIds, cancellationToken);
                resultDict[categoryIds[i]] = teams.Where(t => t != null).Select(t => t!).ToArray();
            }
            else
            {
                missingCategoryIds.Add(categoryIds[i]);
            }
        }

        // 3. Xử lý các phần còn thiếu bằng SQL
        if (missingCategoryIds.Any())
        {
            var dbRelations = await db.TeamCategories
                .AsNoTracking()
                .Where(tc => missingCategoryIds.Contains(tc.CategoryId))
                .GroupBy(tc => tc.CategoryId)
                .Select(g => new { CategoryId = g.Key, TeamIds = g.Select(x => x.Team.Id).ToArray() })
                .ToArrayAsync(cancellationToken);

            foreach (var item in dbRelations)
            {
                // Load từ DB vào result
                var teams = await teamByIdLoader.LoadAsync(item.TeamIds, cancellationToken);
                resultDict[item.CategoryId] = teams.Where(t => t != null).Select(t => t!).ToArray();

                // Lưu cache
                string cacheKey = $"cat_teams_ids:{item.CategoryId}";
                await dbRedis.StringSetAsync(cacheKey, string.Join(",", item.TeamIds), TimeSpan.FromMinutes(15));
            }
        }

        return resultDict;
    }
    //[DataLoader]
    //public static async Task<IReadOnlyDictionary<int, Team[]>> GetCategoryByTeamsAsync(
    //    IReadOnlyList<int> categoryIds,
    //    TeamPlayerContext db,
    //    [Service] IDistributedCache redisCache,
    //    CancellationToken cancellationToken)
    //{
    //    var relations = await db.TeamCategories
    //        .Where(tc => categoryIds.Contains(tc.CategoryId))
    //        .Select(tc => new { tc.CategoryId, tc.Team })
    //        .ToArrayAsync(cancellationToken);

    //    return relations
    //        .GroupBy(r => r.CategoryId)
    //        .ToDictionary(g => g.Key, g => g.Select(x => x.Team).ToArray());
    //}
}
// =========================================================================
// 3. MỞ RỘNG THỰC THỂ TEAM (BƠM FIELD 'categories' VÀO TEAM)
// =========================================================================
[ExtendObjectType<Team>]
public static partial class TeamExtensions
{


    public static async Task<IEnumerable<CategoryStub>> GetCategoriesAsync(
        [Parent] Team team,
        TeamByCategoriesDataLoader dataLoader,
       [Service] IDistributedCache redisCache) // Tự sinh ngầm bởi Source Generator v16
    {
        string cacheKey = $"categories_team_{team.Id}";

        // 1. Thử lấy từ Redis
        byte[]? cachedData = await redisCache.GetAsync(cacheKey);
        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<List<CategoryStub>>(cachedData)!;
        }
        // 2. Nếu không có thì gọi DataLoader (đã tối ưu SQL)
        var result = await dataLoader.LoadAsync(team.Id) ?? Array.Empty<CategoryStub>();

        // 3. Lưu vào Redis (thời gian 900s)
        await redisCache.SetAsync(cacheKey, JsonSerializer.SerializeToUtf8Bytes(result),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(900) });

        return result;

        //return await dataLoader.LoadAsync(team.Id) ?? Array.Empty<CategoryStub>();
    }
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, CategoryStub[]>> GetTeamByCategoriesAsync(
        IReadOnlyList<int> teamIds,
        TeamPlayerContext db,
        CancellationToken cancellationToken)
    {
        var relations = await db.TeamCategories
              .AsNoTracking()
            .Where(tc => teamIds.Contains(tc.TeamId))
            .Select(tc => new { tc.TeamId, tc.CategoryId })
            .ToArrayAsync(cancellationToken);

        return relations
            .GroupBy(r => r.TeamId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => new CategoryStub { Id = x.CategoryId }).ToArray()
            );
    }
}