// -----------------------------------------------------------------------
// <copyright file="SportType.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.Base.Extension;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Reviewed.")]
    public enum SportType : int
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

        [StringValue("Billiard")]
        ESport = 43,

        [StringValue("Billiard")]
        Winter = 29,

        [StringValue("Billiard")]
        HorseRacingFixedOdd = 154,

        #endregion Billiard

        #region Racing

        [StringValue("Racing")]
        HorseRacing = 151,

        [StringValue("Racing")]
        Greyhound = 152,

        [StringValue("Racing")]
        Harness = 153,

        #endregion Racing

        [StringValue("NumberGame")]
        NumberGame = 161,

        [StringValue("LiveCasino")]
        LiveCasino = 162,

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
        Bingo = 252,

        [StringValue("Other")]
        P2P = 253,

        [StringValue("Other")]
        Others = 1000,

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

        #endregion VirtualSport

        [StringValue("MicroGamingLC")]
        MicroGamingLC = 203,

        [StringValue("MicroGamingRNG")]
        MicroGamingRNG = 205,
        [StringValue("AllBetCasino")]
        AllBetCasino = 211,

        [StringValue("AGCasino")]
        AGCasino = 250,

        [StringValue("GoldDeluxe")]
        GoldDeluxe = 255


    }
}