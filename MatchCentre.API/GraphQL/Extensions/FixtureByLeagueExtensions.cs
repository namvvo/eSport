using eSport.MatchCentre.API.GraphQL.Queries.Fixtures;

namespace eSport.MatchCentre.API.GraphQL.Extensions;
// add extra fields to object type
[ExtendObjectType<Fixture>]
public static class FixtureByLeagueExtensions
{
    public static TeamReference GetHomeTeam(
        [Parent] Fixture parent)
        => new()
        {
            Id = parent.HomeId
        };

    public static TeamReference GetAwayTeam(
        [Parent] Fixture parent)
        => new()
        {
            Id = parent.AwayId
        };

   
    
    //public static string GetScoreStatus([Parent] Fixture f, bool home)
    //{

    //    string score =!String.IsNullOrWhiteSpace(f.FullTime) ? f.FullTime: String.Empty;
        
    //    if (!String.IsNullOrWhiteSpace(f.PK)) score = f.PK;
    //    if (!String.IsNullOrWhiteSpace(f.ExtraTime)) score = f.ExtraTime;

    //    if (String.IsNullOrWhiteSpace(score) || score.Contains("vs")) return String.Empty;
    //    var numbers = score.Split(':');
    //    int number1 = Int16.Parse(numbers[0].Trim());
    //    int number2 = Int16.Parse(numbers[1].Trim());
    //    string value = "";
    //    if (home)
    //    {
    //        if (number1 > number2) value = "Tg";
    //        if (number1 == number2) value = "H";
    //        if (number1 < number2) value = "Th";
    //    }
    //    else
    //    {
    //        if (number1 > number2) value = "Th";
    //        if (number1 == number2) value = "H";
    //        if (number1 < number2) value = "Tg";
    //    }
    //    return value;

    //}

}