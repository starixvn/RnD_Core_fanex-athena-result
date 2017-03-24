namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.SoccerDetail
{
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels.BasketBallDetail;

    [TestFixture]
    public class BasketBallResultDetailTests
    {
        private static readonly string[] MoreResultResourceIdsTestCase =
        {
            "604",
            "605",
            "60620",
            "60625",
            "60630",
            "60720",
            "60725",
            "60730",
            "6130110",
            "6130115",
            "6130120",
            "6130210",
            "6130215",
            "6130220",
            "6130310",
            "6130315",
            "6130320",
            "6130410",
            "6130415",
            "6130420",
            "61701",
            "61702",
            "61703"
        };

        private ISportsbookRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsBookHelper;
        private Sportsbook _sportsBook;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
            _sportsBookHelper = Substitute.For<ISportsbookHelperWraper>();
            _sportsBook = new Sportsbook();
            _setting = new ResultSetting();
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void CreateGameDetailItem_IsMoreResultResourceId_SetResourceNameByResourceId(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId });
            var betTypeId = resourceId.Substring(0, 3);
            var resourceTypeId = resourceId.Remove(0, 3);
            _refDataHelper.GetBetTypeNameById(betTypeId, "en", resourceTypeId).Returns("Bet Type Name");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Bet Type Name", resultDetail.GetDetailItems().First().ResourceName);
        }

        [Test]
        public void CreateGameDetailItem_IsNotMoreResultResourceId_ResourceNameIsNull()
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = "XXX" });

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.IsNull(resultDetail.GetDetailItems().First().ResourceName);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsHomeTeam_ShowHomeTeam(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "h" });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Home Team", resultDetail.GetDetailItems().First().HomeValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsAwayTeam_ShowAwayTeam(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "a" });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Away Team", resultDetail.GetDetailItems().First().HomeValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsNone_ShowNone(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "n" });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("None", resultDetail.GetDetailItems().First().HomeValue);
        }
    }
}