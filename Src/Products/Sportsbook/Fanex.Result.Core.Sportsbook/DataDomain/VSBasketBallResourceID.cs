namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    public static class VSBasketBallResourceID
    {
        public const string FTRaceTo20Points = "280920";
        public const string FTRaceTo40Points = "280940";
        public const string FTRaceTo60Points = "280960";

        internal static readonly string[] MoreResultResourceIDs =
        {
            FTRaceTo20Points,
            FTRaceTo40Points,
            FTRaceTo60Points
        };
    }
}