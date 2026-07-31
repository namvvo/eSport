using System.ComponentModel.DataAnnotations;

namespace eSport.MatchCentre.API.Models;

public partial class FixtureStat : Entity<int>
{
    [Required] public int FixtureId { get; set; } // int
    [Required] public int PlayerId { get; set; } // int
    public string? Position { get; set; } // varchar(10)
    public string? PlayablePositions { get; set; } // nchar(30)
    public string? XY { get; set; } // varchar(5)
    public int? WhoscoredFormationPlace { get; set; } // int
    public int? ShirtNumber { get; set; } // int
    [Required] public int TeamOwnerId { get; set; } // int
    [Required] public int SubInMinute { get; set; } // int
    [Required] public int SubOutMinute { get; set; } // int
    [Required] public int MinPlayed { get; set; } // int
    [Required] public int Shots { get; set; } // int
    [Required] public int ShotsOnTarget { get; set; } // int
    [Required] public int ShotsOffTarget { get; set; } // int
    [Required] public int ShotsBlocked { get; set; } // int
    [Required] public int BigChanceCreated { get; set; } // int
    [Required] public int BigChanceMissed { get; set; } // int
    [Required] public int Dribbles { get; set; } // int
    [Required] public int DribblesWon { get; set; } // int
    [Required] public int DribblesPast { get; set; } // int
    [Required] public int DuelWon { get; set; } // int
    [Required] public int DuelLost { get; set; } // int
    [Required] public int Fouled { get; set; } // int
    [Required] public int Fouls { get; set; } // int
    [Required] public int Offsided { get; set; } // int
    [Required] public int PenaltyConceded { get; set; } // int
    [Required] public int Dispossessed { get; set; } // int
    [Required] public int UnsTouches { get; set; } // int
    [Required] public int KeyPasses { get; set; } // int
    [Required] public int AccPasses { get; set; } // int
    [Required] public int Passes { get; set; } // int
    [Required] public int Crosses { get; set; } // int
    [Required] public double AccCrosses { get; set; } // float
    [Required] public int LongBall { get; set; } // int
    [Required] public double AccLB { get; set; } // float
    [Required] public int ThroughBall { get; set; } // int
    [Required] public double AccThB { get; set; } // float
    [Required] public int TotalTackles { get; set; } // int
    [Required] public int LastManTackle { get; set; } // int
    [Required] public int Interceptions { get; set; } // int
    [Required] public int PossessionLost { get; set; } // int
    [Required] public int Clearances { get; set; } // int
    [Required] public int BlockedShots { get; set; } // int
    [Required] public double Rating { get; set; } // float
    [Required] public bool Motm { get; set; } // bit
    public string? KeyEvents { get; set; } // nvarchar(max)
    [Required] public int AerialWon { get; set; } // int
    [Required] public int AerialLost { get; set; } // int
    [Required] public int GroundDuelWon { get; set; } // int
    [Required] public int GroundDuelLost { get; set; } // int
    [Required] public int YellowCard { get; set; } // int
    [Required] public int YellowRed { get; set; } // int
    [Required] public int RedCard { get; set; } // int
    [Required] public int Assist { get; set; } // int
    [Required] public int NoMatches { get; set; } // int
    [Required] public int Goal { get; set; } // int
    [Required] public int OwnGoal { get; set; } // int
    [Required] public int Touches { get; set; } // int
    [Required] public int PenGoal { get; set; } // int
    [Required] public int PenWon { get; set; } // int
    [Required] public int Error2Goal { get; set; } // int
    [Required] public int ClearanceOffline { get; set; } // int
    [Required] public int ShotOnPost { get; set; } // int
    [Required] public int PKMissed { get; set; } // int
    [Required] public int PKShootoutScored { get; set; } // int
    [Required] public int PKShootoutMissed { get; set; } // int
    [Required] public int PKShootoutSaved { get; set; } // int
    [Required] public int ThrowIns { get; set; } // int
    [Required] public int GKSaves { get; set; } // int
    [Required] public int GKCatch { get; set; } // int
    [Required] public int GKPunch { get; set; } // int
    [Required] public int GKClearance { get; set; } // int
    [Required] public int GKTotalSweeper { get; set; } // int
    [Required] public int GKErrorLeadToAShot { get; set; } // int
    [Required] public int GKSweeper { get; set; } // int
    [Required] public int GKPenSaves { get; set; } // int
    [Required] public bool IsCaptain { get; set; } // bit


    public Fixture Fixture { get; set; } = null!;
}

