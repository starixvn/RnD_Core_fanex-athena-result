namespace Fanex.Result.Core.GoldDeluxe.Tests.Utilities
{
    using GoldDeluxe.Utilities;
    using NUnit.Framework;

    public class GoldDeluxeHelperTests
    {
        [Test]
        public void GetRoutletResult_FromResultIsZero_ReturnsRoutletTemplateContainsGreenClassAndResultIsZero()
        {
            // Arrange
            var result = "0";

            // Act
            var routletResult = GoldDeluxeHelper.GetRoutletResult(result);

            // Assert
            var expectedRoutletTemplate = "<span class=\"green\">0</span>";
            Assert.AreEqual(expectedRoutletTemplate, routletResult);
        }

        [TestCase("1")]
        [TestCase("3")]
        public void GetRoutletResult_FromResultIsRed_ReturnsRoutletTemplateContainsRedClassAndResult(string result)
        {
            // Act
            var routletResult = GoldDeluxeHelper.GetRoutletResult(result);

            // Assert
            var expectedRoutletTemplate = "<span class=\"red\">" + result + "</span>";
            Assert.AreEqual(expectedRoutletTemplate, routletResult);
        }

        [TestCase("2")]
        [TestCase("4")]
        public void GetRoutletResult_FromResultIsNotRed_ReturnsRoutletTemplateContainsBlackClassAndResult(string result)
        {
            // Act
            var routletResult = GoldDeluxeHelper.GetRoutletResult(result);

            // Assert
            var expectedRoutletTemplate = "<span class=\"black\">" + result + "</span>";
            Assert.AreEqual(expectedRoutletTemplate, routletResult);
        }
    }
}