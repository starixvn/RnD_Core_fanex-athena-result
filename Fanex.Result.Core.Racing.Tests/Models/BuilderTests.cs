namespace Fanex.Result.Core.Racing.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using NUnit.Framework;
    using Racing.Models;

    [TestFixture]
    public class BuilderTests
    {
        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightHtmlBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.PopUpParalyBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.HtmlGameDetailBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightExcelBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}