namespace eSport.TeamPlayer.API.GraphQL.Types;

public sealed class TeamObjectType : ObjectType<Team>
{
    protected override void Configure(IObjectTypeDescriptor<Team> descriptor)
    {
        descriptor.Field(p => p.Id).Shareable();
        // The legacy string remains persisted but Fusion exposes logo as MediaAsset.
        descriptor.Ignore(p => p.Logo);
    }
}
