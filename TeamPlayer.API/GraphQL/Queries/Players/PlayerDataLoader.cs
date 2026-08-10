using GreenDonut.Data;

namespace eSport.TeamPlayer.API.GraphQL.Queries.Players;

public static class PlayerDataLoader
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Player>> GetPlayersByIdAsync(
    IReadOnlyList<int> ids,
    TeamPlayerContext db,
    ISelectorBuilder selector,
    CancellationToken ct)
    {
        return await db.Players
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .Select(x => x.Id, selector)
            .ToDictionaryAsync(x => x.Id, ct);
    }
}
