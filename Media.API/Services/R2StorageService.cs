using Amazon.S3;
using Amazon.S3.Model;
using Media.API.Options;
using Microsoft.Extensions.Options;

namespace Media.API.Services;

public sealed class R2StorageService : IMediaObjectStorage
{
    private readonly IAmazonS3 _s3;
    private readonly R2Options _options;

    public R2StorageService(IAmazonS3 s3, IOptions<R2Options> options)
    {
        _s3 = s3;
        _options = options.Value;
    }

    public Task<Uri> CreateUploadUrlAsync(string storageKey, string contentType, CancellationToken ct = default)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _options.BucketName,
            Key = storageKey,
            Verb = HttpVerb.PUT,
            ContentType = contentType,
            Expires = DateTime.UtcNow.AddMinutes(15)
        };

        return Task.FromResult(new Uri(_s3.GetPreSignedURL(request)));
    }

    public async Task<MediaObjectMetadata?> GetMetadataAsync(string storageKey, CancellationToken ct = default)
    {
        try
        {
            var response = await _s3.GetObjectMetadataAsync(new GetObjectMetadataRequest
            {
                BucketName = _options.BucketName,
                Key = storageKey
            }, ct);

            return new MediaObjectMetadata(response.ContentLength, response.ETag, response.Headers.ContentType);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<bool> ExistsAsync(string storageKey, CancellationToken ct = default)
        => await GetMetadataAsync(storageKey, ct) is not null;

    public Task DeleteAsync(string storageKey, CancellationToken ct = default)
        => _s3.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = _options.BucketName,
            Key = storageKey
        }, ct);
}
