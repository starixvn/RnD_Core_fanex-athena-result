namespace Fanex.Result.Core.GoldDeluxe.Tests.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ViewModels;
    using TestUtils;
    using NUnit.Framework;
    using GoldDeluxe.ViewModels;
    using GoldDeluxe.ExcelModels;
    using NSubstitute;
    using Fanex.Result.Core.Base.Utilities;

    [TestFixture]
    public class GoldDeluxeExcelTests
    {
        private ResultSetting _resultSetting;
        private IEnumerable<IResultViewModel> _resultData;

        [SetUp]
        public void Setup()
        {
            _resultSetting = DataUtils.CreateResultSetting();
            _resultData = new List<GoldDeluxeViewModel>();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void ColumnTemplateCount_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new GoldDeluxeExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.ColumnTemplateCount);
        }

        [Test]
        public void ColumnStatus_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new GoldDeluxeExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(2, excelViewModel.ColumnStatus);
        }

        [Test]
        public void RowContentTemplate_Always_SetFromCtor()
        {
            // Act
            var excelViewModel = new GoldDeluxeExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.AreEqual(3, excelViewModel.RowContentTemplate);
        }

        [Test]
        public void AssignHeader_SheetIsNotNull_AssignExpectedHeaderValue()
        {
            // Arrange
            var excelViewModel = new GoldDeluxeExcel(TemplateFileUtils.GetExcelTemplatePath("GoldDeluxe.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualHeaderRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(1);
            Assert.AreEqual("Game ID", actualHeaderRow.Cells[0].StringCellValue);
            Assert.AreEqual("Result", actualHeaderRow.Cells[1].StringCellValue);
            Assert.AreEqual("Status", actualHeaderRow.Cells[2].StringCellValue);
        }

        [Test]
        public void SetDataForMatch_Always_SetRowValue()
        {
            // Arrange
            var goldDeluxeViewModel = new GoldDeluxeViewModel(new DTO.GoldDeluxe
            {
                Result = "A=1;Result=1"
            }, _resultSetting);

            _resultData = new List<GoldDeluxeViewModel> { goldDeluxeViewModel };
            var excelViewModel = new GoldDeluxeExcel(TemplateFileUtils.GetExcelTemplatePath("GoldDeluxe.xls"), _resultSetting, _resultData.ToList());

            // Act
            excelViewModel.Export();

            // Assert
            var actualDataRow = excelViewModel.Workbook.GetSheetAt(0).GetRow(3);
            Assert.AreEqual("1", actualDataRow.Cells[0].StringCellValue);
            Assert.AreEqual("1", actualDataRow.Cells[1].StringCellValue);
            Assert.AreEqual("Completed", actualDataRow.Cells[2].StringCellValue);
        }

        [Test]
        public void Ctor_Always_SetIsSupportMatchCodeToFalse()
        {
            // Act
            var excelViewModel = new GoldDeluxeExcel(string.Empty, _resultSetting, _resultData.ToList());

            // Assert
            Assert.IsFalse(excelViewModel.IsSupportMatchCode);
        }
    }
}