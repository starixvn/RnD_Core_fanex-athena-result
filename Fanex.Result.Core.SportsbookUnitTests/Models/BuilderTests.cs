namespace Fanex.Result.Core.Sportsbook.Tests.Models
{
    using System.Collections.Generic;
    using Base;
    using DTO;
    using NUnit.Framework;
    using Sportsbook.Models;
    using TestUtils;

    [TestFixture]
    public class BuilderTests
    {
        private ResultSetting _resultSetting;
        private IList<DTO.Sportsbook> _dataList;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultSetting.SportType = SportType.Soccer;
            _dataList = new List<DTO.Sportsbook>();
        }

        [Test]
        public void HtmlBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlBuilder(_dataList, _resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("Soccer.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
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
        public void OutrightHtmlBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var outrightDataList = new List<Outright>()
            {
                new Outright()
            };
            var builder = new Builder();

            // Act
            var actualHtml = builder.OutrightHtmlBuilder(outrightDataList, _resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("Outright.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void OutrightExcelBuilder_Always_GetNotNullExcelStream()
        {
            // Arrange
            var outrightDataList = new List<Outright>()
            {
                new Outright()
            };
            var builder = new Builder();
            _resultSetting.ExcelTemplatePath = TemplateFileUtils.GetExcelTemplateFolderPath();

            // Act
            var actualExcelMemoryStream = builder.OutrightExcelBuilder(outrightDataList, _resultSetting);

            // Assert
            Assert.NotNull(actualExcelMemoryStream);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var parlayDataList = new List<Parlay>()
            {
                new Parlay()
            };
            var builder = new Builder();

            // Act
            var actualHtml = builder.PopUpParalyBuilder(parlayDataList, _resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("Parlay.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlGameDetailBuilder(new Sportsbook(), _resultSetting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("SoccerDetail.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }
    }
}