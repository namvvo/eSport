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
            TeamGoals = row.TeamGoals,
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

                ShotsPerGame = Math.Round(row.ShotsPerGame, 2),
                ShotsOnTarget = Math.Round(row.ShotsOT, 2),
                Dribbles = Math.Round(row.Dribbles, 2),
                KeyPasses = Math.Round(row.KeyPasses, 2),
                Fouled = Math.Round(row.Fouled, 2),
                Offsides = Math.Round(row.Offsides, 2),
            },

            Passing = new PassingStats
            {
                PSPercentage = Math.Round(row.AccuratePassingPercentage, 2),
                Passes = row.Passes,
                AccCrosses = Math.Round(row.AccCrosses, 2),
                Crosses = Math.Round(row.Crosses, 2),
                LongBalls = Math.Round(row.LongBalls, 2),
                AvgP = Math.Round(row.AvgP, 2),
                ThroughBalls = Math.Round(row.ThroughBalls, 2),
                PassAccuracy = Math.Round(row.AccPasses,2)
            },

            Defending = new DefendingStats
            {
                Tackles = Math.Round(row.Tackles, 2),
                Interceptions = Math.Round(row.Interceptions, 2),
                Clearances = Math.Round(row.Clearances, 2),
                Blocks = Math.Round(row.Blocks, 2),
                AerielWon = Math.Round(row.AerielsWon, 2),
                Dispossessed = Math.Round(row.Dispossessed, 2),
                Fouls = Math.Round(row.Fouls, 2),
                OwnGoals = row.OwnGoals,
            },

            Goalkeeping = new GoalkeepingStats
            {
                Saves = Math.Round(row.Saves, 2),
                //GoalsConceded = row.GoalsConceded,
                //CleanSheets = row.CleanSheets,
                //PenSaves = row.PenS
            }
        };
    }

    
}
