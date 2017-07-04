namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class BilliardViewModelTests
    {
        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = scoreValue };
            var viewModel = new BilliardViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new BilliardViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalAwayScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }
    }
}