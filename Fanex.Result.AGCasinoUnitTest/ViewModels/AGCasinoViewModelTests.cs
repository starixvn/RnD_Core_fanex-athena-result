namespace Fanex.Result.AGCasino.Tests.ViewModels
{
    using Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Core.AGCasino.DTO;
    using Core.Base;
    using Core.AGCasino.ViewModels;

    [TestFixture]
    public class AGCasinoViewModelTests
    {
        private AGCasino _casino;
        private ResultSetting _setting;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _setting = new ResultSetting { LanguageCode = "en-US" };
            _casino = new AGCasino();
        }

        [Test]
        public void LeagueName_Always_GetBetTypeNameFromRemotingRef()
        {
            // Arrange
            _casino.Bettype = 1;
            _refDataHelper.GetBetTypeNameById("1", "en-US", string.Empty).Returns("League");

            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("League", viewModel.LeagueName);
        }

        [Test]
        public void EventStatus_Always_ReturnCompleted()
        {
            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Completed", viewModel.EventStatus);
        }

        [Test]
        public void Result_BetTypeIsBacarat_ReturnBacaratHtml()
        {
            // Arrange
            _casino.Bettype = 1801;
            _casino.Result = "round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0";

            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            var expectedResult = "<ul class=\"banker\"><li><span class=\"B\">" +
                "<span class=\"icon\"></span><span class=\"card\">Banker&nbsp;(5)&nbsp;:</span></span></li>" +
                "<li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">5</span></span></li><li><span class=\"club\"><span class=\"icon\">&clubs;</span><span class=\"card\">K</span></span></li><li><span class=\"diamond\"><span class=\"icon\">&diams;</span><span class=\"card\">A</span></span></li></ul><ul class=\"player\"><li><span class=\"P\"><span class=\"icon\"></span><span class=\"card\">Player&nbsp;(0)&nbsp;:</span></span></li><li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">K</span></span></li><li><span class=\"diamond\"><span class=\"icon\">&diams;</span><span class=\"card\">Q</span></span></li><li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">J</span></span></li></ul>";
            Assert.AreEqual(expectedResult, viewModel.Result);
        }

        [Test]
        public void Result_BetTypeIsRoulette_ReturnRouletteHtml()
        {
            // Arrange
            _casino.Bettype = 1802;
            _casino.Result = "round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0";

            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            var expectedResult = "<span class=\"black\">B;H5;CK;DA:P;HK;DQ;HJ</span>";
            Assert.AreEqual(expectedResult, viewModel.Result);
        }

        [Test]
        public void Result_BetTypeIsDragonTiger_ReturnDragonTigerHtml()
        {
            // Arrange
            _casino.Bettype = 1803;
            _casino.Result = "round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0";

            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            var expectedResult = "<ul class=\"tiger\"><li><span class=\"T\"><span class=\"icon\"></span><span class=\"card\">Tiger&nbsp;:</span></span></li><li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">5</span></span></li></ul><ul class=\"tiger\"><li><span class=\"T\"><span class=\"icon\"></span><span class=\"card\">Tiger&nbsp;:</span></span></li><li><span class=\"heart\"><span class=\"icon\">&hearts;</span><span class=\"card\">K</span></span></li></ul>";
            Assert.AreEqual(expectedResult, viewModel.Result);
        }

        [Test]
        public void Result_BetTypeIsSicBo_ReturnSicBoHtml()
        {
            // Arrange
            _casino.Bettype = 1804;
            _casino.Result = "round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0";

            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            var expectedResult = "<span class=\"black\">B</span><span class=\"black\">H5</span><span class=\"black\">CK</span><span class=\"black\">DA:P</span><span class=\"black\">HK</span><span class=\"black\">DQ</span><span class=\"black\">HJ</span>";
            Assert.AreEqual(expectedResult, viewModel.Result);
        }

        [Test]
        public void Result_BetTypeIsNotAGCasino_ReturnEmpty()
        {
            // Arrange
            _casino.Bettype = 1;
            _casino.Result = "round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0";

            // Act
            var viewModel = new AGCasinoViewModel(_casino, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.Result);
        }
    }
}