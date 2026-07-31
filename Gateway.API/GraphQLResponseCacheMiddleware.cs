
namespace Gateway.API;

using HotChocolate.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using static HotChocolate.ErrorCodes;
using static HotChocolate.Types.DirectiveNames;

public sealed class GraphQLResponseCacheMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;

    public GraphQLResponseCacheMiddleware(
        RequestDelegate next,
        IDistributedCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task Invoke(HttpContext context)
    {
        // 1. Chỉ xử lý POST request tới endpoint /graphql
        if (context.Request.Path != "/graphql" || !HttpMethods.IsPost(context.Request.Method))
        {
            await _next(context);
            return;
        }

        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; // Đặt lại vị trí stream để HotChocolate đọc sau này

        // 2. KIỂM TRA MUTATION (Bypass Cache)
        if (IsMutation(body))
        {
            await _next(context);
            return;
        }

        // 3. XỬ LÝ CACHE CHO QUERY
        var key = CreateCacheKey(body);
        var cached = await _cache.GetStringAsync(key);

        if (cached != null)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(cached);
            return;
        }

        // 4. Bắt lại Response nếu Cache Miss
        var originalBody = context.Response.Body;
        using var ms = new MemoryStream();
        context.Response.Body = ms;

        await _next(context);

        ms.Position = 0;
        var response = await new StreamReader(ms).ReadToEndAsync();

        ms.Position = 0;
        await ms.CopyToAsync(originalBody);
        context.Response.Body = originalBody;

        // Chỉ cache nếu gọi thành công (Status 200) và không có lỗi GraphQL (tùy chọn thêm)
        if (context.Response.StatusCode == 200 && !response.Contains("\"errors\":"))
        {
            await _cache.SetStringAsync(
                key,
                response,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                });

            //WARNING: INVALIDATION OTHER SUBGRAPH CACHE KEY WHEN MUTATION HAPPENED 
            //3. Pro-Tip: "Purge by Pattern" (The Redis Advantage)
//            In your middleware, you are hashing the entire query body into a SHA256 key.This means you don't know which fixture is inside that query.

//If you want to be able to purge "all caches related to Fixture 123", do not use a single hash. Instead, use Tags or Key Prefixes:

//            Change your cache key format: Instead of just a raw SHA256, add a prefix: query: fixture: 123:{ sha256}.

//Purge by pattern: Redis supports the KEYS or SCAN command.Your Invalidation API can use SCAN to find all keys starting with query:fixture: 123:*and delete them all in one go.
            // After DB update, invalidate the cache key
            //await _cache.RemoveAsync(key);
        }
    }

    // Hàm kiểm tra Mutation siêu nhẹ
    private static bool IsMutation(string body)
    {
        try
        {
            // Dùng JsonDocument để parse cực nhanh mà không cần tạo object
            using var jsonDoc = JsonDocument.Parse(body);
            if (jsonDoc.RootElement.TryGetProperty("query", out var queryElement))
            {
                var queryText = queryElement.GetString().AsSpan().TrimStart();

                // Kiểm tra xem chuỗi query có bắt đầu bằng chữ "mutation" không
                return queryText.StartsWith("mutation", StringComparison.OrdinalIgnoreCase);
            }
        }
        catch
        {
            // Nếu JSON lỗi, coi như là Mutation để an toàn bỏ qua cache
            return true;
        }

        return false;
    }

    private static string CreateCacheKey(string body)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(body));
        return Convert.ToHexString(bytes);
    }
}
//public sealed class GraphQLResponseCacheMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly IDistributedCache _cache;

//    public GraphQLResponseCacheMiddleware(
//        RequestDelegate next,
//        IDistributedCache cache)
//    {
//        _next = next;
//        _cache = cache;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        if (context.Request.Path != "/graphql")
//        {
//            await _next(context);
//            return;
//        }

//        if (!HttpMethods.IsPost(context.Request.Method))
//        {
//            await _next(context);
//            return;
//        }

//        context.Request.EnableBuffering();

//        using var reader = new StreamReader(
//            context.Request.Body,
//            leaveOpen: true);

//        var body = await reader.ReadToEndAsync();

//        context.Request.Body.Position = 0;

//        var key = CreateCacheKey(body);

//        var cached = await _cache.GetStringAsync(key);

//        if (cached != null)
//        {
//            context.Response.ContentType = "application/json";

//            await context.Response.WriteAsync(cached);

//            return;
//        }

//        var originalBody = context.Response.Body;

//        using var ms = new MemoryStream();

//        context.Response.Body = ms;

//        await _next(context);

//        ms.Position = 0;

//        var response = await new StreamReader(ms).ReadToEndAsync();

//        ms.Position = 0;

//        await ms.CopyToAsync(originalBody);

//        context.Response.Body = originalBody;

//        if (context.Response.StatusCode == 200)
//        {
//            await _cache.SetStringAsync(
//                key,
//                response,
//                new DistributedCacheEntryOptions
//                {
//                    AbsoluteExpirationRelativeToNow =
//                        TimeSpan.FromMinutes(5)
//                });
//        }
//    }

//    static string CreateCacheKey(string body)
//    {
//        var bytes = SHA256.HashData(
//            Encoding.UTF8.GetBytes(body));

//        return Convert.ToHexString(bytes);
//    }
//}
