using eSport.Catalog.API.Grpc;
using eSport.TeamPlayer.API.Dto.Teams;
namespace eSport.TeamPlayer.API.Services;

public partial class TeamService : ITeamService
{
    private readonly TeamPlayerContext _db;
    private readonly CategoryGrpc.CategoryGrpcClient _categoryGrpcClient;
    public TeamService(TeamPlayerContext db,
                       CategoryGrpc.CategoryGrpcClient categoryGrpcClient)
    {
        _db = db;
        _categoryGrpcClient = categoryGrpcClient;
    }
    public async Task<IList<Team>> GetTeamsAsync(int categoryId, IList<int> seasonStageIds)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId);
        var teamCategories = _db.TeamCategories
                 .AsNoTracking()
                 .Where(tc => tc.CategoryId == categoryId);

        if (seasonStageIds.Any() && seasonStageIds[0] > 0)
            teamCategories = teamCategories.Where(q => seasonStageIds.Contains(q.SeasonStageId));

        return await teamCategories.Select(x => x.Team).Distinct().ToListAsync();
    }
    public async Task<TeamStandingModel> GetTeamStandingsByCategoryAsync(int categoryId, int seasonStageId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(seasonStageId);

        var response = _categoryGrpcClient.GetCategoryById(new GetCategoryByIdRequest()
        {
            CategoryId = categoryId
        });
        var category = response.Category;

        ArgumentNullException.ThrowIfNull(category, nameof(category));
     
        var query = from tc in _db.TeamCategories.AsNoTracking()
                    join t in _db.Teams.AsNoTracking() on tc.TeamId equals t.Id
                    where tc.CategoryId == categoryId && tc.SeasonStageId == seasonStageId
                    select new TeamStatDto()
                    {
                        P = tc.P ?? 0,
                        W = tc.W,
                        D = tc.D ?? 0,
                        L = tc.L ?? 0,
                        GA = tc.GA ?? 0,
                        GF = tc.GF ?? 0,
                        GD = tc.GD ?? 0,
                        Pts = tc.Pts ?? 0,
                        Rank = tc.Rank ?? 0,
                        LastRank = tc.LastRank ?? 0,
                        Forms = tc.Forms,
                        TeamId = tc.TeamId,
                        Team = t.Name,
                        TeamSeName = t.SeName,
                        TeamShortName = t.ShortName
                    };
        var teamStandings = new TeamStandingModel()
        {
            RankRule = PrepareRuleRank(category),
            IsTournament = category.IsTournament,
            IsComplete = true,
            TeamStandings = query.OrderBy(x => x.Rank).ToList()
        };
        return teamStandings;
    }
    internal RankRuleModel PrepareRuleRank(CategoryMapping category)
    {


        if (category.IsTournament)
        {
            return new RankRuleModel()
            {
                Group1 = 2,
                Group2 = 3,
                Group3 = 4,
                Group4 = 100,
                Group5 = 100,

            };
        }
        else
            return new RankRuleModel()
            {
                Group1 = category.UefaC1,
                Group2 = category.UefaC1Qualifiers,
                Group3 = category.EuropaLeague,
                Group4 = category.EuropaLeagueQualifiers,
                Group5 = category.RelegationPlayOff,
                Group6 = category.Relegation,

            };
    }
}
