using Amazon.S3;
using Amazon.S3.Model;

namespace Media.API.Services;

public class R2StorageService
{
    private readonly IAmazonS3 _s3;
    private readonly string _bucket;

    public R2StorageService(
        IAmazonS3 s3,
        IConfiguration configuration)
    {
        _s3 = s3;
        _bucket = configuration["R2:BucketName"]
            ?? throw new InvalidOperationException("R2:BucketName is missing");
    }

    public async Task<string> UploadAsync(
        Stream stream,
        string key,
        string contentType,
        CancellationToken ct = default)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucket,
            Key = key,
            InputStream = stream,
            ContentType = contentType,

            // Required by Cloudflare R2 with AWSSDK.S3
            DisablePayloadSigning = true,
            DisableDefaultChecksumValidation = true
        };

        var response = await _s3.PutObjectAsync(request, ct);

        return response.ETag;
    }

    public async Task<List<string>> ListAsync(
        CancellationToken ct = default)
    {
        var result = await _s3.ListObjectsV2Async(
            new ListObjectsV2Request
            {
                BucketName = _bucket
            },
            ct);

        return result.S3Objects
            .Select(x => x.Key)
            .ToList();
    }
}
