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
            _refDataHelper.GetLeagueName(1, "en-US", string.Empty).Returns("League");

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
        public void FirstBall_BallHasNoData_Get00()
        {
            // Arrange
            _numberGame.Ball = new List<int> { };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("00", viewModel.FirstBall);
        }

        [Test]
        public void LastBall_BallHasData_GetLastBall()
        {
            // Arrange
            _numberGame.Ball = new List<int> { 1, 2, 3 };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("03", viewModel.ThirdBall);
        }

        [Test]
        public void LastBall_BallHasNoData_Get00()
        {
            // Arrange
            _numberGame.Ball = new List<int> { };

            // Act
            var viewModel = new NumberGameViewModel(_numberGame, _setting);

            // Assert
            Assert.AreEqual("00", viewModel.ThirdBall);
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
            Assert.AreEqual("01", viewModel.FirstBall);
            Assert.AreEqual("02", viewModel.SecondBall);
            Assert.AreEqual("03", viewModel.ThirdBall);
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
    }
}