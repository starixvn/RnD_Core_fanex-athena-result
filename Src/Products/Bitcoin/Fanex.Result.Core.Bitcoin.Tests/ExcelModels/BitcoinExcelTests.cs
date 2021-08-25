namespace Fanex.Result.Core.Bitcoin.Tests.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.Utilities;
    using Base.ViewModels;
    using NSubstitute;
    using Bitcoin.ExcelModels;
    using Bitcoin.ViewModels;
    using NUnit.Framework;
    using TestUtils;
    using Fanex.Result.Core.Bitcoin.DTO;

    [TestFixture]
    public class BitcoinExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<BitcoinViewModel>();
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new BitcoinExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(4, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new BitcoinExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new BitcoinExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new BitcoinExcel(TemplateFileUtils.GetExcelTemplatePath("Bitcoin.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Date", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Match", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Result Price", actualHeaderRow.Cells[2].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[3].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            var BitcoinViewModel = new BitcoinViewModel(new DTO.Bitcoin
            {
                EventDate = DateTime.Parse("01/01/2017"),
                EventStatus = "Completed",
                Match3rdId = 1111,
                BetStatus = string.Empty,
                GameResults = new List<GameDetail> { new GameDetail { Match3rdId = 1111, GameResult = "1234.123" } },
            }, _resultSetting, _refDataHelper);

            _resultData = new List<BitcoinViewModel> { BitcoinViewModel };
            var excelViewModel = new BitcoinExcel(TemplateFileUtils.GetExcelTemplatePath("Bitcoin.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("01/01/2017\n12:00:00 AM", actualDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("1111", actualDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("1,234.12", actualDataRow.Cells[2].StringCellValue);
            Assert.AreEqual("Completed", actualDataRow.Cells[3].StringCellValue);
        }
    }
}