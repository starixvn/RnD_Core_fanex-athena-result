namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;
    using TestUtils;

    [TestFixture]
    public class SoccerViewModelTests
    {
        private ResultSetting _setting;
        private Sportsbook _soccer;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _setting = new ResultSetting();
            _soccer = new Sportsbook();
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(_refDataHelper);
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

        [TestCase(false, false, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIs3_ReturnExpected(bool isHTCal, bool cal, bool expected)
        {
            // Arrange
            _soccer.Bettype = 3;
            _soccer.IsHTCal = isHTCal;
            _soccer.Calculate = cal;
            _setting.IsParlayPopUp = false;
            _setting.IsPopUp = true;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expected, viewModel.IsShowLiveScore);
        }

        [TestCase(false, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, false, false)]
        public void IsShowLiveScore_IsNotParlayPopupAndIsPopup_BetTypeIs8_ReturnExpected(bool isHTCal, bool cal, bool expected)
        {
            // Arrange
            _soccer.Bettype = 8;
            _soccer.IsHTCal = isHTCal;
            _soccer.Calculate = cal;
            _setting.IsParlayPopUp = false;
            _setting.IsPopUp = true;

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            Assert.AreEqual(expected, viewModel.IsShowLiveScore);
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
            // Arrange
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

        [TestCase("Abandoned 1H", false, "1")]
        [TestCase("Abandoned 2H", false, "1")]
        [TestCase("Refund FT", false, "1")]
        [TestCase("Completed", true, "1")]
        [TestCase("Completed", false, "-")]
        public void HTHomeScore_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(string eventStatus, bool isHTCal, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
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

        [TestCase("Abandoned 1H", false, "1")]
        [TestCase("Abandoned 2H", false, "1")]
        [TestCase("Refund FT", false, "1")]
        [TestCase("Completed", true, "1")]
        [TestCase("Completed", false, "-")]
        public void HTAwayScore_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            string eventStatus, bool isHTCal, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
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

        [TestCase("Abandoned 2H", false, "1")]
        [TestCase("Completed", true, "1")]
        [TestCase("Completed", false, "-")]
        public void FinalHomeScore_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            string eventStatus, bool isFinalCalculate, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
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
            _soccer.IsHTCal = true;
            _soccer.Calculate = true;
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

        [TestCase("Abandoned 2H", false, "1")]
        [TestCase("Completed", true, "1")]
        [TestCase("Completed", false, "-")]
        public void FinalAwayScore_EventStatusIsNotAbandonedAndRefundFT_ReturnHyphen(
            string eventStatus, bool isFinalCalculate, string expectedResult)
        {
            // Arrange
            _soccer.EventStatus = eventStatus;
            _soccer.Calculate = isFinalCalculate;
            _soccer.FinalAwayScore = 1;

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

        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, "", "", "FT Sequence")]
        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, ";", "", "FT Sequence")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "", "", "FT Sequence")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, ";", "", "FT Sequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "", "", "FT Sequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, ";", "", "FT Sequence")]
        [TestCase(EventStatusID.RunningSettlementStatusID, "AH;H", "", "FT Sequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "12312", "", "FT Sequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "AH;H", "A.H.H", "FT Sequence")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "AH;H", "A.H.H", "FT Sequence")]
        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, "AH;H", "A.H.H", "FT Sequence")]
        [TestCase(EventStatusID.RefundSettlementStatusID, "AH;H", "Refund", "FT Sequence")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "AH;H", "RefundHT", "FT Sequence")]
        [TestCase(EventStatusID.RefundFTSettlementStatusID, "AH;H", "RefundFT", "FT Sequence")]
        [TestCase(342, "AH;H", "-", "FT Sequence")]
        public void DetailedResult_ResourceIsGoalSequence_IsShowGoalSequence_ReturnExpectedResult(int eventStatusId, string goalSequence, string expectedResult, string expectedName)
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
                TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_GoalSequence.html")
                .Replace("{Result}", expectedResult)
                .Replace("{Name}", expectedName);
            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, "", "", "1HSequence")]
        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, ";", "", "1HSequence")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "", "", "1HSequence")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, ";", "", "1HSequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "", "", "1HSequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, ";", "", "1HSequence")]
        [TestCase(EventStatusID.RunningSettlementStatusID, "AH;H", "", "1HSequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "12312", "", "1HSequence")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "AH;H", "A.H.H", "1HSequence")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "AH;H", "A.H.H", "1HSequence")]
        [TestCase(EventStatusID.Abandoned2HSettlementStatusID, "AH;H", "A.H.H", "1HSequence")]
        [TestCase(EventStatusID.RefundSettlementStatusID, "AH;H", "Refund", "1HSequence")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "AH;H", "RefundHT", "1HSequence")]
        [TestCase(EventStatusID.RefundFTSettlementStatusID, "AH;H", "RefundFT", "1HSequence")]
        [TestCase(342, "AH;H", "-", "1HSequence")]
        public void DetailedResult_ResourceIsSequence_IsShowGoalSequence_ReturnExpectedResult(int eventStatusId, string goalSequence, string expectedResult, string expectedName)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "09", HomeValue = goalSequence, SettlementStatusID = eventStatusId }
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting);

            // Assert
            var expectedHtml = string.IsNullOrEmpty(expectedResult) ?
                expectedResult :
                TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_GoalSequence.html")
                .Replace("{Result}", expectedResult)
                .Replace("{Name}", expectedName);
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
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("AwayTeam");

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
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("AwayTeam");

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
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("AwayTeam");

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
        public void DetailedResult_ResourceIsSpecial_ReturnExpectedResult(string value, string expectedResult)
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
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_Special.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("S", "Shot", "1")]
        [TestCase("H", "Header", "2")]
        [TestCase("P", "Penalty", "3")]
        [TestCase("FK", "FreeKick", "4")]
        [TestCase("OG", "OwnGoal", "5")]
        [TestCase("NG", "NoGoal", "6")]
        public void DetailedResult_ResourceIsExtraTime_ReturnExpectedResult(string value, string expectedResult, string expectedRound)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3950" + expectedRound, HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_ExtraTime.html")
                .Replace("{Result}", expectedResult)
                .Replace("{goal}", expectedRound);
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
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("AwayTeam");

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
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("AwayTeam");

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
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
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
            _refDataHelper.GetResourceDataByRefIdAndResourceId("FastMarket", "2", "en-US", Arg.Any<string>()).Returns("Goal");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
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
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
            var fastMarketResultDetail = new FastMarketResultDetail(_soccer, _setting, _refDataHelper, null);

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, fastMarketResultDetail, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_FastMarkets.html")
                .Replace("{Time}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", " (       0  (")]
        public void DetailedResult_ResourceIsFastMarkets5Minutes_Two_ReturnExpectedResult(string betId, string value)
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
            _refDataHelper.GetResourceDataByRefIdAndResourceId("FastMarket", "2", "en-US", Arg.Any<string>()).Returns("Goal");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
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
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en-US").Returns("Penalty225");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
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
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
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
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Penalty");
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("BetType");
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
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("HomeTeam");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_NextGoal.html")
                .Replace("{Result}", expectedResult);
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("12", "Time Of First Corner", "12 min 12 sec")]
        [TestCase("2", "Time Of First Corner", "02 min 02 sec")]
        [TestCase("0", "Time Of First Corner", "No Corner")]
        [TestCase("00", "Time Of First Corner", "No Corner")]
        public void DetailedResult_ResourceIsTimeOfFirstCorner_ReturnExpectedResult(string value, string expectedName, string expectedValue)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "479", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_Corners.html")
                .Replace("{Name}", expectedName)
                .Replace("{Value}", expectedValue);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("12", "Time Of 2H First Corner", "12 min 12 sec")]
        [TestCase("2", "Time Of 2H First Corner", "02 min 02 sec")]
        [TestCase("0", "Time Of 2H First Corner", "No Corner")]
        [TestCase("00", "Time Of 2H First Corner", "No Corner")]
        public void DetailedResult_ResourceIsTimeOf2HFirstCorner_ReturnExpectedResult(string value, string expectedName, string expectedValue)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "481", HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_Corners.html")
                .Replace("{Name}", expectedName)
                .Replace("{Value}", expectedValue);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("S", "1", "Goal", "Goal", 4)]
        [TestCase("M", "1", "Miss", "Miss", 4)]
        [TestCase("NT", "1", "NotTaken", "NotTaken", 1)]
        [TestCase("S", "2", "Goal", "Goal", 4)]
        [TestCase("M", "2", "Miss", "Miss", 4)]
        [TestCase("NT", "2", "NotTaken", "NotTaken", 1)]
        [TestCase("S", "3", "Goal", "Goal", 4)]
        [TestCase("M", "3", "Miss", "Miss", 4)]
        [TestCase("NT", "3", "NotTaken", "NotTaken", 1)]
        [TestCase("S", "4", "Goal", "Goal", 4)]
        [TestCase("M", "4", "Miss", "Miss", 4)]
        [TestCase("NT", "4", "NotTaken", "NotTaken", 1)]
        [TestCase("S", "5", "Goal", "Goal", 4)]
        [TestCase("M", "5", "Miss", "Miss", 4)]
        [TestCase("NT", "5", "NotTaken", "NotTaken", 1)]
        public void DetailedResult_ResourceIsScoreMiss_ReturnExpectedResult(string value, string expectedRound, string expectedHome, string expectedAway, int settlementId)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3820" + expectedRound, HomeValue = value, SettlementStatusID = settlementId},
                new GameDetail {ResourceId = "3810" + expectedRound, HomeValue = value, SettlementStatusID = settlementId},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutScoreMiss.html")
                .Replace("{home}", expectedHome)
                .Replace("{away}", expectedAway)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1", "1", "Home")]
        [TestCase("X", "1", "Draw")]
        [TestCase("2", "1", "Away")]
        [TestCase("1", "2", "Home")]
        [TestCase("X", "2", "Draw")]
        [TestCase("2", "2", "Away")]
        [TestCase("1", "3", "Home")]
        [TestCase("X", "3", "Draw")]
        [TestCase("2", "3", "Away")]
        [TestCase("1", "4", "Home")]
        [TestCase("X", "4", "Draw")]
        [TestCase("2", "4", "Away")]
        [TestCase("1", "5", "Home")]
        [TestCase("X", "5", "Draw")]
        [TestCase("2", "5", "Away")]
        public void DetailedResult_ResourceIsPenaltyShootout1X2_Completed_ReturnExpectedResult(string value, string expectedRound, string expectedHome)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3850" + expectedRound, HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootout1X2_Completed.html")
                .Replace("{result}", expectedHome)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1SNT", "1", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "1", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "1", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "2", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "2", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "2", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "3", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "3", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "3", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "4", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "4", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "4", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "5", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "5", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "5", "Home Not Taken - Away Goal")]
        public void DetailedResult_ResourceIsPenaltyShootout1X2_RefundWithSettled_ReturnExpectedResult(string value, string expectedRound, string expectedHome)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3850" + expectedRound, HomeValue = value, SettlementStatusID = EventStatusID.RefundWithSettleDetermined}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootout1X2_RefundWithSettleDetermined.html")
                .Replace("{result}", expectedHome)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("B", "1", "Both")]
        [TestCase("O", "1", "One")]
        [TestCase("N", "1", "No Goal")]
        [TestCase("B", "2", "Both")]
        [TestCase("O", "2", "One")]
        [TestCase("N", "2", "No Goal")]
        [TestCase("B", "3", "Both")]
        [TestCase("O", "3", "One")]
        [TestCase("N", "3", "No Goal")]
        [TestCase("B", "4", "Both")]
        [TestCase("O", "4", "One")]
        [TestCase("N", "4", "No Goal")]
        [TestCase("B", "5", "Both")]
        [TestCase("O", "5", "One")]
        [TestCase("N", "5", "No Goal")]
        public void DetailedResult_ResourceIsPenaltyShootoutBothOneNeither_Completed_ReturnExpectedResult(string value, string expectedRound, string expectedHome)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3840" + expectedRound, HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutBothOneNeither_Completed.html")
                .Replace("{result}", expectedHome)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("1SNT", "1", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "1", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "1", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "1", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "2", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "2", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "2", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "2", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "3", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "3", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "3", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "3", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "4", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "4", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "4", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "4", "Home Not Taken - Away Miss")]
        [TestCase("1SNT", "5", "Home Goal - Away Not Taken")]
        [TestCase("1MNT", "5", "Home Miss - Away Not Taken")]
        [TestCase("NT2S", "5", "Home Not Taken - Away Goal")]
        [TestCase("NT2M", "5", "Home Not Taken - Away Miss")]
        public void DetailedResult_ResourceIsPenaltyShootoutBothOneNeither_RefundWithSettled_ReturnExpectedResult(string value, string expectedRound, string expectedHome)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3840" + expectedRound, HomeValue = value, SettlementStatusID = EventStatusID.RefundWithSettleDetermined}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutBothOneNeither_RefundWithSettleDetermined.html")
                .Replace("{result}", expectedHome)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("H", "1", "Hit")]
        [TestCase("N", "1", "Not Hit")]
        public void DetailedResult_ResourceIsPenaltyShootoutWoodwork_Completed_ReturnExpectedResult(string value, string expectedRound, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3830" + expectedRound, HomeValue = value, AwayValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutWoodwork_Completed.html")
                .Replace("{result}", expectedResult)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("H", "NT", "1", "Home Hit - Away Not Taken")]
        [TestCase("N", "NT", "1", "Home Not Hit - Away Not Taken")]
        [TestCase("NT", "H", "1", "Home Not Taken - Away Hit")]
        [TestCase("NT", "N", "1", "Home Not Taken - Away Not Hit")]
        [TestCase("H", "NT", "2", "Home Hit - Away Not Taken")]
        [TestCase("N", "NT", "2", "Home Not Hit - Away Not Taken")]
        [TestCase("NT", "H", "2", "Home Not Taken - Away Hit")]
        [TestCase("NT", "N", "2", "Home Not Taken - Away Not Hit")]
        [TestCase("H", "NT", "3", "Home Hit - Away Not Taken")]
        [TestCase("N", "NT", "3", "Home Not Hit - Away Not Taken")]
        [TestCase("NT", "H", "3", "Home Not Taken - Away Hit")]
        [TestCase("NT", "N", "3", "Home Not Taken - Away Not Hit")]
        [TestCase("H", "NT", "4", "Home Hit - Away Not Taken")]
        [TestCase("N", "NT", "4", "Home Not Hit - Away Not Taken")]
        [TestCase("NT", "H", "4", "Home Not Taken - Away Hit")]
        [TestCase("NT", "N", "4", "Home Not Taken - Away Not Hit")]
        [TestCase("H", "NT", "5", "Home Hit - Away Not Taken")]
        [TestCase("N", "NT", "5", "Home Not Hit - Away Not Taken")]
        [TestCase("NT", "H", "5", "Home Not Taken - Away Hit")]
        [TestCase("NT", "N", "5", "Home Not Taken - Away Not Hit")]
        public void DetailedResult_ResourceIsPenaltyShootoutWoodwork_RefundWithSettled_ReturnExpectedResult(string home, string away, string expectedRound, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3830" + expectedRound, HomeValue = home, AwayValue = away, SettlementStatusID = EventStatusID.RefundWithSettleDetermined}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutWoodwork_RefundWithSettled.html")
                .Replace("{result}", expectedResult)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [Test]
        public void DetailedResult_ResourceIsPenaltyShootoutOddEvenFirst10_ReturnExpectedResult()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "393", HomeValue = "1", AwayValue = "4", SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutOddEvenFirst10.html");

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase(4, "1")]
        [TestCase(7, "1<div>(Unconditionally Determined)</div>")]
        public void DetailedResult_ResourceIsHomePenaltyShootoutOverUnderFirst10_ReturnExpectedResult(int eventStatusId, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "387", HomeValue = "1", AwayValue = "4", SettlementStatusID = eventStatusId}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_HomePenaltyShootoutOverUnderFirst10.html")
                .Replace("{result}", expectedResult);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase(4, "1")]
        [TestCase(7, "1<div>(Unconditionally Determined)</div>")]
        public void DetailedResult_ResourceIsAwayPenaltyShootoutOverUnderFirst10_ReturnExpectedResult(int eventStatusId, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "386", HomeValue = "1", AwayValue = "4", SettlementStatusID = eventStatusId}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_AwayPenaltyShootoutOverUnderFirst10.html")
                .Replace("{result}", expectedResult);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [Test]
        public void DetailedResult_ResourceIsPenaltyShootoutExactTotalGoalFirst10_Completed_ReturnExpectedResult()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "391", HomeValue = "1", AwayValue = "4", SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutExactTotalGoalFirst10_Completed.html");

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [Test]
        public void DetailedResult_ResourceIsPenaltyShootoutExactTotalGoalFirst10_RefundWithSettled_ReturnExpectedResult()
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "391", HomeValue = "1", AwayValue = "4", SettlementStatusID = EventStatusID.RefundWithSettleDetermined}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutExactTotalGoalFirst10_RefundWithSettleDetermined.html");

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("Y", "Yes")]
        [TestCase("N", "No")]
        public void DetailedResult_ResourceIsPenaltyShootoutSuddenDeath_ReturnExpectedResult(string value, string expectedResult)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "390", HomeValue = value, SettlementStatusID = EventStatusID.CompletedSettlementStatusID}
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutSuddenDeath.html")
                .Replace("{result}", expectedResult);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("S", "6", "Goal", "Goal", 4)]
        [TestCase("M", "6", "Miss", "Miss", 4)]
        [TestCase("NT", "6", "NotTaken", "NotTaken", 1)]
        [TestCase("S", "7", "Goal", "Goal", 4)]
        [TestCase("M", "7", "Miss", "Miss", 4)]
        [TestCase("NT", "7", "NotTaken", "NotTaken", 1)]
        public void DetailedResult_ResourceIsScoreMissInSuddenDeathGroup_ReturnExpectedResult(string value, string expectedRound, string expectedHome, string expectedAway, int settlementId)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "3820" + expectedRound, HomeValue = value, SettlementStatusID = settlementId},
                new GameDetail {ResourceId = "3810" + expectedRound, HomeValue = value, SettlementStatusID = settlementId},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutScoreMissSuddenDeathGroup.html")
                .Replace("{home}", expectedHome)
                .Replace("{away}", expectedAway)
                .Replace("{round}", expectedRound);

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }

        [TestCase("S", "Goal", "Goal", 4)]
        [TestCase("M", "Miss", "Miss", 4)]
        [TestCase("NT", "NotTaken", "NotTaken", 1)]
        [TestCase("S", "Goal", "Goal", 4)]
        [TestCase("M", "Miss", "Miss", 4)]
        [TestCase("NT", "NotTaken", "NotTaken", 1)]
        public void DetailedResult_ResourceIsScoreMissInSuddenDeathGroupOnRound99_ReturnExpectedResult(string value, string expectedHome, string expectedAway, int settlementId)
        {
            // Arrange
            _soccer.GameDetails = new List<GameDetail>
            {
                new GameDetail {ResourceId = "38299", HomeValue = value, SettlementStatusID = settlementId},
                new GameDetail {ResourceId = "38199", HomeValue = value, SettlementStatusID = settlementId},
            };

            // Act
            var viewModel = new SoccerViewModel(_soccer, _setting, null, null, _refDataHelper);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail_PenaltyShootoutScoreMissSuddenDeathGroup.html")
                .Replace("{home}", expectedHome)
                .Replace("{away}", expectedAway)
                .Replace("{round}", "99");

            Assert.AreEqual(TemplateFileUtils.RemoveEndLineSymbolHtml(expectedHtml), TemplateFileUtils.RemoveEndLineSymbolHtml(viewModel.DetailedResult));
        }
    }
}