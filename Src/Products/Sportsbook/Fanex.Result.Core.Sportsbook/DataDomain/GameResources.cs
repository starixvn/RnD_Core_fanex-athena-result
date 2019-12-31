namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    using System.Collections.Generic;

    internal static class GameResources
    {
        private static IDictionary<string, List<string>> _betRadar = new Dictionary<string, List<string>>
        {
            { "Sequence", new List<string> { GameResourceID.H1Sequence, GameResourceID.FTSequence } },
            { "Goals", new List<string> { GameResourceID.FirstTenMinutes1X2, GameResourceID.FirstGoalMethod} },
            { "GoalRecord", new List<string> { GameResourceID.NextGoal1H, GameResourceID.NextGoal3rd, GameResourceID.ExtraTimeNextGoal } },
            { "GoalTime", new List<string> { GameResourceID.FirstGoalTime10Min, GameResourceID.FirstGoalTime15Min, GameResourceID.NextGoalTime } },
            { "GoalScorer", new List<string> { GameResourceID.FirstGoalScorer, GameResourceID.GoalScorer } },
            { "Corner", new List<string> { GameResourceID.FirstHalfCorners, GameResourceID.FinalCorners } },
            { "Corners", new List<string>
                {
                    GameResourceID.TimeOfFirstCorner,
                    GameResourceID.TimeOfSecondHalfFirstCorner,
                    GameResourceID.CornersRace,
                    GameResourceID.FirstHaftCornerRace,
                    GameResourceID.FTFirstCorner,
                    GameResourceID.FirstHaftFirstCorner,
                    GameResourceID.SecondHaftFirstCorner
                }
            },
            { "FirstLastCorners", new List<string> { GameResourceID.First1HCorner, GameResourceID.Last1HCorner, GameResourceID.FirstCorner, GameResourceID.LastCorner } },
            { "PlayerSentOff", new List<string> { GameResourceID.PlayserSentOff1H, GameResourceID.PlayerSentOff } },
            { "HomeAwayPlayerSentOff", new List<string>
                {
                    GameResourceID.AwayPlayerSentOff1H,
                    GameResourceID.HomePlayerSentOff1H,
                    GameResourceID.HomePlayerSentOff,
                    GameResourceID.AwayPlayerSentOff
                }
            },
            { "Special", new List<string>
                {
                    GameResourceID.ExtraTimeYesNo,
                    GameResourceID.ExtraTimeAndGoal,
                    GameResourceID.PenaltyShootout,
                    GameResourceID.MatchDecidedMethod,
                    GameResourceID.WhoAdvancesToNextRound
                }
            },
            { "ExtraTime", new List<string>{ GameResourceID.ExtraTimeNextGoalMethod} },            
            { "PenaltyShootoutRoundOne", new List<string>
                {
                    GameResourceID.HomePenaltyShootoutRound1,
                    GameResourceID.AwayPenaltyShootoutRound1,
                    GameResourceID.PenaltyShootout1X2Round1,
                    GameResourceID.PenaltyShootoutBothOneNeitherRound1,
                    GameResourceID.PenaltyShootoutWoodworkRound1
                }
            },
            { "PenaltyShootoutRoundTwo", new List<string>
                {
                    GameResourceID.HomePenaltyShootoutRound2,
                    GameResourceID.AwayPenaltyShootoutRound2,
                    GameResourceID.PenaltyShootout1X2Round2,
                    GameResourceID.PenaltyShootoutBothOneNeitherRound2,
                    GameResourceID.PenaltyShootoutWoodworkRound2
                }
            },
            { "PenaltyShootoutRoundThree", new List<string>
                {
                    GameResourceID.HomePenaltyShootoutRound3,
                    GameResourceID.AwayPenaltyShootoutRound3,
                    GameResourceID.PenaltyShootout1X2Round3,
                    GameResourceID.PenaltyShootoutBothOneNeitherRound3,
                    GameResourceID.PenaltyShootoutWoodworkRound3
                }
            },
            { "PenaltyShootoutRoundFour", new List<string>
                {
                    GameResourceID.HomePenaltyShootoutRound4,
                    GameResourceID.AwayPenaltyShootoutRound4,
                    GameResourceID.PenaltyShootout1X2Round4,
                    GameResourceID.PenaltyShootoutBothOneNeitherRound4,
                    GameResourceID.PenaltyShootoutWoodworkRound4
                }
            },
            { "PenaltyShootoutRoundFive", new List<string>
                {
                    GameResourceID.HomePenaltyShootoutRound5,
                    GameResourceID.AwayPenaltyShootoutRound5,
                    GameResourceID.PenaltyShootout1X2Round5,
                    GameResourceID.PenaltyShootoutBothOneNeitherRound5,
                    GameResourceID.PenaltyShootoutWoodworkRound5
                }
            },
            { "PenaltyShootoutFirstTen", new List<string>
                {
                    GameResourceID.PenaltyShootoutOddEvenFirst10,
                    GameResourceID.HomePenaltyShootoutOverUnderFirst10,
                    GameResourceID.AwayPenaltyShootoutOverUnderFirst10,
                    GameResourceID.PenaltyShootoutExactTotalGoalFirst10                    
                }
            },
            { "PenaltyShootoutSuddenDeath", new List<string>
                {
                    GameResourceID.PenaltyShootoutToGoToSuddenDeath,
                    GameResourceID.HomePenaltyShootoutRoundMore5,
                    GameResourceID.AwayPenaltyShootoutRoundMore5
                }
            }
        };

        private static IDictionary<string, List<string>> _moreResultGroup = new Dictionary<string, List<string>>
        {
            { "Goals", new List<string>() },
            { "GoalRecord", new List<string>()},
            { "GoalTime", new List<string> { GameResourceID.TimeOfFirstGoal } },
            { "GoalScorer", new List<string>() },
            { "Corner", new List<string>() },
            { "FirstLastCorners", new List<string>() },
            { "PlayerSentOff", new List<string>()},
            { "HomeAwayPlayerSentOff", new List<string>()},
            { "Special", new List<string>()}
        };

        private static IList<string> _fastMarkets = new List<string>
        {
            GameResourceID.NextOneMinutes,
            GameResourceID.NextFiveMinutes,
            GameResourceID.FirstOneMinutes,
            GameResourceID.FirstFiveMinutes,
            GameResourceID.NextOneMinuteSetPiece,
            GameResourceID.WhichCombinationFirstOneMinutes,
            GameResourceID.WhichCombinationFirstFiveMinutes
        };

        public static IDictionary<string, List<string>> BetRadar => _betRadar;

        public static IDictionary<string, List<string>> MoreResultGroup => _moreResultGroup;

        public static IList<string> FastMarkets => _fastMarkets;

        public static int[] PossibleMuayThaiScore { get; } = new[] { 0, 1, 5 };
    }
}