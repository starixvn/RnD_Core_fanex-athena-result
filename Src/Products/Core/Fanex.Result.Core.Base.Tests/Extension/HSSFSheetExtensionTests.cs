namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Base.Extension;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class HssfSheetExtensionTests
    {
        private HSSFWorkbook _workbook;
        private ISheet _sheet;

        [SetUp]
        public void SetUp()
        {
            var templatePath = AppDomain.CurrentDomain.BaseDirectory + "/../../Data/Soccer.xls";
            var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
            _workbook = new HSSFWorkbook(stream);
            _sheet = _workbook.GetSheetAt(0);
        }

        [Test]
        public void CopyRow_4Params_FromSourceRowIndexLessThanNumberRowsOfSheet_ReturnsLastRowIndexIsSourceRowIndex()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;
            const int destRowIndex = 3;

            // Act
            newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex);

            // Assert
            Assert.AreEqual(destRowIndex, newSheet.LastRowNum);
        }

        [Test]
        public void CopyRow_4Params_FromSourceRowIndexIsOutRange_ReturnsRowIsNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, Arg.Any<int>());

            // Assert
            Assert.IsNull(newRow);
        }

        [Test]
        public void CopyRow_4Params_FromValidSourceRowIndex_ReturnsRowIsNotNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 2;
            const int destRowIndex = 0;

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex);

            // Assert
            Assert.IsNotNull(newRow);
        }

        [Test]
        public void CopyRow_5Params_FromSourceRowIndexLessThanNumberRowsOfSheet_ReturnsLastRowIndexIsSourceRowIndex()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;
            const int destRowIndex = 3;
            var cellIndexList = new List<int>();

            // Act
            newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex, cellIndexList);

            // Assert
            Assert.AreEqual(destRowIndex, newSheet.LastRowNum);
        }

        [Test]
        public void CopyRow_5Params_FromSourceRowIndexIsOutRange_ReturnsRowIsNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;
            var cellIndexList = new List<int>();

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, Arg.Any<int>(), cellIndexList);

            // Assert
            Assert.IsNull(newRow);
        }

        [Test]
        public void CopyRow_5Params_FromValidSourceRowIndexAndCellList_ReturnRowIsNotNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 2;
            const int destRowIndex = 3;
            var cellIndexList = new List<int> { 0, 1 };

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex, cellIndexList);

            // Assert
            Assert.IsNotNull(newRow);
        }

        [Test]
        public void CopyRow_6Params_FromSourceRowIndexLessThanNumberRowsOfSheet_ReturnsLastRowIndexIsSourceRowIndex()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;
            const int destRowIndex = 3;
            var cellIndexList = new List<int>();

            // Act
            newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex, cellIndexList, Arg.Any<ICellStyle>());

            // Assert
            Assert.AreEqual(destRowIndex, newSheet.LastRowNum);
        }

        [Test]
        public void CopyRow_6Params_FromSourceRowIndexIsOutRange_ReturnsRowIsNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;
            var cellIndexList = new List<int>();

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, Arg.Any<int>(), cellIndexList, Arg.Any<ICellStyle>());

            // Assert
            Assert.IsNull(newRow);
        }

        [Test]
        public void CopyRow_6Params_FromValidSourceRowIndexAndCellList_ReturnRowIsNotNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 2;
            const int destRowIndex = 3;
            var cellIndexList = new List<int> { 0, 1 };

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex, cellIndexList, Arg.Any<ICellStyle>());

            // Assert
            Assert.IsNotNull(newRow);
        }

        [Test]
        public void CopyRow_SourceRowIsNull_ReturnRowIsNull()
        {
            // Arrange
            var newSheet = _workbook.CreateSheet("NewSheet");
            const int sourceRowIndex = 10;
            const int destRowIndex = 3;
            var cellIndexList = new List<int> { 0, 1 };

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex, cellIndexList, Arg.Any<ICellStyle>());

            // Assert
            Assert.IsNull(newRow);
        }

        [Test]
        public void CopyRow_CellIsNotInIndexList_SetEmptyValueAndDefaultStyle()
        {
            // Arrange
            var newSheet = _workbook.CloneSheet(0);
            const int sourceRowIndex = 3;
            const int destRowIndex = 4;
            var cellIndexList = new List<int> { 0, 1 };
            var defaultCellStyle = new HSSFCellStyle(1, null, _workbook);

            // Act
            var newRow = newSheet.CopyRow(_sheet, sourceRowIndex, destRowIndex, cellIndexList, defaultCellStyle);

            // Assert
            Assert.True(
                newRow.Cells[2].StringCellValue?.Length == 0
                && newRow.Cells[2].CellStyle.Index == 1);
        }

        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 1)]
        public void InsertCopy_FromRowNumLessThanOrEqualSourceRowNum_ReturnsNewRowHasFirstCellValueEqualFirstCellValueOfSourceRow(
            int rowNum, int rowSourceNum)
        {
            // Act
            var newRow = _sheet.InsertCopy(rowNum, rowSourceNum);
            var sourceRow = _sheet.GetRow(rowSourceNum + 1);

            // Assert
            Assert.AreEqual(sourceRow.GetCell(0).StringCellValue, newRow.GetCell(0).StringCellValue);
        }

        [Test]
        public void CopyCell_FromSourceCellIsNull_ReturnsNullCell()
        {
            // Arrange
            ICell sourceCell = null;
            ICell destCell = null;

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.IsNull(destCell);
        }

        [Test]
        public void CopyCell_FromDestCellHasComment_ReturnsDestCellCommentIsDestCellComment()
        {
            // Arrange
            const string commentContent = "This is a comment for test";
            var comment = Substitute.For<IComment>();
            comment.String.Returns(new HSSFRichTextString(commentContent));

            var sourceCell = _sheet.GetCell(3, 2);
            sourceCell.CellComment = comment;

            var destCell = Substitute.For<ICell>();
            destCell.CellComment = Substitute.For<IComment>();

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.AreEqual(commentContent, destCell.CellComment.String.ToString());
        }

        [Test]
        public void CopyCell_FromDestCellCommentIsNull_ReturnsDestCellCommentIsNull()
        {
            // Arrange
            var sourceCell = _sheet.GetCell(3, 2);
            var destCell = Substitute.For<ICell>();
            destCell.CellComment = null;

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.IsNull(destCell.CellComment);
        }

        [Test]
        public void CopyCell_FromDestCellHasHyperlink_ReturnsDestCellHyperlinkIsDestCellHyperlink()
        {
            // Arrange
            const string address = "http://google.com";
            var hyperlink = new HSSFHyperlink(HyperlinkType.Url)
            {
                Address = address
            };

            var sourceCell = _sheet.GetCell(3, 2);
            sourceCell.Hyperlink = hyperlink;

            ICell destCell = Substitute.For<ICell>();
            destCell.Hyperlink = Substitute.For<IHyperlink>();

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.AreEqual(address, destCell.Hyperlink.Address);
        }

        [Test]
        public void CopyCell_FromDestCellHyperlinkIsNull_ReturnsDestCellHyperlinkIsNull()
        {
            // Arrange
            var sourceCell = _sheet.GetCell(3, 2);

            var destCell = Substitute.For<ICell>();
            destCell.Hyperlink = null;

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.IsNull(destCell.Hyperlink);
        }

        [Test]
        public void CopyCell_FromSourceCellTypeIsBlank_ReturnsDestCellHasStringValueEqualToSourceCellStringValue()
        {
            // Arrange
            var sourceCell = _sheet.GetCell(5, 0);
            var destCell = Substitute.For<ICell>();
            destCell.Hyperlink = null;

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.AreEqual(sourceCell.StringCellValue, destCell.StringCellValue);
        }

        [Test]
        public void CopyCell_FromSourceCellTypeIsNumeric_ReturnsDestCellHasNumericValueEqualToSourceCellNumericValue()
        {
            // Arrange
            var sourceCell = _sheet.GetCell(5, 4);
            var destCell = _sheet.GetCell(7, 4);

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.AreEqual(sourceCell.NumericCellValue, destCell.NumericCellValue);
        }

        [Test]
        public void CopyCell_FromSourceCellTypeIsString_ReturnsDestCellHasStringValueEqualToSourceCellStringValue()
        {
            // Arrange
            var sourceCell = _sheet.GetCell(5, 1);
            var destCell = _sheet.GetCell(7, 1);

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.AreEqual(sourceCell.RichStringCellValue.ToString(), destCell.RichStringCellValue.ToString());
        }

        [Test]
        public void CopyCell_FromSourceCellTypeIsBoolean_ReturnsDestCellHasBooleanValueEqualToSourceCellBooleanValue()
        {
            // Arrange
            var sourceCell = _sheet.GetCell(5, 2);
            var destCell = _sheet.GetCell(7, 0);

            // Act
            _sheet.CopyCell(sourceCell, destCell);

            // Assert
            Assert.AreEqual(sourceCell.BooleanCellValue.ToString(), destCell.BooleanCellValue.ToString());
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void GetCell_FromRowIndexAndColIndex_ReturnsSameAsOldGetCellFunc(int rowIndex, int colIndex)
        {
            // Act
            var cell = _sheet.GetCell(rowIndex, colIndex);

            // Assert
            var expectedCell = _sheet.GetRow(rowIndex).GetCell(colIndex);
            Assert.AreEqual(expectedCell, cell);
        }

        [Test]
        public void GetCell_FromRowIndexIsOutRange_ThrowsException()
        {
            // Arrange
            var rowIndex = _sheet.LastRowNum + 10;

            // Act
            void testDelegate() => _sheet.GetCell(rowIndex, Arg.Any<int>());

            // Assert
            Assert.Throws<NullReferenceException>(testDelegate);
        }

        [Test]
        public void GetCell_FromInvalidRowIndexAndColIndexIsOutRange_ReturnsNull()
        {
            // Arrange
            const int colIndex = 100;

            // Act
            var cell = _sheet.GetCell(0, colIndex);

            // Assert
            Assert.IsNull(cell);
        }

        [TestCase(1, 1, 3, 3)]
        [TestCase(1, 1, 1, 3)]
        [TestCase(1, 1, 1, 20)]
        [TestCase(2, 0, 10, 20)]
        public void MergeCells_FromFirstRowNLastRowNFirstColNLastCol_ReturnsTrue(
            int firstRow,
            int firstCol,
            int lastRow,
            int lastCol)
        {
            // Act
            var result = _sheet.MergeCells(firstRow, firstCol, lastRow, lastCol);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void DeleteRow_FromRowIndex_ThatRowIsNull(int rowIndex)
        {
            // Act
            _sheet.DeleteRow(rowIndex);

            // Assert
            Assert.IsNull(_sheet.GetRow(rowIndex));
        }
    }
}