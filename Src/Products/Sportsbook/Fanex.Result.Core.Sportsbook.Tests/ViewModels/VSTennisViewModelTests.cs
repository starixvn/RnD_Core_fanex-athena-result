namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class VSTennisViewModelTests
    {
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void CorrectScore_CallRefResource_CorrectKey()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = "betstatus" };
            var viewModel = new VSTennisViewModel(sportsbookDto, new ResultSetting(), _refDataHelper);

            // Act
            var actual = viewModel.CorrectScore;

            // Assert
            _refDataHelper.Received().GetResourceDataByRefIdAndResourceId("BetChoice", "bet1235_betstatus", "en-US", string.Empty);
        }

        [TestCase(1, null, "-")]
        [TestCase(null, 1, "-")]
        [TestCase(1, 1, "2")]
        public void TotalPoint_Is_HomePlusAwayScore(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new VSTennisViewModel(sportsbookDto, new ResultSetting(), _refDataHelper);

            // Act
            var actualFinalHomeScore = viewModel.TotalPoint;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HomePoint_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = scoreValue };
            var viewModel = new VSTennisViewModel(sportsbookDto, new ResultSetting(), _refDataHelper);

            // Act
            var actualFinalHomeScore = viewModel.HomePoint;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void AwayPoint_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalAwayScore = scoreValue };
            var viewModel = new VSTennisViewModel(sportsbookDto, new ResultSetting(), _refDataHelper);

            // Act
            var actualFinalAwayScore = viewModel.AwayPoint;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }
    }
}