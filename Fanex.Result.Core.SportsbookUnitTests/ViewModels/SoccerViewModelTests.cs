namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.DataDomain;
    using DTO;
    using Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels;
    using TestUtils;

    [TestFixture]
    public class SoccerViewModelTests
    {
        private ResultSetting _setting;
        private Sportsbook _soccer;
        private ISportsbookRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _setting = new ResultSetting();
            _soccer = new Sportsbook();
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void IsShowLiveScore_IsParlayPopupOrIsNotPopup_ReturnFalse(bool isParlayPopup, bool isPopUp)
        {
            // Arrange
            _setting.IsParlayPopUp = isParlayPopup;
            _setting.IsPopUp = isPopUp;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowLiveScore);
        }

        [TestCase(10, false, true)]
        [TestCase(15, false, true)]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIsNot3And8_ReturnFalse(int betType, bool isParlayPopup, bool isPopUp)
        {
            // Arrange
            _soccer.Bettype = betType;
            _setting.IsParlayPopUp = isParlayPopup;
            _setting.IsPopUp = isPopUp;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowLiveScore);
        }

        [Test]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIs3_FinalHomeScoreHasNoValue_ReturnTrue()
        {
            // Arrange
            _soccer.Bettype = 3;
            _soccer.FinalHomeScore = null;
            _setting.IsParlayPopUp = false;
            _setting.IsPopUp = true;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowLiveScore);
        }

        [Test]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIs3_FinalHomeScoreHasValue_ReturnFalse()
        {
            // Arrange
            _soccer.Bettype = 3;
            _soccer.FinalHomeScore = 1;
            _setting.IsParlayPopUp = false;
            _setting.IsPopUp = true;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowLiveScore);
        }

        [Test]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIs8_HalfTimeHomeScoreHasNoValue_ReturnTrue()
        {
            // Arrange
            _soccer.Bettype = 8;
            _soccer.HtHomeScore = null;
            _setting.IsParlayPopUp = false;
            _setting.IsPopUp = true;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowLiveScore);
        }

        [Test]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIs8_HalfTimeHomeScoreHasValue_ReturnFalse()
        {
            // Arrange
            _soccer.Bettype = 8;
            _soccer.HtHomeScore = 1;
            _setting.IsParlayPopUp = false;
            _setting.IsPopUp = true;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowLiveScore);
        }

        [Test]
        public void PenaltyMatchDetail_ExistHandicapMatchCode_ReturnHandicapName()
        {
            // Arrange
            _soccer.MatchCode = "1 (h)";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual("(HDP)", viewModel.PenaltyMatchDetail);
        }

        [Test]
        public void PenaltyMatchDetail_ExistOverUnderMatchCode_ReturnOverUnderName()
        {
            // Arrange
            _soccer.MatchCode = "1 (o)";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual("(O/U)", viewModel.PenaltyMatchDetail);
        }

        [Test]
        public void PenaltyMatchDetail_NotExistHandicapOrOverUnderCode_ReturnEmpty()
        {
            // Arrange
            _soccer.MatchCode = "1 (12312o)";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.PenaltyMatchDetail);
        }

        [Test]
        public void HomeId_Always_ReturnDataHomeId()
        {
            // Arrange
            _soccer.HomeId = 1;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.HomeId);
        }

        [Test]
        public void AwayId_Always_ReturnDataAwayId()
        {
            // Arrange
            _soccer.AwayId = 1;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.AwayId);
        }

        [Test]
        public void VisibleDetailedLink_SettingInvisibleDetailedLink_ReturnFalse()
        {
            // Arrange
            _setting.VisibleDetailedLink = false;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void VisibleDetailedLink_HasNoValidGameData_And_InvisibleGoalRecord_ReturnFalse()
        {
            // Arrange
            _setting.VisibleDetailedLink = true;
            _setting.VisibleGoalRecord = false;

            _soccer.GoalRecord = "A;H";
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "34234", HomeValue = "1" }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void VisibleDetailedLink_HasValidGameData_Or_VisibleGoalRecord_ReturnTrue()
        {
            // Arrange
            _setting.VisibleDetailedLink = true;
            _setting.VisibleGoalRecord = true;

            _soccer.GoalRecord = "A;H";
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "192", HomeValue = "1" }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void VisibleGameDetail_HasInvalidGameData_ReturnFalse()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "324234", HomeValue = "1" }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.VisibleGameDetail);
        }

        [Test]
        public void VisibleGameDetail_HasValidGameData_ReturnFalse()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "192", HomeValue = "1" }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.VisibleGameDetail);
        }

        [Test]
        public void IsShowFGHome_BetStatusIsNull_ReturnFalse()
        {
            // Arrange // 1FL:1-1;FL:1-1;
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowFGHome);
        }

        [Test]
        public void IsShowFGHome_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "FL:1-1;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowFGHome);
        }

        [Test]
        public void IsShowFGAway_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowFGAway);
        }

        [Test]
        public void IsShowFGAway_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "FL:2-1;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowFGAway);
        }

        [Test]
        public void IsShowLGHome_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowLGHome);
        }

        [Test]
        public void IsShowLGHome_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "FL:2-1;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowLGHome);
        }

        [Test]
        public void IsShowLGAway_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowLGAway);
        }

        [Test]
        public void IsShowLGAway_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "FL:2-2;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowLGAway);
        }

        [Test]
        public void IsShowedFirstHalfFGHome_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowedFirstHalfFGHome);
        }

        [Test]
        public void IsShowedFirstHalfFGHome_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "1FL:1-1;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowedFirstHalfFGHome);
        }

        [Test]
        public void IsShowedFirstHalfFGAway_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowedFirstHalfFGAway);
        }

        [Test]
        public void IsShowedFirstHalfFGAway_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "1FL:2-1;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowedFirstHalfFGAway);
        }

        [Test]
        public void IsShowedFirstHalfLGHome_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowedFirstHalfLGHome);
        }

        [Test]
        public void IsShowedFirstHalfLGHome_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "1FL:2-1;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowedFirstHalfLGHome);
        }

        [Test]
        public void IsShowedFirstHalfLGAway_BetStatusIsNull_ReturnFalse()
        {
            // Arrange
            _soccer.BetStatus = null;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.False(viewModel.IsShowedFirstHalfLGAway);
        }

        [Test]
        public void IsShowedFirstHalfLGAway_BetStatusHasCorrectValue_ReturnTrue()
        {
            // Arrange
            _soccer.BetStatus = "1FL:2-2;";

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.True(viewModel.IsShowedFirstHalfLGAway);
        }

        [TestCase("Refund")]
        [TestCase("Refund HT")]
        public void HTHomeScore_EventStatusIsRefund_ReturnRefund(string eventStatus)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual("Refund", viewModel.HTHomeScore);
        }

        [TestCase(false, "Completed", false, "1")]
        [TestCase(true, "Abandoned 1H", false, "1")]
        [TestCase(true, "Abandoned 2H", false, "1")]
        [TestCase(true, "Refund FT", false, "1")]
        [TestCase(true, "Completed", true, "1")]
        [TestCase(true, "Completed", false, "-")]
        public void HTHomeScore_EnableCheckCalculatedScore_And_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            bool enableCheckCalculatedScore, string eventStatus, bool isHTCal, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
            _setting.EnableCheckScoreCalculated = enableCheckCalculatedScore;
            _soccer.IsHTCal = isHTCal;
            _soccer.HtHomeScore = 1;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.HTHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTHomeScore_EventStatusIsNotRefund_WithScore_ReturnExpectedResult(int? score, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = "Abandoned 1H";
            _soccer.HtHomeScore = score;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.HTHomeScore);
        }

        [TestCase("Refund")]
        [TestCase("Refund HT")]
        public void HTAwayScore_EventStatusIsRefund_ReturnRefund(string eventStatus)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual("Refund", viewModel.HTAwayScore);
        }

        [TestCase(false, "Completed", false, "1")]
        [TestCase(true, "Abandoned 1H", false, "1")]
        [TestCase(true, "Abandoned 2H", false, "1")]
        [TestCase(true, "Refund FT", false, "1")]
        [TestCase(true, "Completed", true, "1")]
        [TestCase(true, "Completed", false, "-")]
        public void HTAwayScore_EnableCheckCalculatedScore_And_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            bool enableCheckCalculatedScore, string eventStatus, bool isHTCal, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
            _setting.EnableCheckScoreCalculated = enableCheckCalculatedScore;
            _soccer.IsHTCal = isHTCal;
            _soccer.HtAwayScore = 1;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.HTAwayScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTAwayScore_EventStatusIsNotRefund_WithScore_ReturnExpectedResult(int? score, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = "Abandoned 1H";
            _soccer.HtAwayScore = score;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.HTAwayScore);
        }

        [TestCase("Refund", "Refund")]
        [TestCase("Refund FT", "Refund")]
        [TestCase("Abandoned 1H", "-")]
        public void FinalHomeScore_EventStatusIsRefundOrAbandoned_ReturnExpectedResult(string eventStatus, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.FinalHomeScore);
        }

        [TestCase(false, "Completed", false, "1")]
        [TestCase(true, "Abandoned 2H", false, "1")]
        [TestCase(true, "Completed", true, "1")]
        [TestCase(true, "Completed", false, "-")]
        public void FinalHomeScore_EnableCheckCalculatedScore_And_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            bool enableCheckCalculatedScore, string eventStatus, bool isFinalCalculate, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
            _setting.EnableCheckScoreCalculated = enableCheckCalculatedScore;
            _soccer.Calculate = isFinalCalculate;
            _soccer.FinalHomeScore = 1;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.FinalHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalHomeScore_EventStatusIsNotRefund_WithScore_ReturnExpectedResult(int? score, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = "Completed";
            _soccer.FinalHomeScore = score;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.FinalHomeScore);
        }

        [TestCase("Refund", "Refund")]
        [TestCase("Refund FT", "Refund")]
        [TestCase("Abandoned 1H", "-")]
        public void FinalAwayScore_EventStatusIsRefundOrAbandoned_ReturnExpectedResult(string eventStatus, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.FinalAwayScore);
        }

        [TestCase(false, "Completed", false, "1")]
        [TestCase(true, "Abandoned 2H", false, "1")]
        [TestCase(true, "Completed", true, "1")]
        [TestCase(true, "Completed", false, "-")]
        public void FinalAwayScore_EnableCheckCalculatedScore_And_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            bool enableCheckCalculatedScore, string eventStatus, bool isFinalCalculate, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
            _setting.EnableCheckScoreCalculated = enableCheckCalculatedScore;
            _soccer.Calculate = isFinalCalculate;
            _soccer.FinalAwayScore = 1;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.FinalAwayScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalAwayScore_EventStatusIsNotRefund_WithScore_ReturnExpectedResult(int? score, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = "Completed";
            _soccer.FinalAwayScore = score;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.FinalAwayScore);
        }

        [TestCase(null, "-")]
        [TestCase(1, "1")]
        public void LiveHomeScore_WithValue_ReturnExpectedResult(int? liveHomeScore, string expectedResult)
        {
            // Arrange
            _soccer.LiveHomeScore = liveHomeScore;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.LiveHomeScore);
        }

        [TestCase(null, "-")]
        [TestCase(1, "1")]
        public void LiveAwayScore_WithValue_ReturnExpectedResult(int? liveAwayScore, string expectedResult)
        {
            // Arrange
            _soccer.LiveAwayScore = liveAwayScore;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.LiveAwayScore);
        }

        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("HA;H", "HAH")]
        public void GoalRecord_WithValue_ReturnExpectedResult(string goalRecord, string expectedResult)
        {
            // Arrange
            _soccer.GoalRecord = goalRecord;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.GoalRecord);
        }

        [Test]
        public void DetailedResult_WithinBetRadarDetailedResult_HasInvalidDetailedResult_ReturnEmpty()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "324234", HomeValue = "1" }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual("", viewModel.DetailedResult);
        }

        [TestCase(EventStatusID.RunningSettlementStatusID, "AH;H", "")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "12312", "")]
        public void DetailedResult_ResourceIsGoalSequence_IsNotShowGoalSequence_ReturnExpectedResult(int eventStatusId, string goalSequence, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "08", HomeValue = goalSequence, SettlementStatusID = eventStatusId }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            var expectedHtml = string.IsNullOrEmpty(expectedResult) ?
                expectedResult :
                TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_GoalSequence.html").Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase(EventStatusID.CompletedSettlementStatusID, "AH;H", "A.H.H")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "AH;H", "A.H.H")]
        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, "AH;H", "A.H.H")]
        [TestCase(EventStatusID.RefundSettlementStatusID, "AH;H", "Refund")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "AH;H", "RefundHT")]
        [TestCase(EventStatusID.RefundFTSettlementStatusID, "AH;H", "RefundFT")]
        [TestCase(342, "AH;H", "-")]
        public void DetailedResult_ResourceIsGoalSequence_IsShowGoalSequence_ReturnExpectedResult(int eventStatusId, string goalSequence, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "08", HomeValue = goalSequence, SettlementStatusID = eventStatusId }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            var expectedHtml = string.IsNullOrEmpty(expectedResult) ?
                expectedResult :
                TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_GoalSequence.html").Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", "HomeTeam")]
        [TestCase("H", "HomeTeam")]
        [TestCase("2", "AwayTeam")]
        [TestCase("A", "AwayTeam")]
        [TestCase("X", "Draw")]
        [TestCase("N", "Draw")]
        [TestCase("323X", "-")]
        [TestCase("", "-")]
        [TestCase(null, "-")]
        public void DetailedResult_ResourceIsFirstTenMinutes1X2_ReturnExpectedResult(string homeValue, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "176", HomeValue = homeValue, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };
            _soccer.HomeId = 1;
            _soccer.AwayId = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FirstTenMinutes1X2.html").Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("s", "Shot")]
        [TestCase("h", "Header")]
        [TestCase("p", "Penalty")]
        [TestCase("fk", "FreeKick")]
        [TestCase("og", "OwnGoal")]
        [TestCase("ng", "NoGoal")]
        [TestCase("n23423g", "")]
        public void DetailedResult_ResourceIsFirstGoalMethod_ReturnExpectedResult(string homeValue, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "424", HomeValue = homeValue, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FirstGoalMethod.html").Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("", "-")]
        [TestCase("None", "None")]
        [TestCase("10", "10Min")]
        public void DetailedResult_ResourceIsFirstGoalTime_ReturnExpectedResult(string homeValue, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "192", HomeValue = homeValue, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "193", HomeValue = homeValue, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FirstGoalTime.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("", "-")]
        [TestCase("Harrison", "Harrison")]
        public void DetailedResult_ResourceIsFirstGoalScorer_ReturnExpectedResult(string otherInfo, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "217", OtherInfo = otherInfo, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FirstGoalScorer.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("", "-")]
        [TestCase("5", "5")]
        public void DetailedResult_ResourceIsFirstHalfAndFinalCorners_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "04", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "05", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FirstHalfAndFinalCorner.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", "HomeTeam")]
        [TestCase("H", "HomeTeam")]
        [TestCase("2", "AwayTeam")]
        [TestCase("A", "AwayTeam")]
        [TestCase("X", "None")]
        [TestCase("N", "None")]
        [TestCase("323X", "-")]
        [TestCase("", "-")]
        [TestCase(null, "-")]
        public void DetailedResult_ResourceIsFirstCorner_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "206", HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "207", HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.HomeId = 1;
            _soccer.AwayId = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FirstCorner.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", EventStatusID.CompletedSettlementStatusID, "HomeTeam")]
        [TestCase("H", EventStatusID.CompletedSettlementStatusID, "HomeTeam")]
        [TestCase("2", EventStatusID.CompletedSettlementStatusID, "AwayTeam")]
        [TestCase("A", EventStatusID.CompletedSettlementStatusID, "AwayTeam")]
        [TestCase("X", EventStatusID.CompletedSettlementStatusID, "None")]
        [TestCase("N", EventStatusID.CompletedSettlementStatusID, "None")]
        [TestCase("323X", EventStatusID.CompletedSettlementStatusID, "-")]
        [TestCase("", EventStatusID.CompletedSettlementStatusID, "-")]
        [TestCase(null, EventStatusID.CompletedSettlementStatusID, "-")]
        [TestCase("1", EventStatusID.Abandoned1HSettlementStatusID, "Refund")]
        [TestCase("1", EventStatusID.Abandoned2HSettlementStatusID, "Refund")]
        public void DetailedResult_ResourceIsLastCorner_ReturnExpectedResult(string value, int eventStatusId, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "208", HomeValue = value, SettlementStatusID = eventStatusId },
                new GameDetail {
                    ResourceId = "209",
                    HomeValue = value,
                    SettlementStatusID = eventStatusId == EventStatusID.Abandoned2HSettlementStatusID ? EventStatusID.Abandoned1HSettlementStatusID : eventStatusId},
            };
            _soccer.HomeId = 1;
            _soccer.AwayId = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_LastCorner.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("Y", "Yes")]
        [TestCase("3123", "No")]
        [TestCase("", "-")]
        public void DetailedResult_ResourceIsPlayerSentOff_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "210", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "211", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "212", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "213", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "214", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "215", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PlayerSentOff.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("Y", "Yes")]
        [TestCase("3123", "No")]
        [TestCase("", "-")]
        public void DetailedResult_ResourceIsExtraTime_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "173", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "174", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "135", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_ExtraTime.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", "HomeTeam")]
        [TestCase("H", "HomeTeam")]
        [TestCase("2", "AwayTeam")]
        [TestCase("A", "AwayTeam")]
        [TestCase("X", "")]
        [TestCase("N", "")]
        [TestCase("323X", "-")]
        [TestCase("", "-")]
        [TestCase(null, "-")]
        public void DetailedResult_ResourceIsWhoAdvancesToNextRound_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "168", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.HomeId = 1;
            _soccer.AwayId = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_WhoAdvancesToNextRound.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("HR", "HomeTeam/RegularTime")]
        [TestCase("HE", "HomeTeam/ExtraTime")]
        [TestCase("HP", "HomeTeam/Penalty")]
        [TestCase("AR", "AwayTeam/RegularTime")]
        [TestCase("AE", "AwayTeam/ExtraTime")]
        [TestCase("AP", "AwayTeam/Penalty")]
        [TestCase("423423", "-")]
        [TestCase(null, "-")]
        public void DetailedResult_ResourceIsMatchDecidedMethod_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "175", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.HomeId = 1;
            _soccer.AwayId = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_MatchDecidedMethod.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-01:59")]
        [TestCase("78", " (       0  (", "78:00-78:59")]
        public void DetailedResult_ResourceIsFastMarkets1Minute_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22101", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "22301", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.BetID = betId;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-01:59")]
        [TestCase("78", " (       0  (", "78:00-78:59")]
        public void DetailedResult_ResourceIsFastMarkets1Minute_VersionTwo_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22101", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "44444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _soccer.BetID = betId;
            _soccer.Handicap2 = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetResourceDataByRefIdAndResourceId("FastMarket", "2", "en", Arg.Any<string>()).Returns("Goal");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets221Two.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-05:59")]
        [TestCase("78", " (       0  (", "78:00-82:59")]
        public void DetailedResult_ResourceIsFastMarkets5Minutes_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22201", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "22401", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.BetID = betId;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-05:59")]
        public void DetailedResult_ResourceIsFastMarkets5Minutes_Two_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22201", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "44444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _soccer.BetID = betId;
            _soccer.Handicap2 = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetResourceDataByRefIdAndResourceId("FastMarket", "2", "en", Arg.Any<string>()).Returns("Goal");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets222Two.html");
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-01:59")]
        [TestCase("78", " (       0  (", "78:00-78:59")]
        public void DetailedResult_ResourceIsFastMarkets225_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22501", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "44444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _soccer.Bettype = 225;
            _soccer.BetID = betId;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en").Returns("Penalty225");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets225.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-01:59")]
        [TestCase("78", " (       0  (", "78:00-78:59")]
        public void DetailedResult_ResourceIsFastMarkets226_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22601", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "22301", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "44444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _soccer.Bettype = 226;
            _soccer.BetID = betId;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (", "01:00-05:59")]
        [TestCase("78", " (       0  (", "78:00-82:59")]
        public void DetailedResult_ResourceIsFastMarkets227s_ReturnExpectedResult(string betId, string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "22701", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "22401", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.BetID = betId;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("", "-")]
        [TestCase("None", "None")]
        [TestCase("50", "50Min")]
        public void DetailedResult_ResourceIsNextGoalTime_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "16901", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_NextGoalTime.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [Test]
        public void DetailedResult_ResourceIsGoalScorer_ReturnExpectedResult()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "21601", OtherInfo = "harrison", SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_GoalScorer.html")
                .Replace("{Result}", "harrison");
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", "HomeTeam")]
        [TestCase("H", "HomeTeam")]
        [TestCase("2", "AwayTeam")]
        [TestCase("A", "AwayTeam")]
        [TestCase("X", "None")]
        [TestCase("N", "None")]
        [TestCase("323X", "-")]
        [TestCase("", "-")]
        [TestCase(null, "-")]
        public void DetailedResult_ResourceIsNextGoal_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "18001", HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "16001", HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
                new GameDetail {ResourceId = "16401", HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };
            _soccer.HomeId = 1;
            _soccer.AwayId = 2;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_NextGoal.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }
    }
}