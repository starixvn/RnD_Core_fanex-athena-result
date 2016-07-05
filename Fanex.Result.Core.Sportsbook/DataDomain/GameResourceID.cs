// -----------------------------------------------------------------------
// <copyright file="GameResourceID.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GameResourceID
    {
        /// <summary>
        /// Resource ID = 00.
        /// </summary>
        public const string FinalScore = "00";

        /// <summary>
        /// Resource ID = 01.
        /// </summary>
        public const string FirstHalfScore = "01";

        /// <summary>
        /// Resource ID = 04.
        /// </summary>
        public const string FinalCorners = "04";

        /// <summary>
        /// Resource ID = 05.
        /// </summary>
        public const string FirstHalfCorners = "05";

        /// <summary>
        /// Resource ID = 08.
        /// </summary>
        public const string GoalSequence = "08";

        /// <summary>
        /// Resource ID = 135.
        /// </summary>
        public const string PenaltyShootout = "135";

        /// <summary>
        /// Resource ID = 160xx.
        /// </summary>
        public const string NextGoal3rd = "^160\\d{2}$";

        /// <summary>
        /// Resource ID = 164xx.
        /// </summary>
        public const string ExtraTimeNextGoal = "^164\\d{2}$";

        /// <summary>
        /// Resource ID = 168.
        /// </summary>
        public const string WhoAdvancesToNextRound = "168";

        /// <summary>
        /// Resource ID = 169xx.
        /// </summary>
        public const string NextGoalTime = "^169\\d{2}$";

        /// <summary>
        /// Resource ID = 174.
        /// </summary>
        public const string ExtraTimeAndGoal = "174";

        /// <summary>
        /// Resource ID = 175.
        /// </summary>
        public const string MatchDecidedMethod = "175";

        /// <summary>
        /// Resource ID = 176.
        /// </summary>
        public const string FirstTenMinutes1X2 = "176";

        /// <summary>
        /// Resource ID = 180xx.
        /// </summary>
        public const string NextGoal1H = "^180\\d{2}$";

        /// <summary>
        /// Resource ID = 192.
        /// </summary>
        public const string FirstGoalTime10Min = "192";

        /// <summary>
        /// Resource ID = 193.
        /// </summary>
        public const string FirstGoalTime15Min = "193";

        /// <summary>
        /// Resource ID = 206.
        /// </summary>
        public const string FirstCorner = "206";

        /// <summary>
        /// Resource ID = 207.
        /// </summary>
        public const string First1HCorner = "207";

        /// <summary>
        /// Resource ID = 208.
        /// </summary>
        public const string LastCorner = "208";

        /// <summary>
        /// Resource ID = 209.
        /// </summary>
        public const string Last1HCorner = "209";

        /// <summary>
        /// Resource ID = 210.
        /// </summary>
        public const string PlayerSentOff = "210";

        /// <summary>
        /// Resource ID = 211.
        /// </summary>
        public const string PlayserSentOff1H = "211";

        /// <summary>
        /// Resource ID = 212.
        /// </summary>
        public const string HomePlayserSentOff = "212";

        /// <summary>
        /// Resource ID = 213.
        /// </summary>
        public const string HomePlayserSentOff1H = "213";

        /// <summary>
        /// Resource ID = 214.
        /// </summary>
        public const string AwayPlayserSentOff = "214";

        /// <summary>
        /// Resource ID = 215.
        /// </summary>
        public const string AwayPlayserSentOff1H = "215";

        /// <summary>
        /// Resource ID = 216xx.
        /// </summary>
        public const string GoalScorer = "^216\\d{2}$";

        /// <summary>
        /// Resource ID = 217.
        /// </summary>
        public const string FirstGoalScorer = "217";

        /// <summary>
        /// Resource ID = 173.
        /// </summary>
        public const string ExtraTimeYesNo = "173";

        public const string FirstGoalMethod = "424";
    }
}