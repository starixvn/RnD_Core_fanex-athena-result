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
            Assert.AreEqual(5, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new ESportExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(4, excelViewModel.ColumnStatus);
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
            Assert.AreEqual("Final Score", actualHeaderRow.Cells[3].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[4].StringCellValue);
        }
    }
}