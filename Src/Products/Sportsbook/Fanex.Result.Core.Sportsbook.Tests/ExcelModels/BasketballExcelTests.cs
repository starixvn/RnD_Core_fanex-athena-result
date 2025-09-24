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
    public class BasketballExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<BasketBallViewModel>();
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new BasketBallExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(12, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new BasketBallExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(11, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new BasketBallExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new BasketBallExcel(TemplateFileUtils.GetExcelTemplatePath("BasketBall.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Match Code/ID", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Kick Off. Time", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Match", actualHeaderRow.Cells[2].StringCellValue);
            Assert.AreEqual("1Q", actualHeaderRow.Cells[3].StringCellValue);
            Assert.AreEqual("2Q", actualHeaderRow.Cells[4].StringCellValue);
            Assert.AreEqual("3Q", actualHeaderRow.Cells[5].StringCellValue);
            Assert.AreEqual("4Q", actualHeaderRow.Cells[6].StringCellValue);
            Assert.AreEqual("OT", actualHeaderRow.Cells[7].StringCellValue);
            Assert.AreEqual("HT Score", actualHeaderRow.Cells[8].StringCellValue);
            Assert.AreEqual("2H Score", actualHeaderRow.Cells[9].StringCellValue);
            Assert.AreEqual("Final Score", actualHeaderRow.Cells[10].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[11].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            _refDataHelper.GetTeamName(1, "en-US", string.Empty).Returns("Home Team Name");
            _refDataHelper.GetTeamName(2, "en-US", string.Empty).Returns("Away Team Name");
            var basketballViewModel = new BasketBallViewModel(new DTO.Sportsbook
            {
                MatchCode = "1231",
                MatchId = 1111,
                BetStatus = "21-23,21-16,21-16,0-0,1-1",
                HomeId = 1,
                AwayId = 2,
                EventStatus = "Completed",
                KickOffTime = DateTime.Parse("01/01/2017"),
                HtHomeScore = 9,
                HtAwayScore = 10,
                FinalHomeScore = 11,
                FinalAwayScore = 12,
            }, _resultSetting, null, _refDataHelper);

            _resultData = new List<BasketBallViewModel> { basketballViewModel };
            var excelViewModel = new BasketBallExcel(TemplateFileUtils.GetExcelTemplatePath("BasketBall.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHomeDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("1231", actualHomeDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualHomeDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Home Team Name", actualHomeDataRow.Cells[2].StringCellValue);
            Assert.AreEqual("-", actualHomeDataRow.Cells[3].StringCellValue);
            Assert.AreEqual("21", actualHomeDataRow.Cells[4].StringCellValue);
            Assert.AreEqual("21", actualHomeDataRow.Cells[5].StringCellValue);
            Assert.AreEqual("0", actualHomeDataRow.Cells[6].StringCellValue);
            Assert.AreEqual("1", actualHomeDataRow.Cells[7].StringCellValue);
            Assert.AreEqual("9", actualHomeDataRow.Cells[8].StringCellValue);
            Assert.AreEqual("2", actualHomeDataRow.Cells[9].StringCellValue);
            Assert.AreEqual("11", actualHomeDataRow.Cells[10].StringCellValue);
            Assert.AreEqual("Completed", actualHomeDataRow.Cells[11].StringCellValue);

            var actualAwayDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(4);
            Assert.AreEqual("1111", actualAwayDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualAwayDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Away Team Name", actualAwayDataRow.Cells[2].StringCellValue);
            Assert.AreEqual("23", actualAwayDataRow.Cells[3].StringCellValue);
            Assert.AreEqual("16", actualAwayDataRow.Cells[4].StringCellValue);
            Assert.AreEqual("16", actualAwayDataRow.Cells[5].StringCellValue);
            Assert.AreEqual("0", actualAwayDataRow.Cells[6].StringCellValue);
            Assert.AreEqual("1", actualAwayDataRow.Cells[7].StringCellValue);
            Assert.AreEqual("10", actualAwayDataRow.Cells[8].StringCellValue);
            Assert.AreEqual("2", actualAwayDataRow.Cells[9].StringCellValue);
            Assert.AreEqual("12", actualAwayDataRow.Cells[10].StringCellValue);
            Assert.AreEqual("Completed", actualAwayDataRow.Cells[11].StringCellValue);
        }
    }
}