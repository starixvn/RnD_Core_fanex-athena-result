namespace Fanex.Result.Core.GoldDeluxe.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using GoldDeluxe.Models;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightHtmlBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.PopUpParalyBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.HtmlGameDetailBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightExcelBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}