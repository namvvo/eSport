using GreenDonut.Data;

namespace eSport.TeamPlayer.API.GraphQL.Queries.Teams;

public static class TeamDataLoader
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Team>> GetTeamsByIdAsync(
        IReadOnlyList<int> ids,
        TeamPlayerContext db,
        ISelectorBuilder selector,
        CancellationToken ct)
    {
        return await db.Teams
            .Where(x => ids.Contains(x.Id))
            .Select(x => x.Id, selector)
            .ToDictionaryAsync(x => x.Id, ct);
    }
}
