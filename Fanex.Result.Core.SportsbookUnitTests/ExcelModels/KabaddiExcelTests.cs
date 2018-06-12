namespace Fanex.Result.Core.Sportsbook.Tests.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ViewModels;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ExcelModels;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels;
    using TestUtils;

    [TestFixture]
    public class KabaddiExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private ISportsbookRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<KabaddiViewModel>();
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new KabaddiExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(6, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new KabaddiExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(5, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new KabaddiExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new KabaddiExcel(TemplateFileUtils.GetExcelTemplatePath("NewSports.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Match Code/ID", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Kick Off. Time", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Match", actualHeaderRow.Cells[2].StringCellValue);
            Assert.AreEqual("HT Score", actualHeaderRow.Cells[3].StringCellValue);
            Assert.AreEqual("Final Score", actualHeaderRow.Cells[4].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[5].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("Home Team Name");
            _refDataHelper.GetTeamName(2, "en", string.Empty).Returns("Away Team Name");
            _refDataHelper.GetBetRadarMatchCodeFormat("1231", Arg.Any<string>()).Returns("1231");
            _resultSetting.SportType = SportType.VirtualSoccerNation;

            var viewModel = new KabaddiViewModel(new DTO.Sportsbook
            {
                MatchCode = "1231",
                MatchId = 1111,
                BetStatus = "21-23,21-16,21-16,0-0,0-0",
                HomeId = 1,
                AwayId = 2,
                EventStatus = "Completed",
                KickOffTime = DateTime.Parse("01/01/2017"),
                HtHomeScore = 9,
                HtAwayScore = 10,
                FinalHomeScore = 11,
                FinalAwayScore = 12,
            }, _resultSetting, _refDataHelper);

            _resultData = new List<KabaddiViewModel> { viewModel };
            var excelViewModel = new KabaddiExcel(TemplateFileUtils.GetExcelTemplatePath("NewSports.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHomeDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("1231", actualHomeDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualHomeDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Home Team Name", actualHomeDataRow.Cells[2].StringCellValue);
            Assert.AreEqual("9", actualHomeDataRow.Cells[3].StringCellValue);
            Assert.AreEqual("11", actualHomeDataRow.Cells[4].StringCellValue);
            Assert.AreEqual("Completed", actualHomeDataRow.Cells[5].StringCellValue);

            var actualAwayDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(4);
            Assert.AreEqual("1111", actualAwayDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualAwayDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Away Team Name", actualAwayDataRow.Cells[2].StringCellValue);
            Assert.AreEqual("10", actualAwayDataRow.Cells[3].StringCellValue);
            Assert.AreEqual("12", actualAwayDataRow.Cells[4].StringCellValue);
            Assert.AreEqual("Completed", actualAwayDataRow.Cells[5].StringCellValue);
        }
    }
}