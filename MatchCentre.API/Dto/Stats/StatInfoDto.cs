namespace eSport.MatchCentre.API.Dto.Stats;

public record StatInfo
{
    public int PlayerId { get; set;  }
    public string Name { get; set; } = string.Empty;
    public string Info { get; set; } = string.Empty;
    public string InfoWithDecimal { get; set; } = string.Empty;
    public string Info2 { get; set; } = string.Empty;
    public string SeName { get; set; } = string.Empty;
}
