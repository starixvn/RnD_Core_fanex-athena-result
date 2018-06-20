namespace Fanex.Result.Core.Sportsbook.Tests.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Racing.DTO;
    using Racing.ExcelModels;
    using Racing.ViewModels;
    using TestUtils;

    [TestFixture]
    public class RacingExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<RacingViewModel>();
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new RacingExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(7, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new RacingExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(6, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new RacingExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new RacingExcel(TemplateFileUtils.GetExcelTemplatePath("Racing.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Race", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Results", actualHeaderRow.Cells[3].StringCellValue);
            Assert.AreEqual("Win", actualHeaderRow.Cells[4].StringCellValue);
            Assert.AreEqual("Place", actualHeaderRow.Cells[5].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[6].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            _refDataHelper.GetLeagueName(1, "en", string.Empty).Returns("LeagueName");
            _refDataHelper.GetLeagueName(2, "en", string.Empty).Returns("LeagueName2");
            _refDataHelper.GetHorseTeamName("11", "en", string.Empty).Returns("Team");
            GenerateViewModel();
            var excelViewModel = new RacingExcel(TemplateFileUtils.GetExcelTemplatePath("Racing.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var leagueRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(2);
            Assert.AreEqual("LEAGUENAME", leagueRow.Cells[0].StringCellValue);

            var actualDataRowFirst = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("1", actualDataRowFirst.Cells[0].StringCellValue);
            Assert.AreEqual("1st", actualDataRowFirst.Cells[1].StringCellValue);
            Assert.AreEqual("1", actualDataRowFirst.Cells[2].StringCellValue);
            Assert.AreEqual("Team", actualDataRowFirst.Cells[3].StringCellValue);
            Assert.AreEqual("1.00", actualDataRowFirst.Cells[4].StringCellValue);
            Assert.AreEqual("5.00", actualDataRowFirst.Cells[5].StringCellValue);
            Assert.AreEqual("Official \nSCR:1 \nTF:2", actualDataRowFirst.Cells[6].StringCellValue);

            var actualDataRowSecond = excelViewModel.Workbook.GetSheetAt(0).GetRow(4);
            Assert.AreEqual("1", actualDataRowSecond.Cells[0].StringCellValue);
            Assert.AreEqual("2nd", actualDataRowSecond.Cells[1].StringCellValue);
            Assert.AreEqual("1", actualDataRowSecond.Cells[2].StringCellValue);
            Assert.AreEqual("Team", actualDataRowSecond.Cells[3].StringCellValue);
            Assert.AreEqual("1.00", actualDataRowSecond.Cells[4].StringCellValue);
            Assert.AreEqual("5.00", actualDataRowSecond.Cells[5].StringCellValue);
            Assert.AreEqual("Official \nSCR:1 \nTF:2", actualDataRowFirst.Cells[6].StringCellValue);

            var leagueRow2 = excelViewModel.Workbook.GetSheetAt(0).GetRow(5);
            Assert.AreEqual("LEAGUENAME2", leagueRow2.Cells[0].StringCellValue);

            actualDataRowFirst = excelViewModel.Workbook.GetSheetAt(0).GetRow(7);
            Assert.Null(actualDataRowFirst);
        }

        private void GenerateViewModel()
        {
            _resultData = new List<RacingViewModel> {
                new RacingViewModel(
                    new Racing
                    {
                        LeagueId = 1,
                        RaceName = 1,
                        Place = 1,
                        EngineNo = "1",
                        EngineId = "11",
                        WinOdds = 1,
                        PlaceOdds = 5,
                        SCROfMatch = "1",
                        TFOfMatch = "2"
                    },
                _resultSetting,
                _refDataHelper),
                new RacingViewModel(
                   new Racing
                   {
                       LeagueId = 1,
                       RaceName = 1,
                       Place = 2,
                       EngineNo = "1",
                       EngineId = "11",
                       WinOdds = 1,
                       PlaceOdds = 5,
                       SCROfMatch = "1",
                       TFOfMatch = "2"
                   },
               _resultSetting,
               _refDataHelper),
                new RacingViewModel(
                   new Racing
                   {
                       LeagueId = 2,
                       RaceName = 1,
                       Place = 2,
                       EngineNo = "1",
                       EngineId = "11",
                       WinOdds = 1,
                       PlaceOdds = 5,
                       SCROfMatch = "1",
                       TFOfMatch = "2"
                   },
               _resultSetting,
               _refDataHelper),
                new RacingViewModel(
                   new Racing
                   {
                       LeagueId = 2,
                       RaceName = 1,
                       Place = 5,
                       EngineNo = "1",
                       EngineId = "11",
                       WinOdds = 1,
                       PlaceOdds = 5,
                       SCROfMatch = "1",
                       TFOfMatch = "2"
                   },
               _resultSetting,
               _refDataHelper)};
        }
    }
}