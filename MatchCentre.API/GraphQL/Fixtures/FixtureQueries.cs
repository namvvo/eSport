using eSport.MatchCentre.API.Dto;

namespace eSport.MatchCentre.API.GraphQL.Fixtures;

[QueryType]
public static partial class FixtureQueries
{
    [UseProjection]
    public static IQueryable<Fixture> GetFixture(int id,
        FixtureContext db)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException();
        try
        {
            var f =  db.Fixtures.Where(x => x.Id == id);
            if (f == null) throw new System.Collections.Generic.KeyNotFoundException();
            return f;
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }
   
}
