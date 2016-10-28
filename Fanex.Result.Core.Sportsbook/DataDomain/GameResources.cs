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

        private static IList<string> _fastMarkets = new List<string>
        {
            GameResourceID.NextOneMinutes,
            GameResourceID.NextFiveMinutes,
            GameResourceID.FirstOneMinutes,
            GameResourceID.FirstFiveMinutes,
            GameResourceID.NextOneMinuteSetPiece
        };

        public static IDictionary<string, List<string>> BetRadar
        {
            get { return _betRadar; }
        }

        public static IList<string> FastMarkets
        {
            get { return _fastMarkets; }
        }

        public static int[] PossibleMuayThaiScore { get; } = new int[] { 0, 1, 5 };
    }
}