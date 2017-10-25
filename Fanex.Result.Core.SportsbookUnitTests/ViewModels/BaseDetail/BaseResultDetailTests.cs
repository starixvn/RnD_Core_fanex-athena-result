namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.BaseDetail
{
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels.BaseDetail;

    [TestFixture]
    public class BaseResultDetailTests
    {
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
        }

        [Test]
        public void CreateGameDetailItem_Always_ResourceIdIsSameWithGameDetail()
        {
            // Arrange
            var expectedResourceId = "01";
            var detailItem = new GameDetail { ResourceId = expectedResourceId };
            _sportsBook.GameDetails.Add(detailItem);

            // Act
            var resultDetail = new BaseResultDetail<GameDetailItem>(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);
            var actualDetailItem = resultDetail.GetDetailItems().FirstOrDefault();

            // Assert
            Assert.AreEqual(expectedResourceId, actualDetailItem.ResourceID);
        }

        [TestCase(EventStatusID.RefundSettlementStatusID, "Refund")]
        [TestCase(EventStatusID.RefundFTSettlementStatusID, "Refund FT")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "Refund HT")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "-")]
        public void MapGameValueByStatusId_RefundSettlementStatusID_SetRefundTextInHomeAndAwayValue(int settlementStatusId, string expectedResultValue)
        {
            // Arrange
            var detailItem = new GameDetail { SettlementStatusID = settlementStatusId };
            _sportsBook.GameDetails.Add(detailItem);

            // Act
            var resultDetail = new BaseResultDetail<GameDetailItem>(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);
            var actualDetailItem = resultDetail.GetDetailItems().FirstOrDefault();

            // Assert
            Assert.IsTrue(expectedResultValue == actualDetailItem.HomeValue &&
                expectedResultValue == actualDetailItem.AwayValue);
        }

        [TestCase(EventStatusID.CompletedSettlementStatusID)]
        [TestCase(EventStatusID.RunningSettlementStatusID)]
        public void MapGameValueByPattern_NotRefundSettlementStatusID_SetHomeValueIsHomeValue(int settlementStatusId)
        {
            // Arrange
            var detailItem = new GameDetail
            {
                SettlementStatusID = settlementStatusId,
                HomeValue = "h"
            };
            _sportsBook.GameDetails.Add(detailItem);

            // Act
            var resultDetail = new BaseResultDetail<GameDetailItem>(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);
            var actualDetailItem = resultDetail.GetDetailItems().FirstOrDefault();

            // Assert
            Assert.AreEqual("h", actualDetailItem.HomeValue);
        }

        [TestCase(EventStatusID.CompletedSettlementStatusID)]
        [TestCase(EventStatusID.RunningSettlementStatusID)]
        public void MapGameValueByPattern_NotRefundSettlementStatusID_SetAwayValueIsAwayValue(int settlementStatusId)
        {
            // Arrange
            var detailItem = new GameDetail
            {
                SettlementStatusID = settlementStatusId,
                AwayValue = "a"
            };
            _sportsBook.GameDetails.Add(detailItem);

            // Act
            var resultDetail = new BaseResultDetail<GameDetailItem>(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);
            var actualDetailItem = resultDetail.GetDetailItems().FirstOrDefault();

            // Assert
            Assert.AreEqual("a", actualDetailItem.AwayValue);
        }

        [Test]
        public void HomeTeam_Always_ShowHomeTeamName()
        {
            // Arrange
            _sportsBook.HomeId = 1;
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");

            // Act
            var resultDetail = new BaseResultDetail<GameDetailItem>(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Home Team", resultDetail.HomeTeam);
        }

        [Test]
        public void AwayTeam_Always_ShowAwayTeamName()
        {
            // Arrange
            _sportsBook.AwayId = 1;
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BaseResultDetail<GameDetailItem>(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Away Team", resultDetail.AwayTeam);
        }
    }
}