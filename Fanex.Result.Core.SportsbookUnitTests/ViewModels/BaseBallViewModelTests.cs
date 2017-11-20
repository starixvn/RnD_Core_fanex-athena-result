namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class BaseBallViewModelTests
    {
        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan9_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new BaseBallViewModel(new Sportsbook(), new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(10, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("sdjsdf23423")]
        public void GetHomeAwaySet_FromIndexIs9AndBetStatusIsNullOrEmptyOrHaveNotComma_ReturnsMinus1(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());
            var index = 9;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-1", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("sdjsdf23423")]
        public void GetHomeAwaySet_FromIndexIsLessThan9AndBetStatusIsNullOrEmptyOrHaveNotComma_ReturnsHyphen(string betStatus)
        {
            // Arrange
            var index = 3;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIs9AndBetStatusSetsIsLessThan10_ReturnsMinus1()
        {
            // Arrange
            var betStatus = ";1-2,3-1,2-3,1-2,3-3,3-1,2-3";
            var index = 9;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-1", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan9AndBetStatusSetsIsNotGreaterThanIndex_ReturnsHyphen()
        {
            // Arrange
            var betStatus = ";1-2,3-1,2-3,1-2,3-3,3-1,2-3";
            var index = 8;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(2, ";1-2,3-1,2-3", true, "2")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,3-3", false, "3")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,3-3,", false, "3")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,3-3,-", false, "3")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,3-3,0", false, "3")]
        [TestCase(9, ";1-2,3-1,2-3,1-2,3-3,3-1,2-3,1-2,3-3,2-3", false, "3")]
        public void GetHomeAwaySet_FromIndexIsLessThan10AndBetStatusSets_ReturnsExpectedHomeAwaySet(
            int index,
            string betStatus,
            bool isHome,
            string expectedHomeAwaySet)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, isHome);

            // Assert
            Assert.AreEqual(expectedHomeAwaySet, homeAwaySet);
        }

        [TestCase("2", "2")]
        [TestCase("0", "0")]
        [TestCase("-2", "-")]
        [TestCase("", "-")]
        public void EIAway_ForEachCase_ShowExpectedValue(string eiawayValue, string expectedValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = ";1-2,3-1,2-3,1-2,3-3,3-1,2-3,1-2,3-3,2-" + eiawayValue };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualValue = viewModel.EIAway;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("2", "2")]
        [TestCase("0", "0")]
        [TestCase("-2", "-")]
        [TestCase("", "-")]
        public void EIHome_ForEachCase_ShowExpectedValue(string eihomeValue, string expectedValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = ";1-2,3-1,2-3,1-2,3-3,3-1,2-3,1-2,3-3," + eihomeValue + "-2" };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualValue = viewModel.EIHome;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HtHomeScore = scoreValue };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

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
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalAwayScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }
    }
}