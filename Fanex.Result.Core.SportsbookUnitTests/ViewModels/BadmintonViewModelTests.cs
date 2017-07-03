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
    }
}