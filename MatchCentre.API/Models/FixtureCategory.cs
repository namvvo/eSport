namespace eSport.MatchCentre.API.Models;
public class FixtureCategory : Entity<int>
{
    [Required] public int CategoryId { get; set; } // int
    [Required] public int FixtureId { get; set; } // int

    public Fixture Fixture { get; set; } = null!;
}
