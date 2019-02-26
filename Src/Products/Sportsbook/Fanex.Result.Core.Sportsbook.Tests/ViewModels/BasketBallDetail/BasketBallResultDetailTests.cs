namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.BasketballDetail
{
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels.BasketBallDetail;

    [TestFixture]
    public class BasketBallResultDetailTests
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static readonly string[] MoreResultResourceIdsTestCase =
        {
            "604",
            "605",
            "60620",
            "60625",
            "60630",
            "60635",
            "60720",
            "60725",
            "60730",
            "60735",
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
            "61703",
            "61704"
        };

        private static readonly string[] MainResultResourceIdsTestCase =
        {
            "00", "01", "02", "Q1", "Q2", "Q3", "Q4", "OT"
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private IRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsBookHelper;
        private Sportsbook _sportsBook;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _sportsBookHelper = Substitute.For<ISportsbookHelperWraper>();
            _sportsBook = new Sportsbook();
            _setting = new ResultSetting();
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void CreateGameDetailItem_IsMoreResultResourceId_SetResourceNameByResourceId(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId });
            var betTypeId = resourceId.Substring(0, 3);
            var resourceTypeId = resourceId.Remove(0, 3);
            _refDataHelper.GetBetTypeNameById(betTypeId, "en-US", resourceTypeId).Returns("Bet Type Name");

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
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "h", SettlementStatusID = 4 });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en-US", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en-US", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Home Team", resultDetail.GetDetailItems().First().HomeValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsHomeTeam_DetailIsHomeIsTrue(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "h", SettlementStatusID = 4 });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.IsTrue(resultDetail.GetDetailItems().First().IsHome);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsAwayTeam_ShowAwayTeam(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "a", SettlementStatusID = 4 });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en-US", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en-US", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("Away Team", resultDetail.GetDetailItems().First().HomeValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsAwayTeam_DetailsIsAwayIsTrue(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "a", SettlementStatusID = 4 });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.IsTrue(resultDetail.GetDetailItems().First().IsAway);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsNone_ShowNone(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "n", SettlementStatusID = 4 });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            Assert.AreEqual("None", resultDetail.GetDetailItems().First().HomeValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMoreResultResourceId_IsNone_DetailsIsHomeAndIsAwayIsFalse(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "n", SettlementStatusID = 4 });
            _sportsBook.HomeId = 1;
            _sportsBook.AwayId = 2;
            _refDataHelper.GetTeamName(_sportsBook.HomeId, "en", string.Empty).Returns("Home Team");
            _refDataHelper.GetTeamName(_sportsBook.AwayId, "en", string.Empty).Returns("Away Team");

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(!detailItem.IsHome && !detailItem.IsAway);
        }

        [Test, TestCaseSource("MainResultResourceIdsTestCase")]
        public void MapGameValueByPattern_ValueIsNullOrEmpty_ShowHyphen(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(
                new GameDetail
                {
                    ResourceId = resourceId,
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID,
                    HomeValue = null,
                    AwayValue = string.Empty
                });

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(detailItem.HomeValue == "-" && detailItem.AwayValue == "-");
        }

        [Test, TestCaseSource("MainResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMainResultResourceId_HasCompletedSettlementStatusID_ShowValue(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, HomeValue = "14", AwayValue = "15", SettlementStatusID = 4 });

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(detailItem.HomeValue == "14" && detailItem.AwayValue == "15");
        }

        [Test, TestCaseSource("MainResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMainResultResourceId_HasRunningSettlementStatusID_ShowHyphen(string resourceId)
        {
            // Arrange
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, SettlementStatusID = EventStatusID.RunningSettlementStatusID });
            _sportsBook.BetStatus = ";0-0,0-0,0-0,0-0,0-0";

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(detailItem.HomeValue == "-" && detailItem.AwayValue == "-");
        }

        [Test, TestCaseSource("MainResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMainResultResourceId_HasRefundSettlementStatusID_ShowValue(string resourceId)
        {
            // Arrange
            var expectedResultValue = "Refund";
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, SettlementStatusID = EventStatusID.RefundSettlementStatusID });

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(detailItem.HomeValue == expectedResultValue && detailItem.AwayValue == expectedResultValue);
        }

        [Test, TestCaseSource("MainResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMainResultResourceId_HasRefundHTSettlementStatusID_ShowValue(string resourceId)
        {
            // Arrange
            var expectedResultValue = "Refund HT";
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, SettlementStatusID = EventStatusID.RefundHTSettlementStatusID });

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(detailItem.HomeValue == expectedResultValue && detailItem.AwayValue == expectedResultValue);
        }

        [Test, TestCaseSource("MainResultResourceIdsTestCase")]
        public void MapGameValueByPattern_IsMainResultResourceId_HasRefundFTSettlementStatusID_ShowValue(string resourceId)
        {
            // Arrange
            var expectedResultValue = "Refund FT";
            _sportsBook.GameDetails.Add(new GameDetail { ResourceId = resourceId, SettlementStatusID = EventStatusID.RefundFTSettlementStatusID });

            // Act
            var resultDetail = new BasketBallResultDetail(_sportsBook, _setting, _refDataHelper, _sportsBookHelper);

            // Assert
            var detailItem = resultDetail.GetDetailItems().First();
            Assert.IsTrue(detailItem.HomeValue == expectedResultValue && detailItem.AwayValue == expectedResultValue);
        }
    }
}