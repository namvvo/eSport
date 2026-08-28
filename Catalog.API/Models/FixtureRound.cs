using System.ComponentModel.DataAnnotations.Schema;

namespace eSport.Catalog.API.Models;

[ComplexType]
public class FixtureRound
{
    public int Round { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}
