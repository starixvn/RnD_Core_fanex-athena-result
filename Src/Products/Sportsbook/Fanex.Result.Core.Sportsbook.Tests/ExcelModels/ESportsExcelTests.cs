namespace Fanex.Result.Core.Sportsbook.Tests.ExcelModels
{
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
    public class EsportExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<ESportViewModel>();
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new ESportExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(14, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new ESportExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(13, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new ESportExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new ESportExcel(TemplateFileUtils.GetExcelTemplatePath("ESport.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Match Code/ID", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Kick Off. Time", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Match", actualHeaderRow.Cells[2].StringCellValue);
            Assert.AreEqual("M1", actualHeaderRow.Cells[3].StringCellValue);
            Assert.AreEqual("M2", actualHeaderRow.Cells[4].StringCellValue);
            Assert.AreEqual("M3", actualHeaderRow.Cells[5].StringCellValue);
            Assert.AreEqual("M4", actualHeaderRow.Cells[6].StringCellValue);
            Assert.AreEqual("M5", actualHeaderRow.Cells[7].StringCellValue);
            Assert.AreEqual("M6", actualHeaderRow.Cells[8].StringCellValue);
            Assert.AreEqual("M7", actualHeaderRow.Cells[9].StringCellValue);
            Assert.AreEqual("M8", actualHeaderRow.Cells[10].StringCellValue);
            Assert.AreEqual("M9", actualHeaderRow.Cells[11].StringCellValue);
            Assert.AreEqual("Final Score", actualHeaderRow.Cells[12].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[13].StringCellValue);
        }
    }
}