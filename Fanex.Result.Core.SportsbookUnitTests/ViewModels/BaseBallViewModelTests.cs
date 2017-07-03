namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NSubstitute;
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
    }
}