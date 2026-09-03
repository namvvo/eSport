namespace Media.API.GraphQL;

[QueryType]
public static partial class MediaQueries
{
    [Lookup]
    public static Task<MediaAsset?> GetMediaAssetByIdAsync(
        int id,
        MediaAssetsByIdDataLoader loader,
        CancellationToken ct)
        => loader.LoadAsync(id, ct);
}
