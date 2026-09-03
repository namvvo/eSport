using Media.API.Services;

namespace Media.API.GraphQL;

public sealed class MediaAssetType : ObjectType<MediaAsset>
{
    protected override void Configure(IObjectTypeDescriptor<MediaAsset> descriptor)
    {
        descriptor.Field(x => x.Id).Shareable();
        descriptor.Field("url")
            .Type<NonNullType<StringType>>()
            .Resolve(context => context.Service<MediaUrlGenerator>().GetPublicUrl(context.Parent<MediaAsset>()));
    }
}
