namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class HockeyViewModelTests
    {
        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan3_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new HockeyViewModel(new Sportsbook(), new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(10, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexLessThan3AndBetStatusIsNullOrEmptyOrNotContainsComma_ReturnsHyphen(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());
            var index = 2;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexIs3AndBetStatusIsNullOrEmptyOrNotContainsComma_ReturnsZero(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());
            var index = 3;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("0", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIs3AndBetStatusHasLessThan4Sets_ReturnsZero()
        {
            // Arrange
            var betStatus = ";1-2,3-1";
            var index = 3;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("0", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan3AndBetStatusSetsIsNotGreaterThanIndex_ReturnsHyphen()
        {
            // Arrange
            var index = 2;
            var betStatus = ";1-2,3-1";
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(1, ";1-2,3-1,2-3", true, "3")]
        [TestCase(1, ";1-2,3-1,2-3,1-2,3-3", false, "1")]
        [TestCase(1, ";1-2,3-1,2-3,1-2,", false, "1")]
        [TestCase(1, ";1-2,3-1,2-3,1-2,-", false, "1")]
        [TestCase(1, ";1-2,3-1,2-3,1-2,0", false, "1")]
        [TestCase(0, ";1-2,3-1,2-3,1-2,3-3", false, "2")]
        public void GetHomeAwaySet_FromIndexIsLessThan4AndBetStatusSets_ReturnsExpectedHomeAwaySet(
            int index,
            string betStatus,
            bool isHome,
            string expectedHomeAwaySet)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, isHome);

            // Assert
            Assert.AreEqual(expectedHomeAwaySet, homeAwaySet);
        }
    }
}