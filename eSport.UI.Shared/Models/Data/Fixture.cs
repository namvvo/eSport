namespace eSport.UI.Shared.Models.Data;

public partial class FixturePagingModel
{
    //public PaginationModel PaginationModel { get; set; } = new();

    public RoundOfFixture CurRound { get; set; } = new();
    public DateTime StartDate { get; set; }
    public string RoundText { get; set; } = string.Empty;
   
}

