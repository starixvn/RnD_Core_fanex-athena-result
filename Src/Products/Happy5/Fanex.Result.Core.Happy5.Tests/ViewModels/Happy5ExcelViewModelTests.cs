namespace Fanex.Result.Core.Happy5.Tests.ViewModels
{
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Happy5.DataDomain;
    using Fanex.Result.Core.Happy5.Resources;
    using Fanex.Result.Core.Happy5.Tests.Data;
    using Fanex.Result.Core.Happy5.ViewModels;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class Happy5ExcelViewModelTests
    {
        private const string DateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";
        private ResultSetting resultSetting;
        private IEnumerable<IResultViewModel> resultData;
        private IRefDataHelper refDataHelper;

        [SetUp]
        public void Setup()
        {
            resultSetting = new ResultSetting();
            resultData = new List<Happy5ViewModel>();
            refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Happy5ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.ColumnTemplateCount;

            // Assert
            Assert.AreEqual(11, actualValue);
        }

        [Test]
        public void ColumnStatus_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Happy5ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.ColumnStatus;

            // Assert
            Assert.AreEqual(10, actualValue);
        }

        [Test]
        public void RowContentTemplate_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Happy5ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.RowContentTemplate;

            // Assert
            Assert.AreEqual(3, actualValue);
        }

        [Test]
        public void RowLeagueTemplate_Always_SetValueFromCtor()
        {
            // Arrange
            var excelViewModel = new Happy5ExcelViewModel(string.Empty, resultSetting, resultData.ToList());

            // Act
            var actualValue = excelViewModel.RowLeagueTemplate;

            // Assert
            Assert.AreEqual(2, actualValue);
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
            var expectedHeaderRow = workbook.GetSheetAt(0).GetRow(1);
            Assert.True(
                expectedHeaderRow.Cells[0].StringCellValue == LangResource.MatchCode + "/" + LangResource.ID &&
                expectedHeaderRow.Cells[1].StringCellValue == LangResource.KickOffTime &&
                expectedHeaderRow.Cells[2].StringCellValue == LangResource.Match &&
                expectedHeaderRow.Cells[3].StringCellValue == LangResource.Result &&
                expectedHeaderRow.Cells[4].StringCellValue == Happy5Resources.Total &&
                expectedHeaderRow.Cells[5].StringCellValue == Constant.BigSmall &&
                expectedHeaderRow.Cells[6].StringCellValue == Constant.OddEven &&
                expectedHeaderRow.Cells[7].StringCellValue == Constant.FourSeasons &&
                expectedHeaderRow.Cells[8].StringCellValue == Constant.MoreOddMoreEven &&
                expectedHeaderRow.Cells[9].StringCellValue == Constant.Combo &&
                expectedHeaderRow.Cells[10].StringCellValue == LangResource.Status);
        }

        [Test]
        public void SetDataForMAtch_Always_SetExactData()
        {
            // Arrange
            var viewModel = CreateViewModel();
            resultData = new List<Happy5ViewModel> { viewModel };
            var excelViewModel = CreateExcelViewModel();
            excelViewModel.Export();

            // Act
            var workbook = excelViewModel.Workbook;

            // Asserts
            var expectedDataRow = workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual(expectedDataRow.Cells[0].StringCellValue, Helper.ToRtfString("1" + "<br/>" + 2));
            Assert.AreEqual(expectedDataRow.Cells[1].StringCellValue, Helper.ToRtfString(Helper.FullDateFormat(DateTime.Parse("01/01/2017"), DateTimeFormat)));
            Assert.AreEqual(expectedDataRow.Cells[2].StringCellValue, $"{Happy5Resources.No} 1");
#pragma warning disable S3415 // Assertion arguments should be passed in the correct order
            Assert.AreEqual(expectedDataRow.Cells[3].StringCellValue, "1-2-3-4-5");
            Assert.AreEqual(expectedDataRow.Cells[4].StringCellValue, "15");
            Assert.AreEqual(expectedDataRow.Cells[5].StringCellValue, "S");
            Assert.AreEqual(expectedDataRow.Cells[6].StringCellValue, "O");
            Assert.AreEqual(expectedDataRow.Cells[7].StringCellValue, "Sp");
            Assert.AreEqual(expectedDataRow.Cells[8].StringCellValue, "MO");
            Assert.AreEqual(expectedDataRow.Cells[9].StringCellValue, "SO");
            Assert.AreEqual(expectedDataRow.Cells[10].StringCellValue, "Completed");
#pragma warning restore S3415 // Assertion arguments should be passed in the correct order
        }

        private Happy5ViewModel CreateViewModel()
            => new Happy5ViewModel(
                new DTO.Happy5
                {
                    KickOffTime = new DateTime(2017, 1, 1),
                    MatchCode = "1",
                    MatchId = 2,
                    LeagueId = 11,
                    EventStatus = "completed",
                    Ball = new List<int> { 1, 2, 3, 4, 5 }
                },
                resultSetting, refDataHelper);

        private Happy5ExcelViewModel CreateExcelViewModel()
        {
            return new Happy5ExcelViewModel(
                FilePath.ExcelTemplateFilePath,
                resultSetting,
                resultData.ToList());
        }
    }
}