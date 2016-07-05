// -----------------------------------------------------------------------
// <copyright file="GameResources.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class GameResources
    {
        private static IDictionary<string, List<string>> _betRada = new Dictionary<string, List<string>>
        {
        { "Goals", new List<string> { GameResourceID.FirstTenMinutes1X2, GameResourceID.FirstGoalMethod } },
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

        private static int[] _possibleMuayThaiScore = { 0, 1, 5 };

        public static IDictionary<string, List<string>> BetRada
        {
            get
            {
                return _betRada;
            }
        }

        public static int[] PossibleMuayThaiScore
        {
            get
            {
                return _possibleMuayThaiScore;
            }
        }
    }
}