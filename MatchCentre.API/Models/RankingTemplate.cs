using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace eSport.MatchCentre.API.Models;

public partial class RankingTemplate
{
    [Required] public int SeasonStageId { get; set; } // int
    [Required] public int CategoryId { get; set; } // int
    /// <summary>
    /// 0
    /// </summary>
    [Column, NotNull] public int Relegation { get; set; } // int
    [Column, NotNull] public int RelegationPlayOff { get; set; } // int
    [Column, NotNull] public int UefaC1 { get; set; } // int
    [Column, NotNull] public int UefaC1Qualifiers { get; set; } // int
    [Column, NotNull] public int EuropaLeagueQualifiers { get; set; } // int
    /// <summary>
    /// 5
    /// </summary>
    [Column, NotNull] public int EuropaLeague { get; set; } // int
    [Column, NotNull] public int EuropaLeagueSpots { get; set; } // int
}
