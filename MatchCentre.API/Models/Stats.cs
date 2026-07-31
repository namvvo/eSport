using System.ComponentModel.DataAnnotations.Schema;
namespace eSport.MatchCentre.API.Models;

[ComplexType]
public class Stats
{
    public string? Coach { get; set; } // nvarchar(100)
   
    public string? ShotsGraph { get; set; } // varchar(max)
    public string? Formation { get; set; } // varchar(20)

    public string? MissingPlayers { get; set; } // nvarchar(max)
    [Required] public int Corner { get; set; } // int
    [Required] public int ThrowIns { get; set; } // int
    [Required] public double Possession { get; set; } // float
    [Required] public int Shots { get; set; } // int
    [Required] public double PassAccuracy { get; set; } // float
    [Required] public int ShotsOnTarget { get; set; } // int
    [Required] public double AerielWon { get; set; } // float
    [Required] public int AggressionY { get; set; } // int
    [Required] public int AggressionR { get; set; } // int
    [Required] public double Rating { get; set; } // float

}