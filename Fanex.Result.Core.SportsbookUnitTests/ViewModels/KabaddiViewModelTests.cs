namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class KabaddiViewModelTests
    {
        private ISportsbookRefDataHelper _refDataHelper;
        private ResultSetting _resultSetting;

        [SetUp]
        public void Setup()
        {
            _resultSetting = new ResultSetting
            {
                LanguageCode = "en-US"
            };
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
            _refDataHelper.GetBetRadarMatchCodeFormat(Arg.Any<string>(), Arg.Any<string>()).Returns("MatchCode");
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HtHomeScore = scoreValue, MatchCode = "124" };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting, _refDataHelper);

            // Act
            var actualHTHomeScore = viewModel.HTHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualHTHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTAwayScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HtAwayScore = scoreValue };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualHTAwayScore = viewModel.HTAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualHTAwayScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = scoreValue };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalAwayScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalAwayScore = scoreValue };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualFinalAwayScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }

        [Test]
        public void HalfTimeScore_IsRefund_ShowRefund()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalAwayScore = 1, FinalHomeScore = 2, BetStatus = "Refund;" };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualFinalAwayScore = viewModel.FinalAwayScore;
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalAwayScore);
            Assert.AreEqual("Refund", actualFinalHomeScore);
        }

        [Test]
        public void Score_IsRefundFT_ShowRefundForFinalScore()
        {
            // Arrange
            var sportsbookDto = new Sportsbook
            {
                HtAwayScore = 3,
                HtHomeScore = 4,
                FinalAwayScore = 1,
                FinalHomeScore = 2,
                BetStatus = "Refund FT;"
            };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualHTAwayScore = viewModel.HTAwayScore;
            var actualHTHomeScore = viewModel.HTHomeScore;
            var actualFinalAwayScore = viewModel.FinalAwayScore;
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalAwayScore);
            Assert.AreEqual("Refund", actualFinalHomeScore);
            Assert.AreEqual("4", actualHTHomeScore);
            Assert.AreEqual("3", actualHTAwayScore);
        }

        [Test]
        public void Score_IsRefund_ShowRefund()
        {
            // Arrange
            var sportsbookDto = new Sportsbook
            {
                HtAwayScore = 3,
                HtHomeScore = 4,
                FinalAwayScore = 1,
                FinalHomeScore = 2,
                BetStatus = "Refund;"
            };
            var viewModel = new KabaddiViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualHTAwayScore = viewModel.HTAwayScore;
            var actualHTHomeScore = viewModel.HTHomeScore;
            var actualFinalAwayScore = viewModel.FinalAwayScore;
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalAwayScore);
            Assert.AreEqual("Refund", actualFinalHomeScore);
            Assert.AreEqual("Refund", actualHTHomeScore);
            Assert.AreEqual("Refund", actualHTAwayScore);
        }
    }
}