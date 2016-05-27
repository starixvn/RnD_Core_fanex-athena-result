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
        public void GetHomeAwaySet_FromIndexGreaterThan3_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new HockeyViewModel(Arg.Any<Sportsbook>(), new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(10, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(";343-343")]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexLessThan3NBetStatusIsNullOrWrongFormat_ReturnsHyphen(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());
            var index = 2;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(";343-343")]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexIs3NBetStatusIsNullOrWrongFormat_ReturnsZero(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());
            var index = 3;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("0", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromBetStatusHasLessThan4SetsNIndexIs3_ReturnsZero()
        {
            // Arrange
            var betStatus = ";1-2,3-1";
            var index = 3;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("0", homeAwaySet);
        }

        [TestCase(";1-2,3-1", 2)]
        [TestCase(";1-2", 2)]
        public void GetHomeAwaySet_FromBetStatusHasLessThan4SetsNIndexIsNot3AndEqualOrGreaterThanSetNums_ReturnsHyphen(
            string betStatus,
            int index)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new HockeyViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(1, ";1-2,3-1,2-3", true, "3")]
        [TestCase(1, ";1-2,3-1,2-3,1-2,3-3", false, "1")]
        [TestCase(0, ";1-2,3-1,2-3,1-2,3-3", false, "2")]
        public void GetHomeAwaySet_FromIndexLessThanBetStatusSets_ReturnsExpectedHomeAwaySet(
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