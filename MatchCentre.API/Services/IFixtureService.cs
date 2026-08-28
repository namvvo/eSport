
using eSport.MatchCentre.API.Dto;

namespace eSport.MatchCentre.API.Services;

public interface IFixtureService
{
    Task<FixtureDto> GetFixtureAsync(int id);
    Task<List<FixtureSearchResultDto>> SearchFixturesAsync(SearchFixturesRequest request, CancellationToken cancellationToken = default);
    Task<IQueryable<Fixture>> GetFixturesByLeague(int seasonStageId,
                                            int categoryId,
                                            DateTime startDate,
                                            DateTime endDate,
                                            bool withTeam = false);

}
