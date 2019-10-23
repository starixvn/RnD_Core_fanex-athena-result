namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    public static class BadmintonResourceID
    {
        public const string TotalGame = "00";
        public const string Game1 = "0101";
        public const string Game2 = "0102";
        public const string Game3 = "0103";
        public const string Game4 = "0104";
        public const string Game5 = "0105";

        public const string GameXRaceToYPoints = "7120[1-5][0-1][1-9]";
        public const string GameXPointYWinner = "7130[1-5][0-5][0-9]";

        internal static readonly string[] MoreResultResourceIDRegex =
        {
            GameXRaceToYPoints,
            GameXPointYWinner,
        };

        internal static readonly string[] GameResourcesId =
        {
            Game1, Game2, Game3, Game4, Game5
        };
    }
}