namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System.Collections.Generic;
    using Base;
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class ExtensionTests
    {
        [TestCase(1, "Soccer")]
        [TestCase(44, "MuayThai")]
        [TestCase(2, "BasketBall")]
        [TestCase(32, "BasketBall")]
        [TestCase(5, "Tennis")]
        [TestCase(9, "Badminton")]
        [TestCase(203, "MicroGamingLC")]
        public void StringMe_FromSportType_ReturnsExpectedTemplateName(SportType sportType, string expectedTemplateName)
        {
            // Act
            var templateName = sportType.StringMe();

            // Assert
            Assert.AreEqual(expectedTemplateName, templateName);
        }

        [Test]
        public void ToSafeList_FromINummerable_ReturnsSafeList()
        {
            // Arrange
            IEnumerable<int> list = new List<int> { 1, 2, 3 };

            // Act
            var safeList = list.ToSafeList();

            // Assert
            Assert.AreEqual(3, safeList.Count);
        }
    }
}