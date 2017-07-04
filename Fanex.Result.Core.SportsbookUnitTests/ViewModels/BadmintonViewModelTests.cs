namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class BadmintonViewModelTests
    {
        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan4_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new BadmintonViewModel(new Sportsbook(), new ResultSetting());
            var index = 7;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(3, "")]
        [TestCase(3, null)]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndBetStatusIsEmptyOrEmpty_ReturnsHyphen(
            int index,
            string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndBetStatusDoesNotHaveComma_ReturnsHyphen()
        {
            // Arrange
            var index = 3;
            var betStatus = "; 343 - 343";
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        public void GetHomeAwaySet_FromIndexIsLessThan5AndThanBetStatusSetsIsEqualOrLessThanIndex_ReturnsHyphen()
        {
            // Arrange
            var index = 3;
            var betStatus = ";1-2,3-1,2-3";
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(0, ";1-2,3-1,2-3", true, "1")]
        [TestCase(2, "1-2,3-1,2-3,1-2", false, "3")]
        [TestCase(2, "1-2,3-1,2-3,", false, "3")]
        [TestCase(2, "1-2,3-1,2-3,0", false, "3")]
        [TestCase(2, "1-2,3-1,2-3,-", false, "3")]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndBetStatusSetsIsGreaterThanIndex_ReturnsExpectedSet(
            int index,
            string betStatus,
            bool isHome,
            string expectedSet)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, isHome);

            // Assert
            Assert.AreEqual(expectedSet, homeAwaySet);
        }

        [Test]
        public void TotalPointHome_WhenTotalPointIsGreaterThanZero_ShowTotalPointHome()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = "1-2,3-1,2-3,1-2" };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual("7", actualTotalPoint);
        }

        [Test]
        public void TotalPointHome_WhenTotalPointIsLessThanZero_ShowHyphen()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = "0-0,0-0,0-0,0-0" };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual("-", actualTotalPoint);
        }

        [Test]
        public void TotalPointAway_WhenTotalPointIsLGreaterThanZero_ShowTotalAwayPoints()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = "1-2,3-1,2-3,1-2" };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual("8", actualTotalPoint);
        }

        [Test]
        public void TotalPointAway_WhenTotalPointIsLessThanZero_ShowHyphen()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = "0-0,0-0,0-0,0-0" };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual("-", actualTotalPoint);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = scoreValue };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.TotalSetHome;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalAwayScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalAwayScore = scoreValue };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalAwayScore = viewModel.TotalSetAWay;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }
    }
}