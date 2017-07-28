namespace Fanex.Result.Core.Lucky3.Tests.ViewModels
{
    using System;
    using NUnit.Framework;
    using DTO;
    using Base;
    using Lucky3.ViewModels;
    using Resources;
    using System.Collections.Generic;
    using Base.Utilities;
    using NSubstitute;
    using Data;

    [TestFixture]
    public class Lucky3ViewModelTests
    {
        private Lucky3 resultData;
        private ResultSetting resultSetting;
        private IRefDataHelper refDataHelper;

        [SetUp]
        public void Setup()
        {
            resultData = DataHelper.CreateLucky3Data();
            resultSetting = DataHelper.CreateResultSetting();
            refDataHelper = DataHelper.CreateRefDataHelper();
        }

        [Test]
        public void KickOffTime_Always_ReturnExpectedKickOffTime()
        {
            // Arrange
            resultData.KickOffTime = Convert.ToDateTime("01/01/2017");
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.KickOffTime;

            // Assert
            Assert.AreEqual("01/01/2017<br/>12:00:00 AM", actualValue);
        }

        [Test]
        public void Match_Always_ReturnExpectedMatchText()
        {
            // Arrange
            resultData.MatchCode = "1xxx";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.Match;

            // Assert
            Assert.AreEqual($"{Lucky3Resources.Lucky3GameNo} 1xxx", actualValue);
        }

        [Test]
        public void FirstBall_Always_ReturnFirstBallValue()
        {
            // Arrange
            resultData.Ball = new List<int> { 1, 2, 3 };
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.FirstBall;

            // Assert
            Assert.AreEqual(1, actualValue);
        }

        [Test]
        public void SecondBall_Always_ReturnSecondBallValue()
        {
            // Arrange
            resultData.Ball = new List<int> { 1, 2, 3 };
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.SecondBall;

            // Assert
            Assert.AreEqual(2, actualValue);
        }

        [Test]
        public void ThirdBall_Always_ReturnThirdBallValue()
        {
            // Arrange
            resultData.Ball = new List<int> { 1, 2, 3 };
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.ThirdBall;

            // Assert
            Assert.AreEqual(3, actualValue);
        }

        [Test]
        public void FirstBallColor_Always_ReturnFirstBallColor()
        {
            // Arrange
            resultData.Ball = new List<int> { 1, 2, 3 };
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.FirstBallColor;

            // Assert
            Assert.AreEqual("red", actualValue);
        }

        [Test]
        public void SecondBallColor_Always_ReturnSecondBallColor()
        {
            // Arrange
            resultData.Ball = new List<int> { 1, 2, 3 };
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.SecondBallColor;

            // Assert
            Assert.AreEqual("blue", actualValue);
        }

        [Test]
        public void ThirdBallColor_Always_ReturnThirdBallColor()
        {
            // Arrange
            resultData.Ball = new List<int> { 1, 2, 3 };
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.ThirdBallColor;

            // Assert
            Assert.AreEqual("red", actualValue);
        }

        [Test]
        public void FirstPrize_EventStatusIsRefund_ReturnHyphen()
        {
            // Arrange
            resultData.LiveHomeScore = 1;
            resultData.EventStatus = "refund";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.FirstPrize;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [Test]
        public void FirstPrize_EventStatusIsNotRefund_ReturnLiveHomeScore()
        {
            // Arrange
            resultData.LiveHomeScore = 1;
            resultData.EventStatus = "completed";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.FirstPrize;

            // Assert
            Assert.AreEqual("1", actualValue);
        }

        [Test]
        public void SecondPrize_EventStatusIsRefund_ReturnHyphen()
        {
            // Arrange
            resultData.LiveHomeScore = 1;
            resultData.EventStatus = "refund";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.SecondPrize;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [Test]
        public void SecondPrize_EventStatusIsNotRefund_ReturnLiveAwayScore()
        {
            // Arrange
            resultData.LiveAwayScore = 1;
            resultData.EventStatus = "completed";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.SecondPrize;

            // Assert
            Assert.AreEqual("1", actualValue);
        }

        [Test]
        public void ThirdPrize_EventStatusIsRefund_ReturnHyphen()
        {
            // Arrange
            resultData.LiveHomeScore = 1;
            resultData.EventStatus = "refund";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.ThirdPrize;

            // Assert
            Assert.AreEqual("-", actualValue);
        }

        [Test]
        public void ThirdPrize_EventStatusIsNotRefund_ReturnLiveAwayScore()
        {
            // Arrange
            resultData.FinalHomeScore = 1;
            resultData.EventStatus = "completed";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.ThirdPrize;

            // Assert
            Assert.AreEqual("1", actualValue);
        }

        [TestCase("running", false, false, "Running")]
        [TestCase("running", true, false, "Pending")]
        [TestCase("internal", true, true, "Internal")]
        [TestCase("internal", false, false, "Running")]
        public void EventStatus_Always_GetEventStatus(string eventStatus, bool isPending, bool visibleInternalEventStatus, string expectedEventStatus)
        {
            // Arrange
            resultData.EventStatus = eventStatus;
            resultData.IsPending = isPending;
            resultSetting.VisibleInternalEventStatus = visibleInternalEventStatus;
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.EventStatus;

            // Assert
            Assert.AreEqual(expectedEventStatus, actualValue);
        }

        [Test]
        public void MatchCode_Always_ReturnMatchCode()
        {
            // Arrange
            resultData.MatchCode = "1xxx";
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.MatchCode;

            // Assert
            Assert.AreEqual("1xxx", actualValue);
        }

        [Test]
        public void MatchId_Always_ReturnMatchId()
        {
            // Arrange
            resultData.MatchId = 11;
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.MatchId;

            // Assert
            Assert.AreEqual("11", actualValue);
        }

        [Test]
        public void LeagueName_Always_ReturnLeagueNameForRefData()
        {
            // Arrange
            var viewModel = new Lucky3ViewModel(resultData, resultSetting, refDataHelper);

            // Act
            var actualValue = viewModel.LeagueName;

            // Assert
            Assert.AreEqual("league name", actualValue);
        }
    }
}