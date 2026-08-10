using Microsoft.EntityFrameworkCore;
namespace eSport.ServiceDefaults.APIExtensions;

public sealed class PagedResult<T>
{
    public required IReadOnlyList<T> Items { get; init; }

    //public required int TotalRecords { get; init; }
    //public int ItemCount { get; init; }

    public required int PageIndex { get; init; }

    public required int PageSize { get; init; }

    //public int TotalPages =>
    //    (int)Math.Ceiling((double)TotalRecords / PageSize);

    public required bool HasNextPage { get; init; }
    public required bool HasPreviousPage { get; init; }
    //public bool HasPreviousPage =>
    //    PageIndex > 0;

    public static PagedResult<T> Empty(int pageIndex = 0, int pageSize = 20)
        => new()
        {
            Items = [],
            HasNextPage = false,
            HasPreviousPage = false,
            //TotalRecords = 0,
            //ItemCount = 0,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
}
public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize,
        CancellationToken ct = default)
    {
        var items = await query
       .Skip(pageIndex * pageSize)
       .Take(pageSize + 1)
       .ToListAsync(ct);

        var hasNextPage = items.Count > pageSize;

        if (hasNextPage)
            items.RemoveAt(items.Count - 1);



        return new PagedResult<T>
        {
            Items = items,
            HasNextPage = hasNextPage,
            HasPreviousPage = false,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}
