using eSport.MatchCentre.API.GraphQL.Queries.Fixtures;

namespace eSport.MatchCentre.API.GraphQL.Extensions;
/// <summary>
/// 
/// </summary>
[ExtendObjectType<StatInfo>]
public static partial class LeaguePlayerStatsExtensions
{
   
    public static PlayerReference GetPlayer([Parent] StatInfo parent)
    => new()
    {
        Id = parent.PlayerId
    };

}
