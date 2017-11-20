namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using Base.Extension;
    using DTO;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;
    using Sportsbook.ViewModels.BaseDetail;
    using Sportsbook.ViewModels.BasketBallDetail;

    [TestFixture]
    public class BasketBallViewModelTests
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
            "61703",
            "61704"
        };

        public static readonly string[] OrderedMoreResultResourceIdsTestCase =
        {
           "6130110",
            "6130115",
            "6130120",
            "61701",
            "6130210",
            "6130215",
            "6130220",
            "61702",
            "606[0-9][0-9]",
            "6130310",
            "6130315",
            "6130320",
            "61703",
            "6130410",
            "6130415",
            "6130420",
            "61704",
            "607[0-9][0-9]",
            "604",
            "605"
        };

        private Sportsbook _sportsbook;
        private ResultSetting _setting;
        private IResultDetail<BasketBallGameDetailItem> _resultDetail;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new Sportsbook();
            _setting = new ResultSetting();
            _resultDetail = Substitute.For<IResultDetail<BasketBallGameDetailItem>>();
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan4_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new BasketBallViewModel(_sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(10, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexIs4AndBetStatusIsNullOrEmptyOrNotContainsComma_ReturnsZero(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BasketBallViewModel(sportsbookDto, new ResultSetting());
            var index = 4;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("0", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexLessThan4AndBetStatusIsNullOrEmptyOrNotContainsComma_ReturnsHyphen(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BasketBallViewModel(sportsbookDto, new ResultSetting());
            var index = 3;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIs4AndBetStatusHasLessThan5Sets_ReturnsZero()
        {
            // Arrange
            var betStatus = ";1-2,3-1,2-3,1-2";
            var index = 4;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BasketBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("0", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan4AndBetStatusSetsIsNotGreaterThanIndex_ReturnsHyphen()
        {
            // Arrange
            var index = 3;
            var betStatus = ";1-2,3-1";
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BasketBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(2, ";1-2,3-1,2-3", true, "2")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,3-3", false, "3")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,", false, "3")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,-", false, "3")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,0", false, "3")]
        [TestCase(0, ";1-2,3-1,2-3,1-2,3-3", false, "2")]
        public void GetHomeAwaySet_FromIndexLessThan5AndBetStatusSets_ReturnsExpectedHomeAwaySet(
            int index,
            string betStatus,
            bool isHome,
            string expectedHomeAwaySet)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BasketBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, isHome);

            // Assert
            Assert.AreEqual(expectedHomeAwaySet, homeAwaySet);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void GetHomeAwaySet_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "Q1", HomeValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.GetHomeAwaySet(0, true);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void GetHomeAwaySet_HasNoResultDetailData_BetStatusIsRefund_ShowHyphen(int setIndex, bool isHome)
        {
            // Arrange
            _sportsbook.BetStatus = "Refund;";
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.GetHomeAwaySet(setIndex, isHome);

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void GetHomeAwaySet_HasNoResultDetailData_BetStatusHasValue_ShowValue(int setIndex, bool isHome)
        {
            // Arrange
            _sportsbook.BetStatus = ";0-0,0-0,0-0,0-0,10-3";
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.GetHomeAwaySet(setIndex, isHome);

            // Assert
            Assert.AreEqual("0", actualValue);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void OTHome_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "OT", HomeValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.OTHome;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void OTHome_NoResultDetailData_ValueIsGreaterThanZero_ShowValue()
        {
            // Arrange
            _sportsbook.BetStatus = ";1-2,3-1,2-3,1-2,10-3";
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.OTHome;

            // Assert
            Assert.AreEqual("10", actualValue);
        }

        [Test]
        public void OTHome_NoResultDetailData_ValueIsEqualZero_ShowHyphen()
        {
            // Arrange
            _sportsbook.BetStatus = ";1-2,3-1,2-3,1-2,0-0";
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.OTHome;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void OTAway_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "OT", AwayValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.OTAway;

            // Assert
            Assert.AreEqual(actualValue, expectedValue);
        }

        [Test]
        public void OTAway_NoResultDetailData_ValueIsGreaterThanZero_ShowValue()
        {
            // Arrange
            _sportsbook.BetStatus = ";1-2,3-1,2-3,1-2,10-3";
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.OTAway;

            // Assert
            Assert.AreEqual("3", actualValue);
        }

        [Test]
        public void OTAway_NoResultDetailData_ValueIsEqualZero_ShowHyphen()
        {
            // Arrange
            _sportsbook.BetStatus = ";1-2,3-1,2-3,1-2,10-0";
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.OTAway;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void HTHomeScore_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "01", HomeValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.HTHomeScore;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void HTHomeScore_NoResultDetailData_HtHomeScoreHasValue_ShowValue()
        {
            // Arrange
            _sportsbook.HtHomeScore = 10;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.HTHomeScore;

            // Assert
            Assert.AreEqual("10", actualValue);
        }

        [Test]
        public void HTHomeScore_NoResultDetailData_HtHomeScoreHasNoValue_ShowHyphen()
        {
            // Arrange
            _sportsbook.HtHomeScore = null;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.HTHomeScore;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void HTAwayScore_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "01", AwayValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.HTAwayScore;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void HTAwayScore_NoResultDetailData_HtAwayScoreHasValue_ShowValue()
        {
            // Arrange
            _sportsbook.HtAwayScore = 10;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.HTAwayScore;

            // Assert
            Assert.AreEqual("10", actualValue);
        }

        [Test]
        public void HTAwayScore_NoResultDetailData_HtAwayScoreHasNoValue_ShowHyphen()
        {
            // Arrange
            _sportsbook.HtAwayScore = null;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.HTAwayScore;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void FinalHomeScore_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "00", HomeValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void FinalHomeScore_NoResultDetailData_FinalHomeScoreHasValue_ShowValue()
        {
            // Arrange
            _sportsbook.FinalHomeScore = 10;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("10", actualValue);
        }

        [Test]
        public void FinalHomeScore_NoResultDetailData_FinalHomeScoreHasNoValue_ShowHyphen()
        {
            // Arrange
            _sportsbook.FinalHomeScore = null;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [TestCase("10")]
        [TestCase("Refund")]
        [TestCase("RefundHT")]
        [TestCase("RefundFT")]
        public void FinalAwayScore_HasResultDetailData_ReturnExpectedValue(string expectedValue)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "00", AwayValue = expectedValue };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void FinalAwayScore_NoResultDetailData_FinalAwayScoreHasValue_ShowValue()
        {
            // Arrange
            _sportsbook.FinalAwayScore = 10;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("10", actualValue);
        }

        [Test]
        public void FinalAwayScore_NoResultDetailData_FinalAwayScoreHasNoValue_ShowHyphen()
        {
            // Arrange
            _sportsbook.FinalAwayScore = null;
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem>());
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void VisibleDetailedLink_HasGameDetailDataAndSettingVisible_IsTrue(string resourceId)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = resourceId, HomeValue = "Home Team" };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            _setting.VisibleDetailedLink = true;
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.VisibleDetailedLink;

            // Assert
            Assert.IsTrue(actualValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void VisibleDetailedLink_SettingIsNotVisible_IsFalse(string resourceId)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = resourceId, HomeValue = "Home Team" };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            _setting.VisibleDetailedLink = false;
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.VisibleDetailedLink;

            // Assert
            Assert.IsFalse(actualValue);
        }

        [Test]
        public void VisibleDetailedLink_HasNoGameDetailData_IsFalse()
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "XXX", HomeValue = "Home Team" };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            _setting.VisibleDetailedLink = true;
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.VisibleDetailedLink;

            // Assert
            Assert.IsFalse(actualValue);
        }

        [Test, TestCaseSource("MoreResultResourceIdsTestCase")]
        public void VisibleGameDetail_HasGameDetailData_IsTrue(string resourceId)
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = resourceId, HomeValue = "Home Team" };
            _resultDetail.GetDetailItems().Returns(new SafeList<BasketBallGameDetailItem> { detailItem });
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.VisibleGameDetail;

            // Assert
            Assert.IsTrue(actualValue);
        }

        [Test]
        public void ResultDetailItems_Always_GetFromResultDetail()
        {
            // Arrange
            var detailItem = new BasketBallGameDetailItem { ResourceID = "604", HomeValue = "Home Team" };
            var detailItems = new SafeList<BasketBallGameDetailItem> { detailItem };
            _resultDetail.GetDetailItems().Returns(detailItems);
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);

            // Act
            var actualValue = viewModel.ResultDetailItems;

            // Assert
            Assert.AreEqual(actualValue, detailItems);
        }

        [Test]
        public void MoreResultDetailItems_Always_GetFromResultDetailItemsWithOrder()
        {
            // Arrange
            var detailItems = new SafeList<BasketBallGameDetailItem>
            {
                new BasketBallGameDetailItem { ResourceID = "6130415", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "604", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60735", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60720", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "61704", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60635", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60620", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "6130115", HomeValue = "Home Team" }
            };

            _resultDetail.GetDetailItems().Returns(detailItems);
            var viewModel = new BasketBallViewModel(_sportsbook, _setting, _resultDetail);
            var compareLogic = new CompareLogic();

            // Act
            var actualValue = viewModel.MoreResultDetailItems;

            // Assert

            var expectedValue = new SafeList<BasketBallGameDetailItem>
            {
                new BasketBallGameDetailItem { ResourceID = "6130115", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60620", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60635", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "6130415", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "61704", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60720", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "60735", HomeValue = "Home Team" },
                new BasketBallGameDetailItem { ResourceID = "604", HomeValue = "Home Team" },
            };
            Assert.True(compareLogic.Compare(actualValue, expectedValue).AreEqual);
        }
    }
}