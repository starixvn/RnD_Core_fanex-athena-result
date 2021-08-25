namespace Fanex.Result.Core.Bitcoin.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using DTO;
    using Bitcoin.Models;
    using NUnit.Framework;
    using TestUtils;

    [TestFixture]
    public class BuilderTests
    {
        private Builder _builder;
        private ResultSetting _resultSetting;
        private IList<Bitcoin> _dataList;

        [SetUp]
        public void SetUp()
        {
            _builder = new Builder();
            _resultSetting = DataUtils.CreateResultSetting();
            _resultSetting.SportType = SportType.Bitcoin;
            _dataList = new List<Bitcoin>();
        }

        [Test]
        public void HtmlBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlBuilder(_dataList, _resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("Bitcoin.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
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
            TestDelegate testDelegate = () => _builder.OutrightHtmlBuilder(new List<Bitcoin>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.PopUpParalyBuilder(new List<Bitcoin>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.HtmlGameDetailBuilder(new List<Bitcoin>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightExcelBuilder(new List<Bitcoin>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}