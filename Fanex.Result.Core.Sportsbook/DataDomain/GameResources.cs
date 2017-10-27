namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    using System.Collections.Generic;

    internal static class GameResources
    {
        private static IDictionary<string, List<string>> _betRadar = new Dictionary<string, List<string>>
        {
            { "Goals", new List<string> { GameResourceID.FirstTenMinutes1X2, GameResourceID.FirstGoalMethod, GameResourceID.GoalSequence } },
            { "GoalRecord", new List<string> { GameResourceID.NextGoal1H, GameResourceID.NextGoal3rd, GameResourceID.ExtraTimeNextGoal } },
            { "GoalTime", new List<string> { GameResourceID.FirstGoalTime10Min, GameResourceID.FirstGoalTime15Min, GameResourceID.NextGoalTime } },
            { "GoalScorer", new List<string> { GameResourceID.FirstGoalScorer, GameResourceID.GoalScorer } },
            { "Corner", new List<string> { GameResourceID.FirstHalfCorners, GameResourceID.FinalCorners } },
            { "FirstLastCorners", new List<string> { GameResourceID.First1HCorner, GameResourceID.Last1HCorner, GameResourceID.FirstCorner, GameResourceID.LastCorner } },
            { "PlayerSentOff", new List<string> { GameResourceID.PlayserSentOff1H, GameResourceID.PlayerSentOff } },
            { "HomeAwayPlayerSentOff", new List<string>
                {
                    GameResourceID.AwayPlayserSentOff1H,
                    GameResourceID.HomePlayserSentOff1H,
                    GameResourceID.HomePlayserSentOff,
                    GameResourceID.AwayPlayserSentOff
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
            GameResourceID.NextOneMinuteSetPiece
        };

        public static IDictionary<string, List<string>> BetRadar => _betRadar;

        public static IDictionary<string, List<string>> MoreResultGroup => _moreResultGroup;

        public static IList<string> FastMarkets => _fastMarkets;

        public static int[] PossibleMuayThaiScore { get; } = new[] { 0, 1, 5 };
    }
}