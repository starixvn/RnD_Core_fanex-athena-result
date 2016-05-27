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
        public void GetHomeAwaySet_FromIndexGreaterThan9_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new BaseBallViewModel(Arg.Any<Sportsbook>(), new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(10, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(";343-343")]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexLessThan9NBetStatusIsNullOrWrongFormat_ReturnsHyphen(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());
            var index = 5;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase("")]
        [TestCase(";343-343")]
        [TestCase("343-343")]
        public void GetHomeAwaySet_FromIndexIs9NBetStatusIsNullOrWrongFormat_ReturnsNegative1(string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());
            var index = 9;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-1", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromBetStatusHasLessThan10SetsNIndexIs9_ReturnsNegative1()
        {
            // Arrange
            var betStatus = ";1-2,3-1,2-3,1-2,3-3,3-1,2-3";
            var index = 9;
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-1", homeAwaySet);
        }

        [TestCase(";1-2,3-1,2-3,1-2,3-3,3-1,2-3", 7)]
        [TestCase(";1-2,3-1,2-3,1-2,3-3,3-1,2-3", 8)]
        public void GetHomeAwaySet_FromBetStatusHasLessThan10SetsNIndexIsNot9AndEqualOrGreaterThanSetNums_ReturnsHyphen(
            string betStatus,
            int index)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = betStatus };
            var viewModel = new BaseBallViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(2, ";1-2,3-1,2-3", true, "2")]
        [TestCase(2, ";1-2,3-1,2-3,1-2,3-3", false, "3")]
        [TestCase(9, ";1-2,3-1,2-3,1-2,3-3,3-1,2-3,1-2,3-3,2-3", false, "3")]
        public void GetHomeAwaySet_FromIndexLessThanBetStatusSets_ReturnsExpectedHomeAwaySet(
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