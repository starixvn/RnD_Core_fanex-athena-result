using System.Collections.Generic;
using Fanex.Result.Core.Sportsbook.Resources;

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

        public const string PlayerPoint = "653[0-9][0-9]";
        public const string PlayerAssist = "656[0-9][0-9]";
        public const string PlayerRebound = "659[0-9][0-9]";
        public const string Player3Point = "662[0-9][0-9]";
        public const string PlayerBlock = "665[0-9][0-9]";
        public const string PlayerSteal = "667[0-9][0-9]";
        public const string PlayerMostPoints = "680[0-9][0-9]";
        public const string PlayerMostRebounds = "681[0-9][0-9]";
        public const string PlayerMostSteals = "682[0-9][0-9]";
        public const string PlayerMostAssists = "683[0-9][0-9]";
        public const string PlayerMost3Point = "684[0-9][0-9]";

        public const int MinPropBettype = 653;
        public const int MaxPropBettype = 688;
        public const string PlayerPointAtLeast = "653";
        public const string PlayerPointOver = "654";
        public const string PlayerPointExact = "655";
        public const string PlayerPointBands = "685";

        public const string PlayerAssistAtLeast = "656";
        public const string PlayerAssistOver = "657";
        public const string PlayerAssistExact = "658";
        public const string PlayerAssistBands = "686";

        public const string PlayerReboundAtLeast = "659";
        public const string PlayerReboundOver = "660";
        public const string PlayerReboundExact = "661";
        public const string PlayerReboundBands = "687";

        public const string Player3PointAtLeast = "662";
        public const string Player3PointOver = "663";
        public const string Player3PointExact = "664";
        public const string Player3PointBands = "688";

        public const string PlayerBlockAtLeast = "665";
        public const string PlayerBlockOver = "666";

        public const string PlayerStealsAtLeast = "667";
        public const string PlayerStealsOver = "668";

        //Associated props resource IDs
        public const string PlayerComboPRAAtLeast = "669[0-9][0-9]";
        public const string PlayerComboPRAOver = "670[0-9][0-9]";

        
        public const string PlayerComboRAAtLeast = "671[0-9][0-9]";
        public const string PlayerComboRAOver = "672[0-9][0-9]";

        public const string PlayerComboPROver = "673[0-9][0-9]";

        public const string PlayerComboSBOver = "674[0-9][0-9]";
        public const string PlayerComboPerOther = "675[0-9][0-9][0-9][0-9]";
        public const string PlayerComboPerDoubleOther = "676[0-9][0-9][0-9][0-9]";
        public const string PlayerComboDoubleDouble = "677[0-9][0-9]";
        public const string PlayerComboTripleOther = "678[0-9][0-9][0-9][0-9]";
        public const string PlayerComboTripleDouble = "679[0-9][0-9]";


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

        internal static readonly string[] AbandonedResourceIDs =
        {
            Quarter1, Quarter2, Quarter3, Quarter4, OverTime, FirstHalf, FullTime, SecondHalf
        };

        internal static readonly string[] PlayerProsMoreResultResourceIDs =
        {
            PlayerPoint,
            PlayerAssist,
            PlayerRebound,
            Player3Point,
            PlayerBlock,
            PlayerSteal,
        };

        internal static readonly Dictionary<string, string> PlayerPropsHeaderResourceIDs = new Dictionary<string, string>
        {
            { PlayerPoint, SportsbookResx.Basketball_653 },
            { PlayerAssist, SportsbookResx.Basketball_656 },
            { PlayerRebound, SportsbookResx.Basketball_659 },
            { Player3Point, SportsbookResx.Basketball_662 },
            { PlayerBlock, SportsbookResx.Basketball_665 },
            { PlayerSteal, SportsbookResx.Basketball_667 }
        };

        internal static readonly string[] DirectPropResourceIDs =
        {
            PlayerPointAtLeast,
            PlayerPointOver,
            PlayerPointExact,
            PlayerPointBands,
            PlayerAssistAtLeast,
            PlayerAssistOver,
            PlayerAssistExact,
            PlayerAssistBands,
            PlayerReboundAtLeast,
            PlayerReboundOver,
            PlayerReboundExact,
            PlayerReboundBands,
            Player3PointAtLeast,
            Player3PointOver,
            Player3PointExact,
            Player3PointBands,
            PlayerBlockAtLeast,
            PlayerBlockOver,
            PlayerStealsAtLeast,
            PlayerStealsOver
        };

        internal static readonly string[] AssociatedPropsResourceIDs =
        {
            PlayerComboPRAAtLeast,
            PlayerComboPRAOver,
            PlayerComboRAAtLeast,
            PlayerComboRAOver,
            PlayerComboPROver,
            PlayerComboSBOver,
            PlayerComboPerOther,
            PlayerComboPerDoubleOther,
            PlayerComboDoubleDouble,
            PlayerComboTripleOther,
            PlayerComboTripleDouble,
            PlayerMostPoints,
            PlayerMostRebounds,
            PlayerMostSteals,
            PlayerMostAssists,
            PlayerMost3Point
        };
    }
}