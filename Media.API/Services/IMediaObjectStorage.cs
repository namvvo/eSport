namespace Media.API.Services;

public interface IMediaObjectStorage
{
    Task<Uri> CreateUploadUrlAsync(string storageKey, string contentType, CancellationToken ct = default);
    Task<MediaObjectMetadata?> GetMetadataAsync(string storageKey, CancellationToken ct = default);
    Task<bool> ExistsAsync(string storageKey, CancellationToken ct = default);
    Task DeleteAsync(string storageKey, CancellationToken ct = default);
}

public sealed record MediaObjectMetadata(long Length, string? ETag, string ContentType);
