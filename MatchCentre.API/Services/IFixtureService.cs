
using eSport.MatchCentre.API.Dto;

namespace eSport.MatchCentre.API.Services;

public interface IFixtureService
{
    Task<FixtureDto> GetFixtureAsync(int id);
    Task<List<FixtureSearchResultDto>> SearchFixturesAsync(SearchFixturesRequest request, CancellationToken cancellationToken = default);


}
