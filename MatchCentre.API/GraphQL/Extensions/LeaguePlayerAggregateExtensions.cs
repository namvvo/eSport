using eSport.MatchCentre.API.GraphQL.Queries.Fixtures;

namespace eSport.MatchCentre.API.GraphQL.Extensions;
/// <summary>
/// 
/// </summary>
[ExtendObjectType<LeaguePlayerAggregateDto>]
public static partial class LeaguePlayerAggregateExtensions
{
    //[GraphQLName("player")]
    public static PlayerReference GetPlayer([Parent] LeaguePlayerAggregateDto parent)
    => new()
    {
        Id = parent.PlayerId
    };
    //[GraphQLName("team")]
    public static TeamReference GetTeam(
        [Parent] LeaguePlayerAggregateDto parent)
        => new()
        {
            Id = parent.TeamOwnerId
        };
}
