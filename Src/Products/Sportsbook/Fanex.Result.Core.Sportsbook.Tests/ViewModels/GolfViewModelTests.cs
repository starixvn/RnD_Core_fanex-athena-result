namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class GolfViewModelTests
    {
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(_refDataHelper);
        }

        [TestCase(1, 0, "Won")]
        [TestCase(0, 1, "Lose")]
        public void FinalHomeScore_ScoreHasScoreText_ShowText(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, 1, "1")]
        [TestCase(2, 2, "2")]
        [TestCase(0, 2, "0")]
        [TestCase(null, null, "-")]
        public void FinalHomeScore_HasScore_ShowScore(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, 0, "Lose")]
        [TestCase(0, 1, "Won")]
        public void FinalAwayScore_ScoreHasScoreText_ShowText(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, 1, "1")]
        [TestCase(2, 2, "2")]
        [TestCase(0, 2, "2")]
        [TestCase(null, null, "-")]
        public void FinalAwayScore_HasScore_ShowScore(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }
    }
}