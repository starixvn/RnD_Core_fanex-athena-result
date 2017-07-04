namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class OtherViewModelTests
    {
        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HtHomeScore = scoreValue };
            var viewModel = new OtherViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new OtherViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new OtherViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new OtherViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalAwayScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }
    }
}