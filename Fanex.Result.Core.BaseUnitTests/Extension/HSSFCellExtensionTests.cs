namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System;
    using System.IO;
    using Base.Extension;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class HSSFCellExtensionTests
    {
        private HSSFWorkbook _workbook;
        private ISheet _sheet;

        [SetUp]
        public void SetUp()
        {
            var templatePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "../../Data/Soccer.xls";
            var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
            _workbook = new HSSFWorkbook(stream);
            _sheet = _workbook.GetSheetAt(0);
        }

        [TestCase(1000.1)]
        [TestCase(-1.20)]
        public void SetCellValue_FromDecimalValue_CellNumericValueEqualToValue(decimal value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue(value);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetCellValue_FromBoolValue_CellBoolValueEqualToValue(bool value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue(value);

            // Assert
            Assert.AreEqual(value, cell.BooleanCellValue);
        }

        [Test]
        public void SetCellValue_FromDateTimeValue_CellDateTimeValueEqualToValue()
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);
            var dateTime = DateTime.Now;

            // Act
            cell.SetCellValue(dateTime);

            // Assert
            Assert.AreEqual(dateTime.ToString(), cell.DateCellValue.ToString());
        }

        [TestCase(1.2)]
        [TestCase(-1.20)]
        public void SetCellValue_FromDecimalObjectValue_CellNumbericValueEqualToValue(decimal value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [TestCase(1.2)]
        [TestCase(-1.20)]
        public void SetCellValue_FromDoubleObjectValue_CellNumbericValueEqualToValue(double value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [TestCase(100)]
        [TestCase(-120)]
        public void SetCellValue_FromIntObjectValue_CellNumbericValueEqualToValue(int value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [TestCase(102020202)]
        [TestCase(-1231231)]
        public void SetCellValue_FromLongObjectValue_CellNumbericValueEqualToValue(double value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [TestCase("Hello world")]
        [TestCase("This is string")]
        public void SetCellValue_FromStringObjectValue_CellNumbericValueEqualToValue(string value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, cell.StringCellValue);
        }

        [TestCase(102020202)]
        [TestCase(-1231231)]
        public void SetCellValue_FromLongValue_CellNumbericValueEqualToValue(double value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            cell.SetCellValue(value);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [Test]
        public void SetEmptyCell_FromDefaultStyleIsNull_ThrowException()
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);

            // Act
            TestDelegate testDelegate = () => cell.SetEmptyCell(Arg.Any<bool>(), Arg.Any<ICellStyle>());

            // Assert
            Assert.Throws<NullReferenceException>(testDelegate);
        }

        [TestCase("Hello world")]
        [TestCase("This is string")]
        public void SetEmptyCell_FromStringObject_StringCellValueEqualToValue(string value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);
            var defaultStyle = _sheet.GetCell(1, 0).CellStyle;

            // Act
            cell.SetEmptyCell(value, defaultStyle);

            // Assert
            Assert.AreEqual(value, cell.StringCellValue);
        }

        [TestCase(1000.1)]
        [TestCase(-1.20)]
        public void SetEmptyCell_FromDecimalObject_NumricCellValueEqualToValue(decimal value)
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);
            var defaultStyle = _sheet.GetCell(1, 0).CellStyle;

            // Act
            cell.SetEmptyCell(value, defaultStyle);

            // Assert
            Assert.AreEqual(value, cell.NumericCellValue);
        }

        [Test]
        public void SetEmptyCell_Always_CellHasDefaultStyle()
        {
            // Arrange
            var cell = _sheet.GetCell(0, 0);
            var defaultStyle = _sheet.GetCell(1, 0).CellStyle;

            // Act
            cell.SetEmptyCell(Arg.Any<bool>(), defaultStyle);

            // Assert
            Assert.AreEqual(defaultStyle, cell.CellStyle);
        }
    }
}