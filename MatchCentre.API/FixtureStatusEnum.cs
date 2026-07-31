namespace eSport.MatchCentre.API;

public enum FixtureStatusEnum : byte
{
    NotStarted = 0,
    Postponed = 60,
    Canceled = 70,

    Finished = 100,
    AET = 110
}