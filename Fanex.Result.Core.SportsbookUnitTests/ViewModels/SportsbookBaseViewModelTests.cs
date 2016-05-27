namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class SportsbookBaseViewModelTests
    {
        private SportsbookBaseViewModel _sportsbookBaseViewModel;

        [SetUp]
        public void SetUp()
        {
            var sportsbookDTO = new Sportsbook();

            var resultSetting = new ResultSetting
            {
                SportType = SportType.Soccer
            };

            _sportsbookBaseViewModel = new SportsbookBaseViewModel(sportsbookDTO, resultSetting);
        }

        [TestCase(0, "red", "green", "red")]
        [TestCase(1, "red", "green", "green")]
        public void Color_FromIndexNBgNOddBg_ReturnsExpectedBg(int index, string bg, string oddBg, string expectedBg)
        {
            // Act
            var realBg = _sportsbookBaseViewModel.Color(index, bg, oddBg);

            // Assert
            Assert.AreEqual(expectedBg, realBg);
        }

        [Test]
        public void GetHomeAwaySet_Always_ReturnsHyphen()
        {
            // Act
            var homeAwaySet = _sportsbookBaseViewModel.GetHomeAwaySet(Arg.Any<int>(), Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }
    }
}