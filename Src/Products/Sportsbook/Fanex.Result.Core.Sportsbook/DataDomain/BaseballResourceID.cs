using System.Collections.Generic;

namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    public static class BaseballResourceID
    {
        public const string FinalScore = "00";
        public const string ExtraTime = "EI";
        public const string Hits = "8816";

        public const string Inning1 = "0101";
        public const string Inning2 = "0102";
        public const string Inning3 = "0103";
        public const string Inning4 = "0104";
        public const string Inning5 = "0105";
        public const string Inning6 = "0106";
        public const string Inning7 = "0107";
        public const string Inning8 = "0108";
        public const string Inning9 = "0109";
        public const string InningOverTime = "EI";

        internal static readonly string[] AbandonedResourceIDs =
        {
            Inning1, Inning2, Inning3, Inning4, Inning5, Inning6, Inning7, Inning8, Inning9,
            InningOverTime
        };
    }
}