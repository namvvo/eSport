using GreenDonut.Data;

namespace Media.API.GraphQL;

public static class MediaAssetDataLoader
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, MediaAsset>> GetMediaAssetsByIdAsync(
        IReadOnlyList<int> ids,
        MediaDbContext db,
        ISelectorBuilder selector,
        CancellationToken ct)
        => await db.MediaAssets
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .Select(x => x.Id, selector)
            .ToDictionaryAsync(x => x.Id, ct);
}
