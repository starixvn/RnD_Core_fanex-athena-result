namespace Fanex.Result.Core.Lucky3.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Base;
    using Data;
    using Fanex.Result.Core.Base.Utilities;
    using Lucky3.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        private ResultSetting resultSetting;
        private List<DTO.Lucky3> dataList;

        [SetUp]
        public void Setup()
        {
            resultSetting = DataHelper.CreateResultSetting();
            dataList = new List<DTO.Lucky3> { DataHelper.CreateLucky3Data() };
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void HtmlBuilder_Always_GetExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlBuilder(dataList, resultSetting);

            // Assert
            var expectedHtml = File.ReadAllText(FilePath.ExpectedHtmlFilePath);
            Assert.AreEqual(RemoveEndLineSymbolHtml(expectedHtml), RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void ExcelBuilder_Always_GetExpectedExcel()
        {
            // Arrange
            var builder = new Builder();
            resultSetting.ExcelTemplatePath = FilePath.ExcelTemplatePath;
            var excelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(dataList, resultSetting);

            // Act
            var actualExcelMemoryStream = builder.ExcelBuilder(dataList, resultSetting);

            // Assert
            Assert.AreEqual(excelViewModel.Export(), actualExcelMemoryStream);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowNotImplementedException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.HtmlGameDetailBuilder(dataList, resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowNotImplementedException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightExcelBuilder(dataList, resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowNotImplementedException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.OutrightHtmlBuilder(dataList, resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowNotImplementedException()
        {
            // Arrange
            var builder = new Builder();

            // Act
            TestDelegate testDelegate = () => builder.PopUpParalyBuilder(dataList, resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        private static string RemoveEndLineSymbolHtml(string htmlText)
            => htmlText.Replace("\r", string.Empty).Replace(" ", string.Empty).Replace("\n", string.Empty);
    }
}