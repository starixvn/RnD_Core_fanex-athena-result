namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    using System.Collections.Generic;

    public static class CricketResourceID
    {
        public const string FinalScore = "00";

        public const string HomeFirstScore = "H1";

        public const string HomeSecondScore = "H2";

        public const string AwayFirstScore = "A1";

        public const string AwaySecondScore = "A2";

        public const string FirstHalfScore = "^H\\d{1}$";

        public const string SecondHalfScore = "^A\\d{1}$";

        public const string SuperOverWinner = "9400";

        public const string TossWinner = "9401";

        public const string HighestOpeningPartnership = "9442";

        public const string HighestScoreInOver = "9443";

        public const string MatchFours = "9446";

        public const string MatchSixes = "9447";

        public const string MatchExtras = "9450";

        public const string MatchWickets = "9449";

        public const string HighestIndividualScore = "9452";

        public const string HighestOverScore = "9453";

        public const string HomeGroupXRuns = "^9410\\d{4}$";
        public const string AwayGroupXRuns = "^9411\\d{4}$";
        public const string HomeScoreAfterOverXBallY = "^9422\\d{6}$";
        public const string AwayScoreAfterOverXBallY = "^9423\\d{6}$";
        public const string HomeFallOfWicketX = "^9428\\d{4}$";
        public const string AwayFallOfWicketX = "^9429\\d{4}$";
        public const string HomeWicketXMethodOfDismissal = "^9434\\d{4}$";
        public const string AwayWicketXMethodOfDismissal = "^9435\\d{4}$";
        public const string HomeInnsFours = "^9454\\d{1}$";
        public const string AwayInnsFours = "^9455\\d{1}$";
        public const string HomeInnsSixes = "^9460\\d{1}$";
        public const string AwayInnsSixes = "^9461\\d{1}$";
        public const string HomeOverXExactRuns = "^9484\\d{4}$";
        public const string AwayOverXExactRuns = "^9485\\d{4}$";
        public const string HomeOverXBallYExactRuns = "^9490\\d{6}$";
        public const string AwayOverXBallYExactRuns = "^9491\\d{6}$";

        public static IDictionary<string, List<string>> MoreResultGroup => _moreResultGroup;

        private readonly static IDictionary<string, List<string>> _moreResultGroup = new Dictionary<string, List<string>>
        {
            { "SuperOverWinner", new List<string>{ SuperOverWinner, TossWinner } },
            { "MatchSixes", new List<string>
                {
                    MatchFours, MatchSixes, MatchExtras, HighestIndividualScore, HighestOverScore,
                    HomeGroupXRuns, AwayGroupXRuns, HomeScoreAfterOverXBallY, AwayScoreAfterOverXBallY,
                    HomeFallOfWicketX, AwayFallOfWicketX, HomeWicketXMethodOfDismissal, AwayWicketXMethodOfDismissal,
                    HomeInnsFours, AwayInnsFours, HomeInnsSixes, AwayInnsSixes, HomeOverXExactRuns,
                    AwayOverXExactRuns, HomeOverXBallYExactRuns, AwayOverXBallYExactRuns,
                    HighestOpeningPartnership, HighestScoreInOver
                }
            },
            { "MatchWickets", new List<string> { MatchWickets } }
        };
    }
}