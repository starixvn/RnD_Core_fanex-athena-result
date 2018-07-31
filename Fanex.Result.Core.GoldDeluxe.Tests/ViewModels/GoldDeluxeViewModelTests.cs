namespace Fanex.Result.Core.GoldDeluxe.Tests.ViewModels
{
    using Base;
    using Base.Utilities;
    using DTO;
    using GoldDeluxe.ViewModels;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class GoldDeluxeViewModelTests
    {
        private GoldDeluxe _goldDeluxe;
        private IRefDataHelper _refDataHelper;
        private ResultSetting _setting;

        [SetUp]
        public void Setup()
        {
            _goldDeluxe = new GoldDeluxe();
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _setting = new ResultSetting { LanguageCode = "en-US" };
        }

        [Test]
        public void LeagueName_Always_GetFromRemotingRef()
        {
            // Arrange
            _goldDeluxe.Bettype = 1;
            _refDataHelper.GetBetTypeNameById("1", "en-US", string.Empty).Returns("LeagueName");

            // Act
            var viewModel = new GoldDeluxeViewModel(_goldDeluxe, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("LeagueName", viewModel.LeagueName);
        }

        [Test]
        public void EventStatus_Always_ReturnCompleted()
        {
            // Act
            var viewModel = new GoldDeluxeViewModel(_goldDeluxe, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Completed", viewModel.EventStatus);
        }

        [TestCase("A=1;1", "1")]
        [TestCase("A=;1", "")]
        [TestCase("A=", null)]
        public void GameId_WithGameToken_ReturnExpectedValue(string result, string expectedGameId)
        {
            // Arrange
            _goldDeluxe.Result = result;

            // Act
            var viewModel = new GoldDeluxeViewModel(_goldDeluxe, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(expectedGameId, viewModel.GameID);
        }

        [TestCase("Betchoice=R159DD343;Result=P DIAMOND 5 HEART K CLUB K B HEART 9 SPADE 5",
            "<ul class=\"banker\"><li><span class=\"B\"><span class=\"icon\"></span><span class=\"card\">Banker&nbsp;(4)&nbsp;:</span></span></li><li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">9</span></span></li><li><span class=\"spade\"><span class=\"icon\">&spades;</span><span class=\"card\">5</span></span></li></ul><ul class=\"player\"><li><span class=\"P\"><span class=\"icon\"></span><span class=\"card\">Player&nbsp;(5)&nbsp;:</span></span></li><li><span class=\"diamond\"><span class=\"icon\">&diams;</span><span class=\"card\">5</span></span></li><li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">K</span></span></li><li><span class=\"club\"><span class=\"icon\">&clubs;</span><span class=\"card\">K</span></span></li></ul>")]
        [TestCase("", "No information is available")]
        public void ResultInHtml_WithDetailGameResult_ReturnExpectedValue(string resultToken, string expectedResult)
        {
            // Arrange
            _goldDeluxe.Result = resultToken;

            // Act
            var viewModel = new GoldDeluxeViewModel(_goldDeluxe, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.ResultInHtml);
        }

        [TestCase("A=1;Result=1", "1")]
        [TestCase("A=1;Result=", "")]
        [TestCase("A=1", null)]
        public void ResultInExcel_WithDetailGameResult_ReturnExpectedValue(string resultToken, string expectedResult)
        {
            // Arrange
            _goldDeluxe.Result = resultToken;

            // Act
            var viewModel = new GoldDeluxeViewModel(_goldDeluxe, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.ResultInExcel);
        }
    }
}