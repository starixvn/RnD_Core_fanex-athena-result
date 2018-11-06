namespace Fanex.Result.Core.MetricGaming.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using TestUtils;
    using MetricGaming.DTO;
    using MetricGaming.Models;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        private Builder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new Builder();
        }

        [Test]
        public void HtmlBuilder_Always_ReturnNotNullHtml()
        {
            // Arrange
            var resultSetting = new ResultSetting { SportType = SportType.AGCasino };

            // Act
            var actualHtml = _builder.HtmlBuilder(new List<MetricGaming>(), resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("MetricGaming.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void ExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.ExcelBuilder(new List<MetricGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightHtmlBuilder(new List<MetricGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.PopUpParalyBuilder(new List<MetricGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.HtmlGameDetailBuilder(new List<MetricGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightExcelBuilder(new List<MetricGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}