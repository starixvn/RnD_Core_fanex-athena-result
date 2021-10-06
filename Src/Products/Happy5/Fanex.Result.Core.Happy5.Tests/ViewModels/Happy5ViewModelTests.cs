namespace Fanex.Result.Core.Happy5.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Happy5.Resources;
    using Fanex.Result.Core.Happy5.ViewModels;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Happy5ViewModelTests
    {
        private ResultSetting subResultSetting;
        private IRefDataHelper subRefDataHelper;

        [SetUp]
        public void SetUp()
        {
            subResultSetting = Substitute.For<ResultSetting>();
            subResultSetting.LanguageCode = "en";
            subRefDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void KickOffTime_Always_GetFormatedKickOffTime()
        {
            // Arrange
            var resultData = new DTO.Happy5 { KickOffTime = new DateTime(2018, 1, 1) };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualKickOffTime = viewModel.KickOffTime;

            // Assert
            Assert.AreEqual("01/01/2018<br/>12:00:00 AM", actualKickOffTime);
        }

        [Test]
        public void LeagueId_Always_GetLeagueIdFromData()
        {
            // Arrange
            var resultData = new DTO.Happy5 { LeagueId = 1011 };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualLeagueId = viewModel.LeagueId;

            // Assert
            Assert.AreEqual(1011, actualLeagueId);
        }

        [Test]
        public void Match_Always_GetExpectedValueFromMatchCode()
        {
            // Arrange
            var resultData = new DTO.Happy5 { MatchCode = "1000" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualMatch = viewModel.Match;

            // Assert
            Assert.AreEqual("No. 1000", actualMatch);
        }

        [Test]
        public void FirstBall_Always_GetFirstBallValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FirstBall;

            // Assert
            Assert.AreEqual(1, actualValue);
        }

        [Test]
        public void SecondBall_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.SecondBall;

            // Assert
            Assert.AreEqual(2, actualValue);
        }

        [Test]
        public void ThirdBall_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.ThirdBall;

            // Assert
            Assert.AreEqual(3, actualValue);
        }

        [Test]
        public void FourthBall_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FourthBall;

            // Assert
            Assert.AreEqual(4, actualValue);
        }

        [Test]
        public void FifthBall_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FifthBall;

            // Assert
            Assert.AreEqual(5, actualValue);
        }

        [Test]
        public void FirstBallColor_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FirstBallColor;

            // Assert
            Assert.AreEqual("red", actualValue);
        }

        [Test]
        public void SecondBallColor_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.SecondBallColor;

            // Assert
            Assert.AreEqual("blue", actualValue);
        }

        [Test]
        public void ThirdBallColor_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.ThirdBallColor;

            // Assert
            Assert.AreEqual("red", actualValue);
        }

        [Test]
        public void FourthBallColor_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FourthBallColor;

            // Assert
            Assert.AreEqual("red", actualValue);
        }

        [Test]
        public void FifthBallColor_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FifthBallColor;

            // Assert
            Assert.AreEqual("blue", actualValue);
        }

        [Test]
        public void TotalBall_EventStatusIsRefund_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.TotalBall;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void TotalBall_IsMatchPending_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.TotalBall;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void TotalBall_IsNormalMatch_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.TotalBall;

            // Assert
            Assert.AreEqual("15", actualValue);
        }

        [Test]
        public void BigSmall_EventStatusIsRefund_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.BigSmall;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void BigSmall_IsPendingMatch_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.BigSmall;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [TestCase(4, 8, 10, 11, 19, "S")]
        [TestCase(5, 8, 10, 11, 19, "B")]
        public void BigSmall_IsNormalMatch_GetExpectedValue(int first, int second, int third, int fourth, int fifth, string expected)
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { first, second, third, fourth, fifth } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.BigSmall;

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [Test]
        public void OddEven_EventStatusIsRefund_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.OddEven;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void OddEven_IsPendingMatch_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.OddEven;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [TestCase(4, 8, 10, 11, 19, "E")]
        [TestCase(5, 8, 10, 11, 19, "O")]
        public void OddEven_IsNormalMatch_GetExpectedValue(int first, int second, int third, int fourth, int fifth, string expected)
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { first, second, third, fourth, fifth } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.OddEven;

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [Test]
        public void FourSeason_EventStatusIsRefund_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FourSeason;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void FourSeason_IsPendingMatch_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FourSeason;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [TestCase(1, 2, 3, 4, 5, "Sp")]
        [TestCase(1, 2, 5, 7, 18, "Sp")]
        [TestCase(1, 2, 5, 7, 19, "Su")]
        [TestCase(5, 7, 10, 11, 19, "Su")]
        [TestCase(5, 8, 10, 11, 19, "Au")]
        [TestCase(1, 13, 18, 19, 20, "Au")]
        [TestCase(1, 14, 18, 19, 20, "Wi")]
        public void FourSeason_IsNormalMatch_GetExpectedValue(int first, int second, int third, int fourth, int fifth, string expected)
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { first, second, third, fourth, fifth } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.FourSeason;

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [Test]
        public void MoreOddMoreEven_EventStatusIsRefund_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.MoreOddMoreEven;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void MoreOddMoreEven_IsPendingMatch_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.MoreOddMoreEven;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [TestCase(4, 8, 10, 11, 19, "ME")]
        [TestCase(5, 8, 10, 11, 19, "MO")]
        public void MoreOddMoreEven_IsNormalMatch_GetExpectedValue(int first, int second, int third, int fourth, int fifth, string expected)
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { first, second, third, fourth, fifth } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.MoreOddMoreEven;

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [Test]
        public void Combo_EventStatusIsRefund_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Combo;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [Test]
        public void Combo_IsPendingMatch_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Combo;

            // Assert
            Assert.AreEqual("N/A", actualValue);
        }

        [TestCase(4, 8, 10, 11, 19, "SE")]
        [TestCase(5, 8, 10, 11, 19, "BO")]
        public void Combo_IsNormalMatch_GetExpectedValue(int first, int second, int third, int fourth, int fifth, string expected)
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { first, second, third, fourth, fifth } };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Combo;

            // Assert
            Assert.AreEqual(expected, actualValue);
        }

        [Test]
        public void EventStatus_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { Ball = new List<int> { 1, 2, 3, 4, 5 }, EventStatus = "Completed" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.EventStatus;

            // Assert
            Assert.AreEqual("Completed", actualValue);
        }

        [Test]
        public void MatchCode_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { MatchCode = "123$4" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.MatchCode;

            // Assert
            Assert.AreEqual("123$4", actualValue);
        }

        [Test]
        public void MatchId_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { MatchId = 1234 };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.MatchId;

            // Assert
            Assert.AreEqual("1234", actualValue);
        }

        [Test]
        public void LeagueName_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { LeagueId = 1234 };
            subRefDataHelper.GetLeagueName(1234, "en-US", string.Empty).Returns("League");
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.LeagueName;

            // Assert
            Assert.AreEqual("League", actualValue);
        }

        [Test]
        public void IsPending_Always_GetExpectedValue()
        {
            // Arrange
            var resultData = new DTO.Happy5 { IsPending = true };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.IsPending;

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void Soccer5_StatusIsRefund_ReturnNA()
        {
            // Arrange
            var resultData = new DTO.Happy5 { EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Soccer5;

            // Assert
            Assert.AreEqual(Happy5Resources.NA, actualValue);
        }

        [Test]
        public void Soccer5_StatusIsCompleted_ReturnEmpty()
        {
            // Arrange
            var resultData = new DTO.Happy5 { EventStatus = "Completed" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Soccer5;

            // Assert
            Assert.AreEqual(string.Empty, actualValue);
        }

        [Test]
        public void Soccer5Class_StatusIsRefund_ReturnEmpty()
        {
            // Arrange
            var resultData = new DTO.Happy5 { EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Soccer5Class;

            // Assert
            Assert.AreEqual(string.Empty, actualValue);
        }

        [Test]
        public void Soccer5Class_StatusIsCompleted_ReturnInfo()
        {
            // Arrange
            var resultData = new DTO.Happy5 { EventStatus = "Completed" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Soccer5Class;

            // Assert
            Assert.AreEqual("info", actualValue);
        }

        [Test]
        public void Soccer5Function_StatusIsRefund_ReturnEmpty()
        {
            // Arrange
            var resultData = new DTO.Happy5 { EventStatus = "Refund" };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Soccer5Function;

            // Assert
            Assert.AreEqual(string.Empty, actualValue);
        }

        [Test]
        public void Soccer5Function_StatusIsCompleted_ReturnJavascriptFunction()
        {
            // Arrange
            var resultData = new DTO.Happy5
            {
                MatchId =123,
                EventStatus = "Completed",
                Ball = new List<int> { 1, 2, 3, 4, 5 }
            };
            var viewModel = new Happy5ViewModel(resultData, subResultSetting, subRefDataHelper);

            // Act
            var actualValue = viewModel.Soccer5Function;

            // Assert
            Assert.IsTrue(actualValue.Contains("showSoccer5Result(123, '1, 2, 3, 4, 5')"));
        }
    }
}