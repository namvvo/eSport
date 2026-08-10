using eSport.MatchCentre.API.Dto.League;

namespace eSport.MatchCentre.API.Services;
public static class LeaguePlayerAggregateMapper
{
    public static LeaguePlayerAggregateDto Map(
    LeaguePlayerAggregateRow row)
    {
        return new LeaguePlayerAggregateDto
        {
            PlayerId = row.PlayerId,
            TeamOwnerId = row.TeamOwnerId,
            CategoryId = row.CategoryId,

            Rating = row.Rating,
            Apps = row.Apps,
            MinPlayed = row.MinPlayed,

            General = new GeneralPlayerStats
            {
                Yellow = row.Yellow,
                Red = row.Red,
                Subs = row.Subs,
                Motm = row.Motm
            },

            Attacking = new AttackingStats
            {
                Goals = row.Goals,
                PenGoals = row.PenGoals,
                Assists = row.Assists,
                ShotsPerGame = row.ShotsPerGame,
                ShotsOnTarget = row.ShotsOT,
                Dribbles = row.Dribbles,
                KeyPasses = row.KeyPasses,
                Fouled = row.Fouled,
                Offsides = row.Offsides,
                TeamGoals = row.TeamGoals
            },

            Passing = new PassingStats
            {
                PSPercentage = row.PSPercentage,
                Passes = row.Passes,
                AccCrosses = row.AccCrosses,
                Crosses = row.Crosses,
                LongBalls = row.LongBalls,
                AvgP = row.AvgP,
                ThroughBalls = row.ThroughBalls,
                //PassAccuracy = row.
            },

            Defending = new DefendingStats
            {
                Tackles = row.Tackles,
                Interceptions = row.Interceptions,
                Clearances = row.Clearances,
                Blocks = row.Blocks,
                AerielWon = row.AerielsWon,
                Dispossessed = row.Dispossessed,
                Fouls = row.Fouls,
                OwnGoals = row.OwnGoals,

            },

            Goalkeeping = new GoalkeepingStats
            {
                Saves = row.Saves,
                //GoalsConceded = row.GoalsConceded,
                //CleanSheets = row.CleanSheets,
                //PenSaves = row.PenS
            }
        };
    }
}
