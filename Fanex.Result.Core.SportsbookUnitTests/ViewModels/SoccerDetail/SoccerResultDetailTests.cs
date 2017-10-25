namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.SoccerDetail
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DataDomain;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels.SoccerDetail;
    using Sportsbook.ViewModels.SoccerInfo;

    [TestFixture]
    public class SoccerResultDetailTests
    {
        private ISportsbookRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsbookHelper;
        private Sportsbook _sportsbook;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
            _sportsbookHelper = Substitute.For<ISportsbookHelperWraper>();
            _sportsbook = new Sportsbook();
            _setting = new ResultSetting();
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
        }

        [Test]
        public void GetDetailItems_Always_GetInBetRadarResourceId()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.TimeOfFirstGoal, HomeValue = "11-20" },
                new GameDetail { ResourceId = "234234", HomeValue = "11-20" }
            };
            var soccerResultDetail = new SoccerResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual(1, actualDetailItems.Count);
        }

        [Test]
        public void GetDetailItems_Always_ReturnCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> { new GameDetail { ResourceId = GameResourceID.TimeOfFirstGoal, HomeValue = "11-20" } };
            var soccerResultDetail = new SoccerResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.True(
                actualDetailItems.First().GroupCode == "GoalTime" &&
                actualDetailItems.First().ResourceID == "455" &&
                actualDetailItems.First().GroupName == "Goal Time" &&
                actualDetailItems.First().ResourceName == "Time Of First Goal");
        }

        [TestCase("11-20", "10:01-20:00")]
        [TestCase("1-10", "00:01-10:00")]
        [TestCase("NG", "No Goal")]
        public void MapGameValueByPattern_WhenResourceIdIs455_ReturnExpectedHomeValue(string homeValue, string expectedHomeValue)
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> { new GameDetail { ResourceId = GameResourceID.TimeOfFirstGoal, HomeValue = homeValue } };
            var soccerResultDetail = new SoccerResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual(expectedHomeValue, actualDetailItems.FirstOrDefault().HomeValue);
        }
    }
}