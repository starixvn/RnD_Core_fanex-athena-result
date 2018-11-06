namespace Fanex.Result.AGCasino.Tests.Utilitties
{
    using Core.AGCasino.Utilities;
    using NUnit.Framework;

    [TestFixture]
    public class AGCasinoHelperTest
    {
        private string _bankerText;
        private string _playerText;
        private string _htmlPatternExpression;

        [SetUp]
        public void Setup()
        {
            _bankerText = "Banker";
            _playerText = "Player";
            _htmlPatternExpression = @"</?\w+\s+[^>]*>";
        }

        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0", "B;H5;CK;DA:P;HK;DQ;HJ")]
        [TestCase("round=DSP*tableCode=va21*game=DT*type=21*val=*result=D;H3:T;C9*winResult=22*result2=D;3:T;9", "D;H3:T;C9")]
        [TestCase("round=AGQ*tableCode=va21*game=SHB*type=41*val=*result=4;3;3*winResult=42;44;54;55;61;73;85*result2=", "4;3;3")]
        [TestCase("round=DSP*tableCode=va21*game=ROU*type=101*val=6*result=20*winResult=108;112;115;116;119*result2=", "20")]
        [TestCase("round=DSP*tableCode=va21*game=ROU*type=101*val=6*result=*winResult=108;112;115;116;119*result2=", "")]
        [TestCase("round=DSP*tableCode=va21*game=ROU*type=101*val=6*winResult=108;112;115;116;119*result2=", "")]
        [TestCase("round=DSP*tableCode=va21*game=ROU*type=101*val=*winResult=108;112;115;116;119*result2=", "")]
        [TestCase("round=DSP*tableCode=va21*game=ROU*type=101*val=*result=**winResult=108;112;115;116;119*result2=", "")]
        public void GetAGCasinoResultToken_WhenAnyResult_ReturnValidResultToken(string inputResult, string expectedResultToken)
        {
            //// Action
            var result = AGCasinoHelper.GetAGCasinoResultToken(inputResult);

            StringAssert.AreEqualIgnoringCase(expectedResultToken, result);
        }

        [Test]
        public void GetBacaratResult_WhenValidResult_ReturnValidHtml()
        {
            //// Arrange
            var inputResult = "B;H5;C8;D7:P;H10;D9;HJ";
            //// Action

            var result = AGCasinoHelper.GetBacaratResult(inputResult, _bankerText, _playerText);

            StringAssert.IsMatch(_htmlPatternExpression, result);
        }

        [TestCase("B;H5;C8;D7:P;H10;D9;", "(0)")]
        [TestCase("B;H9;C10;D7:P;H10;D9;S3", "(6)")]
        [TestCase("B;H10;CJ;DK:P;H10;DK;SJ", "(0)")]
        public void GetBacaratResult_WhenValidResult_ReturnValidBankerPoint(string inputResult, string expectedResult)
        {
            // Action
            var result = AGCasinoHelper.GetBacaratResult(inputResult, _bankerText, _playerText);

            // Assert
            StringAssert.Contains(expectedResult, result);
        }

        [TestCase("B;H5;C8;D7:P;H10;D9;", "(9)")]
        [TestCase("B;H5;C8;D7:P;H10;D9;S3", "(2)")]
        [TestCase("B;H5;C8;D7:P;H10;DK;SJ", "(0)")]
        public void GetBacaratResult_WhenValidResult_ReturnValidPlayerPoint(string inputResult, string expectedResult)
        {
            // Action
            var result = AGCasinoHelper.GetBacaratResult(inputResult, _bankerText, _playerText);

            // Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void GetBacaratResult_WhenEmptyResult_ReturnEmptyText()
        {
            // Arrange
            var expectedResult = string.Empty;

            // Action
            var result = AGCasinoHelper.GetBacaratResult(string.Empty, _bankerText, _playerText);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetBacaratResult_WhenNullResult_ThrowException()
        {
            //// Arrange
            string inputResult = null;
            //// Assert
            Assert.Throws<System.NullReferenceException>(() => AGCasinoHelper.GetBacaratResult(inputResult, _bankerText, _playerText));
        }

        [TestCase("20")]
        [TestCase("0")]
        public void GetRoutletResult_WhenValidResult_ReturnValidStringText(string inputResult)
        {
            //// Arrange
            var expectedResultPattern = "<span class=\"black\">{0}</span>";
            var expectedResult = string.Format(expectedResultPattern, inputResult);
            //// Action
            var result = AGCasinoHelper.GetRoutletResult(inputResult);
            //// Assert
            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }

        [Test]
        public void GetSicBoResult_WhenValidResult_ReturnValidHtml()
        {
            // Arrange
            var inputResult = "4;3;3";

            // Action
            var result = AGCasinoHelper.GetSicBoResult(inputResult);

            // Assert
            StringAssert.IsMatch(_htmlPatternExpression, result);
        }

        [Test]
        public void GetDragonTigerResult_WhenValidResult_ReturnValidHtml()
        {
            // Arrange
            var inputResult = "D;H3:T;C9";

            // Action
            var result = AGCasinoHelper.GetDragonTigerResult(inputResult, _bankerText, _playerText);

            // Assert
            StringAssert.IsMatch(_htmlPatternExpression, result);
        }
    }
}