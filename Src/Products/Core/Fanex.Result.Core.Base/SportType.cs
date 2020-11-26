namespace Fanex.Result.Core.Base
{
    using Extension;

    public enum SportType
    {
        [StringValue("Soccer")]
        Soccer = 1,

        [StringValue("MuayThai")]
        MuayThai = 44,

        #region BasketBall

        [StringValue("BasketBall")]
        BasketBall = 2,

        [StringValue("BasketBall")]
        NetBall = 32,

        [StringValue("BasketBall")]
        FootBall = 3,

        [StringValue("BasketBall")]
        Rugby = 26,

        #endregion BasketBall

        #region Hockey

        [StringValue("Hockey")]
        IceHockey = 4,

        [StringValue("Hockey")]
        FieldHockey = 28,

        #endregion Hockey

        [StringValue("Tennis")]
        Tennis = 5,

        #region Badminton

        [StringValue("Badminton")]
        VolleyBall = 6,

        [StringValue("Badminton")]
        Badminton = 9,

        [StringValue("Badminton")]
        Squash = 30,

        [StringValue("Badminton")]
        SepakTakraw = 48,

        #endregion Badminton

        [StringValue("BaseBall")]
        BaseBall = 8,

        #region Billiard

        [StringValue("Billiard")]
        Golf = 10,

        [StringValue("Billiard")]
        Motorsports = 11,

        [StringValue("Billiard")]
        Billard = 7,

        [StringValue("Billiard")]
        Cycling = 33,

        [StringValue("Billiard")]
        Fencing = 34,

        [StringValue("Billiard")]
        Judo = 35,

        [StringValue("Billiard")]
        ModernPentathlon = 36,

        [StringValue("Billiard")]
        Rowing = 37,

        [StringValue("Billiard")]
        Sailing = 38,

        [StringValue("Billiard")]
        Shooting = 39,

        [StringValue("Billiard")]
        Taekwondo = 40,

        [StringValue("Billiard")]
        Triathlon = 41,

        [StringValue("Billiard")]
        Wrestling = 42,

        [StringValue("ESport")]
        ESport = 43,

        [StringValue("Billiard")]
        Winter = 29,

        #endregion Billiard        

        [StringValue("NumberGame")]
        NumberGame = 161,

        [StringValue("Happy5")]
        Happy5 = 164,

        #region Other

        [StringValue("Other")]
        Swimming = 12,

        [StringValue("Other")]
        Politics = 13,

        [StringValue("Other")]
        WaterPolo = 14,

        [StringValue("Other")]
        Diving = 15,

        [StringValue("Other")]
        Boxing = 16,

        [StringValue("Other")]
        Archery = 17,

        [StringValue("Other")]
        TableTennis = 18,

        [StringValue("Other")]
        Weightlifting = 19,

        [StringValue("Other")]
        Canoeing = 20,

        [StringValue("Other")]
        Gymnastics = 21,

        [StringValue("Other")]
        Athletics = 22,

        [StringValue("Other")]
        Equestrian = 23,

        [StringValue("Other")]
        Handball = 24,

        [StringValue("Other")]
        Dart = 25,

        [StringValue("Other")]
        Cricket = 27,

        [StringValue("Other")]
        CricketIndia = 50,

        [StringValue("Other")]
        OtherSports = 99,

        [StringValue("Other")]
        Finance = 201,

        [StringValue("Other")]
        Casino = 251,

        [StringValue("Other")]
        Entertainment = 31,

        [StringValue("Other")]
        VirtualSports = 180,

        [StringValue("Other")]
        Others = 1000,

        [StringValue("Other")]
        VirtualSoccerLeague = 190,

        [StringValue("Other")]
        VirtualSoccerNation = 191,

        [StringValue("Other")]
        VirtualSoccerWorldCup = 192,

        [StringValue("Other")]
        VirtualSoccerAsianCup = 194,

        [StringValue("Other")]
        VirtualSoccerEnglishPremier = 195,

        [StringValue("Other")]
        VirtualSoccerChampionsCup = 196,

        [StringValue("Other")]
        VirtualSoccerEuroCup = 197,
        #endregion Other

        #region VirtualSport

        [StringValue("VSHRacing")]
        VirtualHorseRacing = 181,

        [StringValue("VSHRacing")]
        VirtualGreyhounds = 182,

        [StringValue("VSSpeedWay")]
        VirtualSpeedway = 183,

        [StringValue("VSHRacing")]
        VirtualMotorRacing = 184,

        [StringValue("VSHRacing")]
        VirtualCycling = 185,

        [StringValue("VSTennis")]
        VirtualTennis = 186,

        [StringValue("VSBasketBall")]
        VirtualBasketBall = 193,

        #endregion VirtualSport

        [StringValue("BBin")]
        BBin = 241,

        [StringValue("SexyGaming")]
        SexyGaming = 243,

        [StringValue("AllBetCasino")]
        AllBetCasino = 211,

        [StringValue("AGCasino")]
        AGCasino = 250,

        [StringValue("GoldDeluxe")]
        GoldDeluxe = 255,

        [StringValue("Kabaddi")]
        Kabaddi = 47,

        [StringValue("Kabaddi")]
        Futsal = 49,

        [StringValue("Kabaddi")]
        UpcomingSport56 = 56,

        [StringValue("BeachSoccer")]
        BeachSoccer = 51,

        [StringValue("BeachSoccer")]
        UpcomingSport52 = 52,

        [StringValue("BeachSoccer")]
        UpcomingSport53 = 53,

        [StringValue("BeachSoccer")]
        UpcomingSport54 = 54,

        [StringValue("BeachSoccer")]
        UpcomingSport55 = 55,

        [StringValue("BeachSoccer")]
        UpcomingSport57 = 57,

        [StringValue("BeachSoccer")]
        UpcomingSport58 = 58,

        [StringValue("BeachSoccer")]
        UpcomingSport59 = 59,

        [StringValue("BeachSoccer")]
        UpcomingSport60 = 60,

        [StringValue("BeachSoccer")]
        UpcomingSport61 = 61,

        [StringValue("BeachSoccer")]
        UpcomingSport62 = 62,

        [StringValue("BeachSoccer")]
        UpcomingSport63 = 63,

        [StringValue("BeachSoccer")]
        UpcomingSport64 = 64,
        
        [StringValue("BeachSoccer")]
        UpcomingSport65 = 65,

        [StringValue("BeachSoccer")]
        UpcomingSport66 = 66,

        [StringValue("SAGaming")]
        SAGaming = 244,

        [StringValue("GTCasino")]
        GTCasino = 242,

        [StringValue("WM")]
        WM = 233,
    }
}