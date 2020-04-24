namespace Fanex.Result.Core.Sportsbook.Utilities
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Sportsbook.DTO;

    public static class ConvertHomeAwayToText
    {
        public static string GetFirstGoalMethodResult(string homeValue)
        {
            switch (homeValue.ToUpperInvariant())
            {
                case "S":
                    return LangResource.Shot;

                case "H":
                    return LangResource.Header;

                case "P":
                    return LangResource.Penalty;

                case "FK":
                    return LangResource.FreeKick;

                case "OG":
                    return LangResource.OwnGoal;

                case "NG":
                    return LangResource.NoGoal;

                default:
                    return string.Empty;
            }
        }

        public static string GetResourceGameValueByPatternGoalTime(string homeValue)
        {
            if (string.IsNullOrEmpty(homeValue))
            {
                return ConstantValue.Hyphen;
            }

            return homeValue.Equals(ConstantValue.None, StringComparison.OrdinalIgnoreCase) ?
                LangResource.None : homeValue + " " + LangResource.Min;
        }

        public static string GetTextValueOfYesNoChoice(string choice)
        {
            if (string.IsNullOrEmpty(choice))
            {
                return ConstantValue.Hyphen;
            }

            return choice.Equals(ConstantValue.Yes, StringComparison.OrdinalIgnoreCase) ? LangResource.Yes : LangResource.No;
        }

        public static string GetResourceGameValueByTimeEvent(GameDetail detailedValue)
        {
            if ((detailedValue.HomeValue == "0" || detailedValue.HomeValue == "00")
                && (detailedValue.AwayValue == "0" || detailedValue.AwayValue == "00"))
            {
                return LangResource.NoCorner;
            }

            var homeValue = detailedValue.HomeValue.Trim().Length == 1 ? "0" + detailedValue.HomeValue : detailedValue.HomeValue;
            var awayValue = detailedValue.AwayValue.Trim().Length == 1 ? "0" + detailedValue.AwayValue : detailedValue.AwayValue;

            return string.Format(LangResource.MinSec, homeValue, awayValue);
        }

        public static string GetResourceGameValueByScoreMiss(string homeValue)
        {
            switch (homeValue.ToUpperInvariant())
            {
                case "S":
                    return LangResource.Goal;
                case "M":
                    return LangResource.Miss;
                case "NT":
                    return LangResource.NotTaken;
                default:
                    return ConstantValue.Hyphen;
            }
        }

        public static string GetResourceGameValueByBothOneNeith(string homeValue)
        {
            var resources = new Dictionary<string, string>
            {
                {"B",LangResource.Both },
                {"O",LangResource.One },
                {"N",LangResource.NoGoal },
                {"1SNT",LangResource.HomeScoreAwayNotTaken },
                {"1MNT",LangResource.HomeMissAwayNotTaken },
                {"NT2S",LangResource.HomeNotTakenAwayScore },
                {"NT2M",LangResource.HomeNotTakenAwayMiss }
            };

            return resources.ContainsKey(homeValue.ToUpperInvariant()) ? resources[homeValue.ToUpperInvariant()] : ConstantValue.Hyphen;
        }

        public static string GetResourceGameValueBy1X2Determinded(string homeValue)
        {
            var resources = new Dictionary<string, string>
            {
                {"1SNT",LangResource.HomeScoreAwayNotTaken },
                {"1MNT",LangResource.HomeMissAwayNotTaken },
                {"NT2S",LangResource.HomeNotTakenAwayScore },
                {"NT2M",LangResource.HomeNotTakenAwayMiss },
                {"1",LangResource.Home },
                {"X",LangResource.Draw },
                {"2",LangResource.Away }
            };

            return resources.ContainsKey(homeValue.ToUpperInvariant()) ? resources[homeValue.ToUpperInvariant()] : ConstantValue.Hyphen;
        }

        public static string GetResourceGameValueByWoodwork(GameDetail detailedValue, bool isHome)
        {
            if (detailedValue.SettlementStatusID == EventStatusID.CompletedSettlementStatusID)
            {
                var value = isHome ? detailedValue.HomeValue : detailedValue.AwayValue;
                switch (value.ToUpperInvariant())
                {
                    case "H":
                        return LangResource.Hit;
                    case "N":
                        return LangResource.NotHit;
                    default:
                        return ConstantValue.Hyphen;
                }
            }

            var home = detailedValue.HomeValue.ToUpperInvariant();
            var away = detailedValue.AwayValue.ToUpperInvariant();
            var resources = new Dictionary<string, string>
            {
                { "HNT", LangResource.HomeHitAwayNotTaken},
                { "NNT", LangResource.HomeNotHitAwayNotTaken},
                { "NTH", LangResource.HomeNotTakenAwayHit},
                { "NTN", LangResource.HomeNotTakenAwayNotHit}
            };

            return resources.ContainsKey(home + away) ? resources[home + away] : ConstantValue.Hyphen;
        }
    }
}
