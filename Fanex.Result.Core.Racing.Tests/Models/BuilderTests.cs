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
        private Builder _bulder;

        [SetUp]
        public void SetUp()
        {
            _bulder = new Builder();
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _bulder.OutrightHtmlBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _bulder.PopUpParalyBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _bulder.HtmlGameDetailBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _bulder.OutrightExcelBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}