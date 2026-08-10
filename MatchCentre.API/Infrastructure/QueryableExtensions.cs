using System.Linq.Expressions;

namespace eSport.MatchCentre.API.Infrastructure;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderByDirection<T, TKey>(
        this IQueryable<T> query,
        Expression<Func<T, TKey>> keySelector,
        SortDirection direction)
    {
        return direction == SortDirection.Desc
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);
    }
}
