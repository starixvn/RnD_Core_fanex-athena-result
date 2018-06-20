namespace Fanex.Result.Core.NumberGame.Tests.ViewModels
{
    using NumberGame.ViewModels;
    using NUnit.Framework;
    using DTO;
    using Base;
    using System;
    using Base.Utilities;
    using NSubstitute;
    using System.Collections.Generic;

    [TestFixture]
    public class NumberGameViewModelTests
    {
        private NumberGame _numberGame;
        private ResultSetting _setting;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _numberGame = new NumberGame();
            _setting = new ResultSetting { LanguageCode = "en-US" };
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void KickOffTime_Always_GetFormatedTime()
        {
            // Arrange
            _numberGame.KickOffTime = DateTime.Parse("10/10/2017");

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("10/10/2017<br/>12:00:00 AM", viewModel.KickOffTime);
        }

        [Test]
        public void IsPending_Always_GetFromNumberGame()
        {
            // Arrange
            _numberGame.IsPending = false;

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual(false, viewModel.IsPending);
        }

        [Test]
        public void LeagueId_Always_GetFromNumberGame()
        {
            // Arrange
            _numberGame.LeagueId = 1;

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void LeagueName_Always_GetFromRef()
        {
            // Arrange
            _numberGame.LeagueId = 1;
            _refDataHelper.GetLeagueName(1, "en", string.Empty).Returns("League");

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("League", viewModel.LeagueName);
        }

        [Test]
        public void MatchCode_Always_GetFromNumberGame()
        {
            // Arrange
            _numberGame.MatchCode = "1";

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.MatchCode);
        }

        [Test]
        public void MatchId_Always_GetFromNumberGame()
        {
            // Arrange
            _numberGame.MatchId = 1;

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.MatchId);
        }

        [Test]
        public void GameNo_Always_GetFromNumberGame()
        {
            // Arrange
            _numberGame.MatchCode = "1";

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.GameNo);
        }

        [Test]
        public void FirstBall_BallHasData_Get1stBall()
        {
            // Arrange
            _numberGame.Ball = new List<int> { 1, 2, 3 };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("01", viewModel.FirstBall);
        }

        [Test]
        public void FirstBall_BallHasNoData_GetHyphen()
        {
            // Arrange
            _numberGame.Ball = new List<int> { };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.FirstBall);
        }

        [Test]
        public void LastBall_BallHasData_GetLastBall()
        {
            // Arrange
            _numberGame.Ball = new List<int> { 1, 2, 3 };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("03", viewModel.LastBall);
        }

        [Test]
        public void LastBall_BallHasNoData_GetHyphen()
        {
            // Arrange
            _numberGame.Ball = new List<int> { };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.LastBall);
        }

        [Test]
        public void Warrior_BallHasNoData_GetHyphen()
        {
            // Arrange
            _numberGame.Ball = new List<int> { };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.Warrior);
        }

        [TestCase(2, 1, "2nd ball win")]
        [TestCase(1, 2, "3rd ball win")]
        public void Warrior_BallHasValidData_WithBallValue_GetExpectedResult(int secondBall, int thirdBall, string expectedResult)
        {
            // Arrange
            _numberGame.Ball = new List<int> { 1, secondBall, thirdBall };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.Warrior);
        }

        [TestCase(1, 2, 1, "Even")]
        [TestCase(2, 1, 2, "Odd")]
        public void OEFT_SumBallIsDivisibleBy2_ReturnExpectedResult(int firstBall, int secondBall, int thirdBall, string expectedResult)
        {
            // Arrange
            _numberGame.Ball = new List<int> { firstBall, secondBall, thirdBall };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.OEFT);
        }

        [Test]
        public void Result_Always_ReturnExpectedResult()
        {
            // Arrange
            _numberGame.Ball = new List<int> { 1, 2, 3 };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("01", viewModel.Result[0]);
            Assert.AreEqual("02", viewModel.Result[1]);
            Assert.AreEqual("03", viewModel.Result[2]);
        }

        [Test]
        public void EventStatus_Always_ReturnExpectedResult()
        {
            // Arrange
            _numberGame.Ball = new List<int> { 1, 2, 3 };
            _numberGame.EventStatus = "Completed";

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("Completed", viewModel.EventStatus);
        }

        [TestCase(0, "00")]
        [TestCase(01, "01")]
        [TestCase(10, "10")]
        [TestCase(100, "100")]
        public void ShowBall_FromBall_ReturnsExpectedFormattedTwoDigits(int ball, string expectedBall)
        {
            // Act
            var formattedBall = NumberGameViewModel.ShowBall(ball);

            // Assert
            Assert.AreEqual(expectedBall, formattedBall);
        }
    }
}