using System.Collections.Generic;

namespace Fanex.Result.Core.Sportsbook.Constants
{
    public static class BetTypeConstants
    {
        public const int OverUnder = 3;
        public const int OverUnder1H = 8;
        public const int OverUnder2H = 18;
        public const int HomeTeamOverUnder = 401;
        public const int AwayTeamOverUnder = 402;
        public const int HomeTeamOverUnder1H = 403;
        public const int AwayTeamOverUnder1H = 404;
        public const int BothHalvesOver = 434;
        public const int BothHalvesUnder = 435;
        public const int HomeTeamOverUnderDec = 461;
        public const int AwayTeamOverUnderDec = 462;
        public const int HomeTeamOverUnder1HDec = 463;
        public const int AwayTeamOverUnder1HDec = 464;
        public const int AlternativeCorners = 475;
        public const int AlternativeCorners1H = 476;
        public const int HomeTeamOverUnderCorners = 482;
        public const int AwayTeamOverUnderCorners = 483;
        public const int HomeTeamOverUnder1HCorners = 484;
        public const int AwayTeamOverUnder1HCorners = 485;

        public static readonly List<int> InstantSettlementBetTypesSupportFT = new List<int>
        {
            OverUnder,
            OverUnder2H,
            HomeTeamOverUnder,
            AwayTeamOverUnder,
            BothHalvesOver,
            BothHalvesUnder,
            HomeTeamOverUnderDec,
            AwayTeamOverUnderDec,
            AlternativeCorners,
            HomeTeamOverUnderCorners,
            AwayTeamOverUnderCorners
        };

        public static readonly List<int> InstantSettlementBetTypesSupportHT = new List<int>
        {
            OverUnder1H,
            HomeTeamOverUnder1H,
            AwayTeamOverUnder1H,
            HomeTeamOverUnder1HDec,
            AwayTeamOverUnder1HDec,
            AlternativeCorners1H,
            HomeTeamOverUnder1HCorners,
            AwayTeamOverUnder1HCorners
        };
    }
}
