namespace Fanex.Result.AGCasino.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Core.AGCasino.DTO;
    using Core.AGCasino.Models;
    using Core.Base;
    using Core.TestUtils;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        private Builder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new Builder();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void HtmlBuilder_Always_ReturnNotNullHtml()
        {
            // Arrange
            var resultSetting = new ResultSetting { SportType = SportType.AGCasino };

            // Act
            var actualHtml = _builder.HtmlBuilder(new List<AGCasino>() { new AGCasino() }, resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("AGCasino.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void ExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.ExcelBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightHtmlBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.PopUpParalyBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.HtmlGameDetailBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightExcelBuilder(new List<AGCasino>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}