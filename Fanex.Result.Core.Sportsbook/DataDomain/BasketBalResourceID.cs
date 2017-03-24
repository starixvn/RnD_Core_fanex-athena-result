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

        public const string SecondHalfRaceTo20Points = "60720";
        public const string SecondHalfRaceTo25Points = "60725";
        public const string SecondHalfRaceTo30Points = "60730";

        public const string Quarter1RaceTo10Points = "6130110";
        public const string Quarter1RaceTo15Points = "6130115";
        public const string Quarter1RaceTo20Points = "6130120";

        public const string Quarter2RaceTo10Points = "6130210";
        public const string Quarter2RaceTo15Points = "6130215";
        public const string Quarter2RaceTo20Points = "6130220";

        public const string Quarter3RaceTo10Points = "6130310";
        public const string Quarter3RaceTo15Points = "6130315";
        public const string Quarter3RaceTo20Points = "6130320";

        public const string Quarter4RaceTo10Points = "6130410";
        public const string Quarter4RaceTo15Points = "6130415";
        public const string Quarter4RaceTo20Points = "6130420";

        public const string Quarter1WhichTeamToScoreTheLastBasket = "61701";
        public const string Quarter2WhichTeamToScoreTheLastBasket = "61702";
        public const string Quarter3WhichTeamToScoreTheLastBasket = "61703";

        public static readonly string[] MoreResultResourceIDs =
        {
            Quarter1RaceTo10Points,
            Quarter1RaceTo15Points,
            Quarter1RaceTo20Points,
            Quarter1WhichTeamToScoreTheLastBasket,
            Quarter2RaceTo10Points,
            Quarter2RaceTo15Points,
            Quarter2RaceTo20Points,
            Quarter2WhichTeamToScoreTheLastBasket,
            FirstHalfRaceTo20Points,
            FirstHalfRaceTo25Points,
            FirstHalfRaceTo30Points,
            Quarter3RaceTo10Points,
            Quarter3RaceTo15Points,
            Quarter3RaceTo20Points,
            Quarter3WhichTeamToScoreTheLastBasket,
            Quarter4RaceTo10Points,
            Quarter4RaceTo15Points,
            Quarter4RaceTo20Points,
            SecondHalfRaceTo20Points,
            SecondHalfRaceTo25Points,
            SecondHalfRaceTo30Points,
            FTWhichTimeToScoreTheFirstBasket,
            FTWhichTimeToScoreTheLastBasket
        };

        public static readonly string[] MainResultResourceIDs =
        {
            FullTime, FirstHalf, SecondHalf, Quarter1, Quarter2, Quarter3, Quarter4, OverTime
        };
    }
}