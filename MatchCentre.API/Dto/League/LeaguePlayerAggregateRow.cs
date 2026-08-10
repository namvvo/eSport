namespace eSport.MatchCentre.API.Dto.League;

public sealed record LeaguePlayerAggregateRow
{
    public int PlayerId { get; set; }
    public int TeamOwnerId { get; set; }
    public int CategoryId { get; set; }
    public double Rating { get; set; }
    public int Apps { get; set; }
    public int Goals { get; set; }
    public int PenGoals { get; set; }
    public int TeamGoals { get; set; }
    public int Assists { get; set; }
    public int Yellow { get; set; }
    public int Red { get; set; }
    public double ShotsPerGame { get; set; }
    public double PSPercentage { get; set; }
    public double AccCrosses { get; set; }
    public double Passes { get; set; }
    public double Dribbles { get; set; }
    public double AerialWon { get; set; }
    public double Tackles { get; set; }
    public double Interceptions { get; set; }
    public double Clearances { get; set; }
    public double Blocks { get; set; }
    public double KeyPasses { get; set; }
    public double Crosses { get; set; }
    public double LongBalls { get; set; }
    public double ThroughBalls { get; set; }
    public int OwnGoals { get; set; }
    public double Fouls { get; set; }
    public double ShotsOT {  get; set; }
    public double Touches { get; set; }
    public double Fouled { get; set; }
    public double Offsides { get; set; }
    public int Subs { get; set; }
    public int Motm { get; set; }
    public double Saves { get; set; }
    public string? CountryCSS { get; set; }
    public string? CategoryName { get; set; }
    public int? LeaguePictureId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string? TeamPosition { get; set; }
    public double Dispossessed { get; set; }
    public double Unstouch { get; set; }
    public int MinPlayed { get; set; }
    public int OwnGoal { get; set; }
    public double AvgP { get; set; }
    public double AerielsWon { get; set; }
    public double MarketValue { get; set; }
    public double CustomScore { get; set; }
}
