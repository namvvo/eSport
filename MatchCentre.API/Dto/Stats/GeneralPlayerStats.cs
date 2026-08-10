namespace eSport.MatchCentre.API.Dto.Stats;

public sealed record GeneralPlayerStats
{
    public int Yellow { get; init; }
    public int Red { get; init; }
    public int Subs { get; init; }
    public int Motm { get; init; }
}
