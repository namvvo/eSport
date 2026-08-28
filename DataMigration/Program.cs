
using Catalog;
using LinqToDB;
using MatchCentre;
using SqlServer;
var connectionString = "Server=HOME;Database=thethaoso;User Id=sa;Password=123456;TrustServerCertificate=True;";

var options = new DataOptions()
    .UseSqlServer(connectionString);

using var source = new SqlServer.TTS(options);
var targetOptions = new DataOptions().UsePostgreSQL("Host=localhost;Database=teamPlayerdb;Port=56910;Username=postgres;Password=C2NY}8T_nq.gGsh5GU(k4_");
using var teamPlayer = new TeamPlayer.db(targetOptions);
var targetOptions2 = new DataOptions().UsePostgreSQL("Host=localhost;Database=matchCentredb;Port=56910;Username=postgres;Password=C2NY}8T_nq.gGsh5GU(k4_");
using var matchCentre = new MatchCentre.db(targetOptions2);

var targetOptions3 = new DataOptions().UsePostgreSQL("Host=localhost;Database=catalogdb;Port=56910;Username=postgres;Password=C2NY}8T_nq.gGsh5GU(k4_");
using var catalog = new Catalog.db(targetOptions3);
var currentSSM = currentSeasonStage(24, 66);
var fixtures = matchCentre.Fixtures
 .Where(f =>
     f.SeasonStageId == currentSSM.SeasonStageId &&
     f.Time <= DateTime.Now.AddDays(1) &&
     f.CategoryMappingFixtureIds.Any(fc =>
         fc.CategoryId == 24))
 .OrderBy(f => f.Time);
var currentRoundFixtures = fixtures.ToList()
            .GroupBy(g => g.Round)
            .OrderByDescending(o => o.Key);
Console.ReadKey(); 
//UpdateCategorySSM();
//updateTotalGoals();
void UpdateCategorySSM()
{
    var catSSM = new List<Category_Stage_Mapping>
    {
         new(24, 8,5,380,1,2,38),
          new(27, 8,5,380,1,2,38),
           new(30, 8,5,306,1,2,34),
            new(33, 8,5,380,1,2,380),
        new(36, 8,5,380,1,2,380),

    };
    foreach (var item in catalog.CategorySsmMappings.ToList())
    {
        var ssm = catSSM.FirstOrDefault(f => f.categoryId == item.CategoryId);
        if (ssm is not null)
        {
            var currentRound = computeCurrentRoundStartEnd(item);
            item.FromMonth = ssm.fromMo;
            item.ToMonth = ssm.toMo;
            item.ToDateUseYearPart = ssm.ToDateUseYearPart;
            item.FixtureCount = ssm.fixtureCount;
            item.LeagueRound = ssm.totalRounds;
            item.CurrentRoundRound = item.CompleteRound;
            if (currentRound is not null)
            {
                item.CurrentRoundStart = currentRound.FirstOrDefault().Time;
                item.CurrentRoundEnd = currentRound.LastOrDefault().Time;
            }
            catalog.Update(item);
        }
    }
}
Catalog.CategorySsmMapping currentSeasonStage(int catId, int seasonStageId) =>
    catalog.CategorySsmMappings.Where(c => c.CategoryId == catId && c.SeasonStageId==seasonStageId)
        .OrderByDescending(o => o.SeasonStageId)
       .FirstOrDefault();

IOrderedEnumerable<MatchCentre.Fixture> computeCurrentRoundStartEnd(Catalog.CategorySsmMapping item)
{

    var currentSSM = currentSeasonStage(item.CategoryId, item.SeasonStageId);
    var fixtures = matchCentre.Fixtures
     .Where(f =>
         f.SeasonStageId == currentSSM.SeasonStageId &&
         f.Time <= DateTime.Now.AddDays(1) &&
         f.CategoryMappingFixtureIds.Any(fc =>
             fc.CategoryId == item.CategoryId))
     .OrderBy(f => f.Time);
    var currentRoundFixtures = fixtures.ToList()
                .GroupBy(g => g.Round)
                .OrderByDescending(o => o.Key);
    return currentRoundFixtures.FirstOrDefault().OrderBy(o => o.Time);

}
void updateTotalGoals()
{
    var catIds = new int[] { 24, 27, 30, 33, 36 };
    int seasonStageId = 1172;
    foreach (var catId in catIds)
    {
        var teamCategoryMappings = teamPlayer.TeamCategoryMappings.Where(t => t.CategoryId == catId
                                    && t.SeasonStageId == seasonStageId).ToList();

        var teamIds = teamCategoryMappings.Select(t => t.TeamId).ToList();
        foreach (var teamId in teamIds)
        {
            var fixtures = SelectFixtures(seasonStageId, catId, teamId).ToList();
            var totalGoals = 0;
            foreach (var f in fixtures)
            {
                if (String.IsNullOrEmpty(f.FullTime)) continue;
                var parts = f.FullTime.Split(':');
                if (parts.Length == 2)
                {
                    var homeScore = int.Parse(parts[0]);
                    var awayScore = int.Parse(parts[1]);

                    totalGoals += f.HomeId == teamId ? homeScore : awayScore;
                }
            }
            if (totalGoals > 0)
            {
                var tc = teamPlayer.TeamCategoryMappings.FirstOrDefault(t => t.CategoryId == catId
                                     && t.SeasonStageId == seasonStageId && t.TeamId == teamId);
                if (tc != null)
                {
                    tc.TeamGoals = totalGoals;
                    teamPlayer.Update(tc);
                }
            }
        }


    }
}
IQueryable<MatchCentre.Fixture> SelectFixtures(int seasonStageId, int catId, int teamId)
{

    var query = from f in matchCentre.Fixtures
                join fc in matchCentre.FixtureCategoryMappings
                on f.Id equals fc.FixtureId
                where fc.CategoryId == catId
                && f.SeasonStageId == seasonStageId
                && (f.HomeId == teamId || f.AwayId == teamId)
                select f;
    return query;
}

SqlServer.UrlRecord GetUrlRecord(int id, string entityName)
=> source.UrlRecords.FirstOrDefault(f => f.EntityId == id && f.EntityName == entityName);


record Category_Stage_Mapping(
   int categoryId,
   int fromMo,
   int toMo,
   int fixtureCount,
   int FromDateUseYearPart,
   int ToDateUseYearPart,
   int totalRounds
);