using eSport.TeamPlayer.API.GraphQL.Queries.Players;
using GreenDonut.Data;
using Microsoft.Extensions.Options;

namespace eSport.TeamPlayer.API.GraphQL.Players;

//[ExtendObjectType(OperationTypeNames.Query)]
[QueryType]
public static partial class PlayerQueries
{
    [Lookup]
    public async static Task<Player?> GetPlayerByIdAsync(
                   int id,
                   IPlayersByIdDataLoader loader,
                    CancellationToken ct)
    {

        return await loader.LoadAsync(id, ct);
    }

}
