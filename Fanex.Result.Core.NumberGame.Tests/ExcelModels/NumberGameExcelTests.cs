namespace Fanex.Result.Core.NumberGame.Tests.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.Utilities;
    using Base.ViewModels;
    using NSubstitute;
    using NumberGame.ExcelModels;
    using NumberGame.ViewModels;
    using NUnit.Framework;
    using TestUtils;

    [TestFixture]
    public class NumberGameExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<NumberGameViewModel>();
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new NumberGameExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(9, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new NumberGameExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(8, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new NumberGameExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new NumberGameExcel(TemplateFileUtils.GetExcelTemplatePath("NumberGame.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Match Code/ID", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Kick Off. Time", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Match", actualHeaderRow.Cells[2].StringCellValue);
            Assert.AreEqual("Results", actualHeaderRow.Cells[3].StringCellValue);
            Assert.AreEqual("Warrior", actualHeaderRow.Cells[4].StringCellValue);
            Assert.AreEqual("O/E FT", actualHeaderRow.Cells[5].StringCellValue);
            Assert.AreEqual("1st Ball", actualHeaderRow.Cells[6].StringCellValue);
            Assert.AreEqual("Last Ball", actualHeaderRow.Cells[7].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[8].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("Home Team Name");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("Away Team Name");
            var numberGameViewModel = new NumberGameViewModel(new DTO.NumberGame
            {
                Ball = new List<int> { 1, 2, 3 },
                MatchCode = "1231",
                MatchId = 1111,
                EventStatus = "Completed",
                KickOffTime = DateTime.Parse("01/01/2017"),
            }, _resultSetting, _refDataHelper);

            _resultData = new List<NumberGameViewModel> { numberGameViewModel };
            var excelViewModel = new NumberGameExcel(TemplateFileUtils.GetExcelTemplatePath("NumberGame.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("1231\n1111", actualDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Number Game No 1231", actualDataRow.Cells[2].StringCellValue);
            Assert.AreEqual("01-02-03", actualDataRow.Cells[3].StringCellValue);
            Assert.AreEqual("3rd ball win", actualDataRow.Cells[4].StringCellValue);
            Assert.AreEqual("Even", actualDataRow.Cells[5].StringCellValue);
            Assert.AreEqual("01", actualDataRow.Cells[6].StringCellValue);
            Assert.AreEqual("03", actualDataRow.Cells[7].StringCellValue);
            Assert.AreEqual("Completed", actualDataRow.Cells[8].StringCellValue);
        }
    }
}