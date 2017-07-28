namespace Fanex.Result.Core.Lucky3.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using Data;
    using Lucky3.ViewModels;
    using NUnit.Framework;
    using Resources;

    [TestFixture]
    public class Lucky3ExcelViewModelTests
    {
        private const string DateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";
        private ResultSetting resultSetting;
        private IEnumerable<IResultViewModel> resultData;
        private IRefDataHelper refDataHelper;

        [SetUp]
        public void Setup()
        {
            resultSetting = DataHelper.CreateResultSetting();
            resultData = new List<Lucky3ViewModel>();
            refDataHelper = DataHelper.CreateRefDataHelper();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Lucky3ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.ColumnTemplateCount;

            // Assert
            Assert.AreEqual(8, actualValue);
        }

        [Test]
        public void ColumnStatus_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Lucky3ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.ColumnStatus;

            // Assert
            Assert.AreEqual(7, actualValue);
        }

        [Test]
        public void RowContentTemplate_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Lucky3ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.RowContentTemplate;

            // Assert
            Assert.AreEqual(4, actualValue);
        }

        [Test]
        public void RowLeagueTemplate_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Lucky3ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.RowLeagueTemplate;

            // Assert
            Assert.AreEqual(3, actualValue);
        }

        [Test]
        public void AssignHeader_Always_AssignExactColumn()
        {
            // Arrange
            var excelViewModel = CreateExcelViewModel();
            excelViewModel.Export();

            // Act
            var workbook = excelViewModel.Workbook;

            // Assert
            var expected1stHeaderRow = workbook.GetSheetAt(0).GetRow(1);
            var expected2ndHeaderRow = workbook.GetSheetAt(0).GetRow(2);
            Assert.True(
                expected1stHeaderRow.Cells[0].StringCellValue == LangResource.MatchCode + "/" + LangResource.ID &&
                expected1stHeaderRow.Cells[1].StringCellValue == LangResource.KickOffTime &&
                expected1stHeaderRow.Cells[2].StringCellValue == LangResource.Match &&
                expected1stHeaderRow.Cells[3].StringCellValue == Lucky3Resources.DrawNumber &&
                expected1stHeaderRow.Cells[4].StringCellValue == Lucky3Resources.NumberOfHits &&
                expected1stHeaderRow.Cells[7].StringCellValue == LangResource.Status &&
                expected2ndHeaderRow.Cells[4].StringCellValue == Lucky3Resources.FirstPrize &&
                expected2ndHeaderRow.Cells[5].StringCellValue == Lucky3Resources.SecondPrize &&
                expected2ndHeaderRow.Cells[6].StringCellValue == Lucky3Resources.ThirdPrize);
        }

        [Test]
        public void SetDataForMAtch_Always_SetExactData()
        {
            // Arrange
            var lucky3ViewModel = CreateViewModel();
            resultData = new List<Lucky3ViewModel> { lucky3ViewModel };
            var excelViewModel = CreateExcelViewModel();
            excelViewModel.Export();

            // Act
            var workbook = excelViewModel.Workbook;

            // Asserts
            var expectedDataRow = workbook.GetSheetAt(0).GetRow(4);
            Assert.True(
                expectedDataRow.Cells[0].StringCellValue == Helper.ToRtfString("1" + "<br/>" + 2) &&
                expectedDataRow.Cells[1].StringCellValue == Helper.ToRtfString(Helper.FullDateFormat(DateTime.Parse("01/01/2017"), DateTimeFormat)) &&
                expectedDataRow.Cells[2].StringCellValue == $"{Lucky3Resources.Lucky3GameNo} 1" &&
                expectedDataRow.Cells[3].StringCellValue == "1-2-3" &&
                expectedDataRow.Cells[4].StringCellValue == "1" &&
                expectedDataRow.Cells[5].StringCellValue == "2" &&
                expectedDataRow.Cells[6].StringCellValue == "3" &&
                expectedDataRow.Cells[7].StringCellValue == "Completed");
        }

        private Lucky3ViewModel CreateViewModel()
            => new Lucky3ViewModel(DataHelper.CreateLucky3Data(), resultSetting, refDataHelper);

        private Lucky3ExcelViewModel CreateExcelViewModel()
        {
            return new Lucky3ExcelViewModel(
                FilePath.ExcelTemplateFilePath,
                resultSetting,
                resultData.ToList());
        }
    }
}