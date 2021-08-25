namespace Fanex.Result.Core.Bitcoin.Tests.ViewModels
{
    using Bitcoin.ViewModels;
    using NUnit.Framework;
    using DTO;
    using Base;
    using System;
    using Base.Utilities;
    using NSubstitute;
    using System.Collections.Generic;

    [TestFixture]
    public class BitcoinViewModelTests
    {
        private Bitcoin _Bitcoin;
        private ResultSetting _setting;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _Bitcoin = new Bitcoin();
            _setting = new ResultSetting { LanguageCode = "en-US" };
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void KickOffTime_Always_GetFormatedTime()
        {
            // Arrange
            _Bitcoin.EventDate = DateTime.Parse("10/10/2017");

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("10/10/2017<br/>12:00:00 AM", viewModel.KickOffTime);
        }

        [Test]
        public void LeagueId_Always_GetFromBitcoin()
        {
            // Arrange
            _Bitcoin.League3rdId = 1;

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void LeagueName_Always_GetFromRef()
        {
            // Arrange
            _Bitcoin.League3rdId = 1;
            _refDataHelper.GetLeague3rdName(1, "en-US", string.Empty).Returns("League 3rd name");

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("League 3rd name", viewModel.LeagueName);
        }

        [Test]
        public void MatchId_Always_GetFromBitcoin()
        {
            // Arrange
            _Bitcoin.Match3rdId = 1;

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.MatchId);
        }

        [Test]
        public void ResultPrice_GameDetailIsEmpty_returnHyphen()
        {
            // Arrange
            _Bitcoin.GameResults = new List<GameDetail>();

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.ResultPrice);
        }

        [Test]
        public void ResultPrice_GameResultIsEmpty_returnHyphen()
        {
            // Arrange
            _Bitcoin.GameResults = new List<GameDetail> { new GameDetail { GameResult = string.Empty } };

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.ResultPrice);
        }

        [TestCase("123", "123.00")]
        [TestCase("123.123", "123.12")]
        [TestCase("1234.123", "1,234.12")]
        public void ResultPrice_Always_GameResultCorrectFormat(string price, string expectedFormat)
        {
            // Arrange
            _Bitcoin.GameResults = new List<GameDetail> { new GameDetail { GameResult = price } };

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual(expectedFormat, viewModel.ResultPrice);
        }

        [Test]
        public void EventStatus_EventStatusIsNotCompleted_ReturnHyphen()
        {
            // Arrange
            _Bitcoin.EventStatus = "running";

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.EventStatus);
        }

        [Test]
        public void EventStatus_EventStatusIsCompleted_BetStatusIsEmpty_ReturnCompleted()
        {
            // Arrange
            _Bitcoin.EventStatus = "completed";
            _Bitcoin.BetStatus = string.Empty;

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("Completed", viewModel.EventStatus);
        }

        [Test]
        public void EventStatus_EventStatusIsCompleted_BetStatusIsRefund_ReturnCompleted()
        {
            // Arrange
            _Bitcoin.EventStatus = "completed";
            _Bitcoin.BetStatus = "Refund";

            // Act
            var viewModel = new BitcoinViewModel(_Bitcoin, _setting);

            // Assert
            Assert.AreEqual("Refund", viewModel.EventStatus);
        }
    }
}