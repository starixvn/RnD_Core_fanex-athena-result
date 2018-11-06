namespace Fanex.Result.Core.Sportsbook.Tests.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ExcelModels;
    using Sportsbook.ViewModels;
    using TestUtils;

    [TestFixture]
    public class OutRightExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<OutrightViewModel>();
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new OutrightExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new OutrightExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(4, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new OutrightExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_Always_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new OutrightExcel(TemplateFileUtils.GetExcelTemplatePath("Outright.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Time", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Team", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Results", actualHeaderRow.Cells[2].StringCellValue);
        }

        [Test]
        public void AssignNoInfoRow_WhenHasNoResultData_SetNoInfoRowValue()
        {
            // Arrange
            var excelViewModel = new OutrightExcel(TemplateFileUtils.GetExcelTemplatePath("Outright.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualNoInfoRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(2);
            Assert.AreEqual("No information is available", actualNoInfoRow.Cells[0].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("Team Name");
            var outrightViewModel = new OutrightViewModel(new DTO.Outright
            {
                TeamId = 1,
                Status = "Completed",
                EventDate = DateTime.Parse("01/01/2017")
            }, _resultSetting, _refDataHelper);

            _resultData = new List<OutrightViewModel> { outrightViewModel };
            var excelViewModel = new OutrightExcel(TemplateFileUtils.GetExcelTemplatePath("Outright.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("Team Name", actualDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Completed", actualDataRow.Cells[2].StringCellValue);
        }
    }
}