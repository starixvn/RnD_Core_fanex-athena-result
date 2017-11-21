namespace Fanex.Result.Core.Racing.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using DTO;
    using NUnit.Framework;
    using Racing.Models;
    using TestUtils;

    [TestFixture]
    public class BuilderTests
    {
        private Builder _builder;
        private ResultSetting _resultSetting;
        private IList<Racing> _dataList;

        [SetUp]
        public void SetUp()
        {
            _resultSetting = new ResultSetting
            {
                SportType = SportType.HorseRacing,
                LanguageCode = "en-US"
            };

            _dataList = new List<Racing>();
            _builder = new Builder();
        }

        [Test]
        public void HtmlBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlBuilder(_dataList, _resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("Racing.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void ExcelBuilder_Always_GetExpectedExcel()
        {
            // Arrange
            var builder = new Builder();
            _resultSetting.ExcelTemplatePath = TemplateFileUtils.GetExcelTemplateFolderPath();
            var excelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(_dataList, _resultSetting);

            // Act
            var actualExcelMemoryStream = builder.ExcelBuilder(_dataList, _resultSetting);

            // Assert
            Assert.AreEqual(excelViewModel.Export(), actualExcelMemoryStream);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightHtmlBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.PopUpParalyBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.HtmlGameDetailBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightExcelBuilder(new List<Racing>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}