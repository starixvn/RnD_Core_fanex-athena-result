namespace Fanex.Result.AGCasino.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Core.AGCasino.DTO;
    using Core.AGCasino.Models;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        [Test]
        public void ExcelBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.ExcelBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightHtmlBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.PopUpParalyBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.HtmlGameDetailBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightExcelBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}