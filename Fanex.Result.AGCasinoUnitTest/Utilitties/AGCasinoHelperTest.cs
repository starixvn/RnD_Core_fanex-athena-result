// -----------------------------------------------------------------------
// <copyright file="AGCasinoHelperTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.AGCasinoUnitTest.Utilitties
{
    using System;
    using Fanex.Result.Core.AGCasino.Utilities;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class AGCasinoHelperTest
    {
        private string bankerText;
        private string playerText;
        private string htmlPatternExpression;
        [SetUp]
        public void Setup()
        {
            bankerText = "Banker";
            playerText = "Player";
            htmlPatternExpression = @"</?\w+\s+[^>]*>";
        }

        [Test]
        public void GetBacaratResult_WhenValidResult_ReturnValidHtml()
        {
            //Arrange
            var inputResult = "B;H5;C8;D7:P;H10;D9;HJ";
            //Action

            var result = AGCasinoHelper.GetBacaratResult(inputResult, bankerText, playerText);

            StringAssert.IsMatch(htmlPatternExpression, result);
        }

        [TestCase("B;H5;C8;D7:P;H10;D9;", "(0)")]
        [TestCase("B;H9;C10;D7:P;H10;D9;S3", "(6)")]
        [TestCase("B;H10;CJ;DK:P;H10;DK;SJ", "(0)")]
        public void GetBacaratResult_WhenValidResult_ReturnValidBankerPoint(string inputResult, string expectedResult)
        {
            //Action
            var result = AGCasinoHelper.GetBacaratResult(inputResult, bankerText, playerText);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [TestCase("B;H5;C8;D7:P;H10;D9;", "(9)")]
        [TestCase("B;H5;C8;D7:P;H10;D9;S3", "(2)")]
        [TestCase("B;H5;C8;D7:P;H10;DK;SJ", "(0)")]
        public void GetBacaratResult_WhenValidResult_ReturnValidPlayerPoint(string inputResult, string expectedResult)
        {
            //Action
            var result = AGCasinoHelper.GetBacaratResult(inputResult, bankerText, playerText);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void GetBacaratResult_WhenEmptyResult_ReturnEmptyText()
        {
            //Arrange
            var expectedResult = string.Empty;

            //Action
            var result = AGCasinoHelper.GetBacaratResult(string.Empty, bankerText, playerText);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetBacaratResult_WhenNullResult_ThrowException()
        {
            //Arrange
            string inputResult = null;
            //Assert
            Assert.Throws<System.NullReferenceException>(() => AGCasinoHelper.GetBacaratResult(inputResult, bankerText, playerText));
        }

        [TestCase("20")]
        [TestCase("0")]
        public void GetRoutletResult_WhenValidResult_ReturnValidStringText(string inputResult)
        {
            //Arrange
            var expectedResultPattern = "<span class=\"black\">{0}</span>";
            var expectedResult = string.Format(expectedResultPattern, inputResult);
            //Action
            var result = AGCasinoHelper.GetRoutletResult(inputResult);
            //Assert
            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }

        [Test]
        public void GetSicBoResult_WhenValidResult_ReturnValidHtml()
        {
            //Arrange
            var inputResult = "4;3;3";

            //Action
            var result = AGCasinoHelper.GetSicBoResult(inputResult);

            //Assert
            StringAssert.IsMatch(htmlPatternExpression, result);
        }


        [Test]
        public void GetDragonTigerResult_WhenValidResult_ReturnValidHtml()
        {
            //Arrange
            var inputResult = "D;H3:T;C9";

            //Action
            var result = AGCasinoHelper.GetDragonTigerResult(inputResult, bankerText, playerText);

            //Assert
            StringAssert.IsMatch(htmlPatternExpression, result);
        }
    }
}