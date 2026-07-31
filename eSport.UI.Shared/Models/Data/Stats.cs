namespace eSport.UI.Shared.Models.Data
{

    public record TopTeamStatModel
    {
        
        public List<StatInfo> Possession { get; set; } = new();
        public List<StatInfo> Aggression { get; set; } = new();
        public List<StatInfo> AerialDuels { get; set; } = new();
        public List<StatInfo> ShotsPerGame { get; set; } = new();
        public List<StatInfo> PassAccuracy { get; set; } = new();
        public List<StatInfo> Ratings { get; set; } = new();
    }

    public record StatInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string InfoWithDecimal { get; set; } = string.Empty;
        public string Info2 { get; set; } = string.Empty;
        public string SeName { get; set; } = string.Empty;
    }

    public record TopPlayerStatsModel
    {
        public List<StatInfo> TopPerformance { get; set; } = new();
        public List<StatInfo> Assist { get; set; } = new();
        public List<StatInfo> Goal { get; set; } = new();
        public List<StatInfo> Dribble { get; set; } = new();
        public List<StatInfo> Aggression { get; set; } = new();
        public List<StatInfo> ShotsPerGame { get; set; } = new();

        public List<StatInfo> PassAccuracy { get; set; } = new();
        public List<StatInfo> Ratings { get; set; } = new();
        public List<StatInfo> AerialsWon { get; set; } = new();

}

}
