namespace Fanex.Result.Core.GoldDeluxe.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using DTO;
    using GoldDeluxe.Models;
    using NUnit.Framework;
    using TestUtils;

    [TestFixture]
    public class BuilderTests
    {
        private Builder _builder;
        private IList<GoldDeluxe> _dataList;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _builder = new Builder();
            _dataList = new List<GoldDeluxe>() { new GoldDeluxe() };
            _setting = new ResultSetting { SportType = SportType.GoldDeluxe, LanguageCode = "en-US" };
        }

        [Test]
        public void HtmlBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlBuilder(_dataList, _setting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("GoldDeluxe.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void ExcelBuilder_Always_GetExpectedExcel()
        {
            // Arrange
            var builder = new Builder();
            _setting.ExcelTemplatePath = TemplateFileUtils.GetExcelTemplateFolderPath();
            _dataList.Add(new GoldDeluxe { Bettype = 1801, Result = "A=1;Result=1" });
            var excelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(_dataList, _setting);

            // Act
            var actualExcelMemoryStream = builder.ExcelBuilder(_dataList, _setting);

            // Assert
            Assert.AreEqual(excelViewModel.Export(), actualExcelMemoryStream);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightHtmlBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.PopUpParalyBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.HtmlGameDetailBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightExcelBuilder(new List<GoldDeluxe>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}