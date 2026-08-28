namespace eSport.UI.Client;

//public static class FixtureHelper
//{
//    public static bool CheckIsLiveMatch(this Fixture f)
//    {
//        //if (!String.IsNullOrWhiteSpace(f.LiveScore))
//        //{
//        var tp = DateTime.Now - f.Time;
//        return !f.IsComplete
//            && tp.TotalMinutes <= 150
//            && f.Status != (int)FixtureStatusEnum.Postponed
//            && f.Status != (int)FixtureStatusEnum.Canceled;
//        //}
//        //return false;
//    }
//    /// <summary>         
//    /// </summary>
//    /// <param name="f"></param>
//    /// <param name="home"></param>
//    /// <returns></returns>
//    public static string GetScoreStatus(this Fixture f, bool home = true)
//    {
//        string score = f.FullTime;
//        if (!String.IsNullOrWhiteSpace(f.PK)) score = f.PK;
//        if (!String.IsNullOrWhiteSpace(f.ExtraTime)) score = f.ExtraTime;

//        if (String.IsNullOrWhiteSpace(score) || score.Contains("vs")) return String.Empty;
//        var numbers = score.Split(':');
//        int number1 = Int16.Parse(numbers[0].Trim());
//        int number2 = Int16.Parse(numbers[1].Trim());
//        string value = "";
//        if (home)
//        {
//            if (number1 > number2) value = "Tg";
//            if (number1 == number2) value = "H";
//            if (number1 < number2) value = "Th";
//        }
//        else
//        {
//            if (number1 > number2) value = "Th";
//            if (number1 == number2) value = "H";
//            if (number1 < number2) value = "Tg";
//        }
//        return value;
//    }
//    public static (int, int) GetScoreAdvantage(this Fixture f)
//    {
//        string score = f.LiveScore;
//        //if (!String.IsNullOrWhiteSpace(f.PK)) score = f.PK;
//        //if (!String.IsNullOrWhiteSpace(f.ExtraTime)) score = f.ExtraTime;

//        if (!f.IsComplete) return (0, 0);

//        var numbers = score.Split(':');
//        int number1 = Int16.Parse(numbers[0].Trim());
//        int number2 = Int16.Parse(numbers[1].Trim());

//        //if (number1 > number2) return f.HomeId;
//        //else
//        //{
//        //    if (number2 > number1) return f.AwayId;
//        //    if (number1 == number2) return 0;
//        //}

//        return (number1, number2);
//    }
//    public static int ComputeTeamPts(this TeamStatModel stat) => stat.W * 3 + stat.D;
//    public static string GetScoreStatus(this FixtureEntityModel f, bool home = true)
//    {
//        string value = "";

//        var fixtureScoreParts = f.FullTime.ParseFixtureScore();
//        int number1 = fixtureScoreParts.Item1;
//        int number2 = fixtureScoreParts.Item2;

//        if (home)
//        {
//            if (number1 > number2) value = "Tg";
//            if (number1 == number2) value = "H";
//            if (number1 < number2) value = "Th";
//        }
//        else
//        {
//            if (number1 > number2) value = "Th";
//            if (number1 == number2) value = "H";
//            if (number1 < number2) value = "Tg";
//        }
//        return value;
//    }

//}