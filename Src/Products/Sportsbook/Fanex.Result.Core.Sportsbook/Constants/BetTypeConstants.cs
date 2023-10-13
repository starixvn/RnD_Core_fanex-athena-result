using Fanex.Result.Core.Sportsbook.DataDomain;
using System.Collections.Generic;
using System.Linq;

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

        public const int QuarterXOverUnder = 610;
        public const int QuarterXHomeTeamOverUnder = 615;
        public const int QuarterXAwayTeamOverUnder = 616;
        public const int MatchHandicapTotal = 646;

        public static bool IsInstantSettlementBetTypesSupportFT(int bettype)
            => InstantSettlementBetTypesSupportFT.Any(i => i == bettype);

        private static readonly List<int> InstantSettlementBetTypesSupportFT = new List<int>
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

        public static bool IsInstantSettlementBetTypesSupportHT(int bettype)
            => InstantSettlementBetTypesSupportHT.Any(i => i == bettype);

        private static readonly List<int> InstantSettlementBetTypesSupportHT = new List<int>
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

        public static readonly Dictionary<int, string> BetTypeBasketballShowLiveScore = new Dictionary<int, string>
        {
            { OverUnder, BasketBallResourceID.FullTime },
            { OverUnder1H, BasketBallResourceID.FirstHalf },
            { HomeTeamOverUnder, BasketBallResourceID.FullTime },
            { AwayTeamOverUnder, BasketBallResourceID.FullTime },
            { HomeTeamOverUnder1H, BasketBallResourceID.FirstHalf },
            { AwayTeamOverUnder1H, BasketBallResourceID.FirstHalf },
            { QuarterXOverUnder, "QX" },
            { QuarterXHomeTeamOverUnder, "QX" },
            { QuarterXAwayTeamOverUnder, "QX" },
            { MatchHandicapTotal, BasketBallResourceID.FullTime },
        };
    }
}
