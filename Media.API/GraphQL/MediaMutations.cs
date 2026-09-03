using Media.API.Services;

namespace Media.API.GraphQL;

public sealed class CreateMediaUploadInput
{
    /// <summary>
    /// team, player, news, etc...
    /// </summary>
    public required string OwnerType { get; init; }
    /// <summary>
    /// teamid, playerid, newsid.... => to generate the storage key for the object storage. The storage key is used to identify the object in the object storage.
    /// </summary>
    public required string OwnerId { get; init; }
    public required string AssetName { get; init; } // team might have logo, banner, background, player might have avatar, background, etc... => to generate the storage key for the object storage. The storage key is used to identify the object in the object storage.
    public required string OriginalFileName { get; init; }
    public required string ContentType { get; init; }
    public long Length { get; init; }
}

public sealed class MediaUploadRequest
{
    public required MediaAsset Asset { get; init; }
    public required string UploadUrl { get; init; }
}

[MutationType]
public static partial class MediaMutations
{
    public static async Task<MediaUploadRequest> CreateMediaUploadAsync(
        CreateMediaUploadInput input,
        MediaDbContext db,
        IMediaObjectStorage storage,
        CancellationToken ct)
    {
        Validate(input);
        var storageKey = $"{input.OwnerType}/{input.OwnerId}/{input.AssetName}.webp";
        var asset = new MediaAsset
        {
            StorageKey = storageKey,
            OriginalFileName = input.OriginalFileName,
            ContentType = input.ContentType,
            Length = input.Length,
            CreatedOnUtc = DateTime.UtcNow,
            Status = MediaUploadStatus.Pending
        };

        db.MediaAssets.Add(asset);
        await db.SaveChangesAsync(ct);

        var uploadUrl = await storage.CreateUploadUrlAsync(asset.StorageKey,
                                                           asset.ContentType,
                                                           ct);
        return new MediaUploadRequest
        {
            Asset = asset,
            UploadUrl = uploadUrl.ToString()
        };
    }

    public static async Task<MediaAsset> CompleteMediaUploadAsync(
        int mediaAssetId,
        MediaDbContext db,
        IMediaObjectStorage storage,
        CancellationToken ct)
    {
        var asset = await db.MediaAssets.SingleOrDefaultAsync(x => x.Id == mediaAssetId, ct)
            ?? throw new GraphQLException($"Media asset {mediaAssetId} was not found.");

        if (asset.Status == MediaUploadStatus.Completed)
            return asset;

        var metadata = await storage.GetMetadataAsync(asset.StorageKey,
                                                      ct)
            ?? throw new GraphQLException("The R2 object has not been uploaded.");

        if (metadata.Length != asset.Length || !string.Equals(metadata.ContentType, asset.ContentType, StringComparison.OrdinalIgnoreCase))
            throw new GraphQLException("The uploaded object does not match the requested media metadata.");

        asset.ETag = metadata.ETag;
        asset.UploadedOnUtc = DateTime.UtcNow;
        asset.Status = MediaUploadStatus.Completed;
        await db.SaveChangesAsync(ct);
        return asset;
    }

    public static async Task<bool> DeleteMediaAssetAsync(
        int mediaAssetId,
        MediaDbContext db,
        IMediaObjectStorage storage,
        CancellationToken ct)
    {
        var asset = await db.MediaAssets.SingleOrDefaultAsync(x => x.Id == mediaAssetId, ct);
        if (asset is null)
            return false;

        await storage.DeleteAsync(asset.StorageKey, ct);
        db.MediaAssets.Remove(asset);
        await db.SaveChangesAsync(ct);
        return true;
    }

    private static void Validate(CreateMediaUploadInput input)
    {
        if (string.IsNullOrWhiteSpace(input.OriginalFileName) || input.OriginalFileName.Length > 255)
            throw new GraphQLException("An original file name of at most 255 characters is required.");
        if (!input.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            throw new GraphQLException("Only image uploads are supported.");
        if (input.Length <= 0)
            throw new GraphQLException("Image length must be positive.");
        //if (!System.Text.RegularExpressions.Regex.IsMatch(input.StorageKey, "^(team|player)/[1-9][0-9]*/original\\.webp$", System.Text.RegularExpressions.RegexOptions.CultureInvariant))
        //    throw new GraphQLException("StorageKey must use team/{id}/original.webp or player/{id}/original.webp.");
    }
}
