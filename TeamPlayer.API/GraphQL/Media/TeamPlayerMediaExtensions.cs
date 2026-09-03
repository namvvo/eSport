using eSport.TeamPlayer.API.GraphQL.Media;

namespace eSport.TeamPlayer.API.GraphQL;

[ExtendObjectType<Team>]
public static class TeamPlayerMediaExtensions
{
    [GraphQLName("logo")]
    public static MediaAssetReference? GetLogo([Parent] Team team)
        => team.LogoMediaId is int id ? new MediaAssetReference { Id = id } : null;

    [GraphQLName("legacyLogoUrl")]
    public static string? GetLegacyLogoUrl([Parent] Team team) => team.Logo;

    [GraphQLName("image")]
    public static MediaAssetReference? GetImage([Parent] Player player)
        => player.ImageMediaId is int id ? new MediaAssetReference { Id = id } : null;
}
