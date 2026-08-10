using eSport.MatchCentre.API.Dto.League;

namespace eSport.MatchCentre.API.GraphQL.Types;

public sealed class LeaguePlayerStatsFilterType
: InputObjectType<LeaguePlayerStatsFilter>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<LeaguePlayerStatsFilter> descriptor)
    {
        descriptor.Field(x => x.PageIndex)
            .DefaultValue(0);

        descriptor.Field(x => x.PageSize)
            .DefaultValue(50);

        descriptor.Field(x => x.CountApp)
            .DefaultValue(0);

        descriptor.Field(x => x.TeamId)
            .DefaultValue(0);

        descriptor.Field(x => x.FixtureId)
            .DefaultValue(0);

        descriptor.Field(x => x.PlayerName)
            .DefaultValue("");
        descriptor.Field(x => x.TeamPosition)
           .DefaultValue("");

        descriptor.Field(x => x.CategoryIds)
            .DefaultValue(new List<int>());

        descriptor.Field(x => x.SeasonStageIds)
            .DefaultValue(0);

        descriptor.Field(x => x.PlayerIds)
            .DefaultValue(new List<int>());

        descriptor.Field(x => x.MinPlayed)
            .DefaultValue(0);

        descriptor.Field(x => x.PriceOrder)
            .DefaultValue(0);

        descriptor.Field(x => x.OnlyRegisteredPlayers)
            .DefaultValue(true);

        descriptor.Field(x => x.OrderBy)
            .DefaultValue(LeaguePlayerOrderBy.Goals);

        descriptor.Field(x => x.Direction)
            .DefaultValue(SortDirection.Desc);
    }
}
