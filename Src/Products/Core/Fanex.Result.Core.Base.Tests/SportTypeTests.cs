namespace Fanex.Result.Core.Base.Tests
{
    using System.Collections.Generic;
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class SportTypeTests
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static int[] sportTypeList = {1, 44, 2, 32, 3, 26, 4, 28, 5, 6, 9,  30, 48, 10, 11, 7 ,33, 34, 35, 36, 37, 38, 39, 40, 41
        ,42, 43, 29, 154, 51, 52, 53, 54, 55, 161, 12, 13, 14, 15, 16, 17, 18 ,19 ,20, 21, 22, 23, 24, 25, 27,
        47, 49, 56, 50, 99, 201, 251, 31, 180, 252, 253, 1000, 190, 191, 192, 181, 182, 183, 184, 185, 186, 193, 203, 205, 211, 250, 255
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private static Dictionary<int, string> SportTypeDtoMapping = new Dictionary<int, string>
        {
            {1, "Soccer" },
            {44, "MuayThai"},
            {2, "BasketBall"},
            {32, "BasketBall"},
            {3, "BasketBall"},
            {26, "BasketBall"},
            {4, "Hockey"},
            {28, "Hockey"},
            {5, "Tennis"},
            {6, "Badminton"},
            {9, "Badminton"},
            {30, "Badminton"},
            {48, "Badminton"},
            {10, "Billiard"},
            {11, "Billiard"},
            {7, "Billiard"},
            {33, "Billiard"},
            {34, "Billiard"},
            {35, "Billiard"},
            {36, "Billiard"},
            {37, "Billiard"},
            {38, "Billiard"},
            {39, "Billiard"},
            {40, "Billiard"},
            {41, "Billiard"},
            {42, "Billiard"},
            {43, "ESport"},
            {29, "Billiard"},
            {154, "Billiard"},
            {51, "BeachSoccer"},
            {52, "BeachSoccer"},
            {53, "BeachSoccer"},
            {54, "BeachSoccer"},
            {55, "BeachSoccer"},           
            {161, "NumberGame"},
            {12, "Other"},
            {13, "Other"},
            {14, "Other"},
            {15, "Other"},
            {16, "Other"},
            {17, "Other"},
            {18, "Other"},
            {19, "Other"},
            {20, "Other"},
            {21, "Other"},
            {22, "Other"},
            {23, "Other"},
            {24, "Other"},
            {25, "Other"},
            {27, "Other"},
            {47, "Kabaddi"},
            {49, "Kabaddi"},
            {56, "Kabaddi"},
            {50, "Other"},
            {99, "Other"},
            {201, "Other"},
            {251, "Other"},
            {31, "Other"},
            {180, "Other"},
            {252, "Other"},
            {253, "Other"},
            {1000, "Other"},
            {190, "Other"},
            {191, "Other"},
            {192, "Other"},
            {181, "VSHRacing"},
            {182, "VSHRacing"},
            {183, "VSSpeedWay"},
            {184, "VSHRacing"},
            {185, "VSHRacing"},
            {186, "VSTennis"},
            {193, "VSBasketBall"},
            {203, "MicroGamingLC"},
            {205, "MicroGamingRNG"},
            {211, "AllBetCasino"},
            {250, "AGCasino"},
            {255, "GoldDeluxe"},
        };

        [TestCaseSource("sportTypeList")]
        public void SportType_Always_GetCorrectDTOName(int sportType)
        {
            // Act
            var dtoName = ((SportType)sportType).StringMe();

            // Assert
            Assert.AreEqual(SportTypeDtoMapping[sportType], dtoName);
        }
    }
}