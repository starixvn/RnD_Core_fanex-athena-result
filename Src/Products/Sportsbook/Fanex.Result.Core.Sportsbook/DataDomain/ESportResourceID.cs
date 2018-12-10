namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    public static class ESportResourceID
    {
        internal const string FinalScore = "00";
        internal const string Map1 = "M1";
        internal const string Map2 = "M2";
        internal const string Map3 = "M3";
        internal const string Map4 = "M4";
        internal const string Map5 = "M5";
        internal const string Map6 = "M6";
        internal const string Map7 = "M7";
        internal const string Map8 = "M8";
        internal const string Map9 = "M9";

        internal const string Refund = "R";
        internal const string Name0X = "0X";
        internal const string NameOY = "0Y";

        internal static readonly string[] MainResultResourceIDs =
        {
            Map1, Map2, Map3, Map4, Map5, Map6, Map7, Map8, Map9, FinalScore
        };

        internal const string MoreResultResourceIDRegex = "[0-9][0-9]";
    }
}