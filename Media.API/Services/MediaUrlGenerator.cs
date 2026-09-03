using Media.API.Models;
using Media.API.Options;
using Microsoft.Extensions.Options;

namespace Media.API.Services;

public sealed class MediaUrlGenerator(IOptions<R2Options> options)
{
    private readonly string _publicBaseUrl = options.Value.PublicBaseUrl.TrimEnd('/');

    public string GetPublicUrl(MediaAsset asset)
        => $"{_publicBaseUrl}/{string.Join('/', asset.StorageKey.Split('/').Select(Uri.EscapeDataString))}";
}
