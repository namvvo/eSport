namespace eSport.TeamPlayer.API.GraphQL.Types
{
    public sealed class PlayerObjectType : ObjectType<Player>
    {
        protected override void Configure(IObjectTypeDescriptor<Player> descriptor)
        {

            // Makes an individual field shareable across subgraphs
            descriptor.Field(p => p.Id)
                .Shareable();
        }
    }
}
