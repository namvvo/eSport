namespace eSport.TeamPlayer.API.GraphQL.Media;

/// <summary>
/// A Fusion entity reference only. TeamPlayer stores the scalar ID; Media owns the asset.
/// </summary>
[ObjectType("MediaAsset")]
public sealed class MediaAssetReference
{
    [Shareable]
    public int Id { get; init; }
}
