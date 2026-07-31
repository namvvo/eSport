namespace eSport.MatchCentre.API.Dto;

public class SearchFixturesRequest
{
    public int? Year { get; set; }
    public List<int> SeasonStageIds { get; set; } = new(); // Thay vì chuỗi string "1,2,3"
    public int HasScore { get; set; } = 2; // 1: Complete, 0: vs / null, 2: All
    public bool IsComplete { get; set; } = false;
    public int TeamId { get; set; } = 0;
    public int TeamId2 { get; set; } = 0;
    public int? OmittedId { get; set; }
    public List<int> CategoryIds { get; set; } = new();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? StartRound { get; set; }
    public int? ToRound { get; set; }
    public bool IsFriendly { get; set; } = false;
    public int PreviousHead2Head { get; set; } = 0; // 0: Normal Query, >0: Head-to-head Query
}
