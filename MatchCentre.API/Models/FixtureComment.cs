
namespace eSport.MatchCentre.API.Models;

public partial class FixtureComment : Entity<int>
{

    [Required] public int FixtureId { get; set; } // int
    [Required] public required string Min { get; set; } // varchar(10)
    [Required] public int Team { get; set; } // int
    [Required] public required string Text { get; set; } // nvarchar(max)
    public string? Action { get; set; } // varchar(max)

    #region Associations


    public Fixture Fixture { get; set; } = null!;

    #endregion
}
