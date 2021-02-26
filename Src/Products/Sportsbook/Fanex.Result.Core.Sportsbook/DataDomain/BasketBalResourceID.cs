namespace Fanex.Result.Core.Sportsbook.DataDomain
{
    public static class BasketBallResourceID
    {
        public const string FullTime = "00";
        public const string FirstHalf = "01";
        public const string SecondHalf = "02";
        public const string Quarter1 = "Q1";
        public const string Quarter2 = "Q2";
        public const string Quarter3 = "Q3";
        public const string Quarter4 = "Q4";
        public const string OverTime = "OT";

        public const string FTWhichTimeToScoreTheFirstBasket = "604";
        public const string FTWhichTimeToScoreTheLastBasket = "605";

        public const string FirstHalfRaceTo20Points = "60620";
        public const string FirstHalfRaceTo25Points = "60625";
        public const string FirstHalfRaceTo30Points = "60630";
        public const string FirstHalfRaceToXXPoints = "606[0-9][0-9]";

        public const string SecondHalfRaceTo20Points = "60720";
        public const string SecondHalfRaceTo25Points = "60725";
        public const string SecondHalfRaceTo30Points = "60730";
        public const string SecondHalfRaceToXXPoints = "607[0-9][0-9]";

        public const string Quarter1RaceToXXPoints = "61301[0-9][0-9]";
        public const string Quarter2RaceToXXPoints = "61302[0-9][0-9]";
        public const string Quarter3RaceToXXPoints = "61303[0-9][0-9]";
        public const string Quarter4RaceToXXPoints = "61304[0-9][0-9]";
        public const string Quarter1WhichTeamToScoreTheLastBasket = "61701";
        public const string Quarter2WhichTeamToScoreTheLastBasket = "61702";
        public const string Quarter3WhichTeamToScoreTheLastBasket = "61703";
        public const string Quarter4WhichTeamToScoreTheLastBasket = "61704";

        internal static readonly string[] MoreResultResourceIDRegex =
        {
            FirstHalfRaceToXXPoints,
            SecondHalfRaceToXXPoints,
            Quarter1RaceToXXPoints,
            Quarter2RaceToXXPoints,
            Quarter3RaceToXXPoints,
            Quarter4RaceToXXPoints
        };

        internal static readonly string[] MoreResultResourceIDs =
        {
            Quarter1RaceToXXPoints,
            Quarter1WhichTeamToScoreTheLastBasket,
            Quarter2RaceToXXPoints,
            Quarter2WhichTeamToScoreTheLastBasket,
            FirstHalfRaceToXXPoints,
            Quarter3RaceToXXPoints,
            Quarter3WhichTeamToScoreTheLastBasket,
            Quarter4RaceToXXPoints,
            Quarter4WhichTeamToScoreTheLastBasket,
            SecondHalfRaceToXXPoints,
            FTWhichTimeToScoreTheFirstBasket,
            FTWhichTimeToScoreTheLastBasket
        };

        internal static readonly string[] MainResultResourceIDs =
        {
            FullTime, FirstHalf, SecondHalf, Quarter1, Quarter2, Quarter3, Quarter4, OverTime
        };
    }
}