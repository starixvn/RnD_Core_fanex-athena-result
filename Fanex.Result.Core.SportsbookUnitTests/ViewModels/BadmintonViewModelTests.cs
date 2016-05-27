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
        public void GetHomeAwaySet_FromIndexGreaterThan4_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new BadmintonViewModel(Arg.Any<Sportsbook>(), new ResultSetting());
            var index = 7;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(3, "")]
        [TestCase(3, ";343-343")]
        public void GetHomeAwaySet_FromIndexLessThan5NBetStatusIsEmptyOrWrongFormat_ReturnsHyphen(
            int index,
            string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(3, ";1-2,3-1,2-3")]
        [TestCase(4, ";1-2,3-1,2-3,1-2")]
        [TestCase(4, ";1-2")]
        public void GetHomeAwaySet_FromIndexLessThan5AndEqualOrGreaterThanBetStatusSets_ReturnsHyphen(
            int index,
            string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(0, ";1-2,3-1,2-3", true, "1")]
        [TestCase(2, ";1-2,3-1,2-3,1-2", false, "3")]
        public void GetHomeAwaySet_FromIndexLessThanBetStatusSets_ReturnsExpectedSet(
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
    }
}