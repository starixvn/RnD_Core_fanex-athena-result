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
#pragma warning disable S101 // Types should be named in PascalCase
    public class HSSFCellExtensionTests
    {
        private HSSFWorkbook _workbook;
        private ISheet _sheet;
        private ICell _cell;

        [SetUp]
        public void SetUp()
        {
            var templatePath = AppDomain.CurrentDomain.BaseDirectory + "/../../Data/Soccer.xls";
            var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
            _workbook = new HSSFWorkbook(stream);
            _sheet = _workbook.GetSheetAt(0);
            _cell = _sheet.GetCell(0, 0);
        }

        [TestCase(1000.1)]
        [TestCase(-1.20)]
        public void SetCellValue_FromDecimalValue_CellNumericValueEqualToValue(decimal value)
        {
            // Act
            _cell.SetCellValue(value);

            // Assert
            Assert.AreEqual(value, _cell.NumericCellValue);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetCellValue_FromBoolValue_CellBoolValueEqualToValue(bool value)
        {
            // Act
            _cell.SetCellValue(value);

            // Assert
            Assert.AreEqual(value, _cell.BooleanCellValue);
        }

        [Test]
        public void SetCellValue_FromDateTimeValue_CellDateTimeValueEqualToValue()
        {
            // Arrange
            var dateTime = DateTime.Now;

            // Act
            _cell.SetCellValue(dateTime);

            // Assert
            Assert.AreEqual(dateTime.ToString(), _cell.DateCellValue.ToString());
        }

        [TestCase(1.2)]
        [TestCase(-1.20)]
        [TestCase(100)]
        [TestCase(-120)]
        [TestCase(102020202)]
        [TestCase(-1231231)]
        [TestCase(102020202)]
        [TestCase(-1231231)]
        public void SetCellValue_FromDoubleObjectValue_CellNumbericValueEqualToValue(double value)
        {
            // Act
            _cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, _cell.NumericCellValue);
        }

        [TestCase("Hello world")]
        [TestCase("This is string")]
        public void SetCellValue_FromStringObjectValue_CellNumbericValueEqualToValue(string value)
        {
            // Act
            _cell.SetCellValue((object)value);

            // Assert
            Assert.AreEqual(value, _cell.StringCellValue);
        }

        [Test]
        public void SetCellValue_FromBoolValue_CellBoolValueEqualToValue()
        {
            // Act
            _cell.SetCellValue((object)false);

            // Assert
            Assert.IsFalse(_cell.BooleanCellValue);
        }

        [Test]
        public void SetCellValue_FromDateValue_CellDateValueEqualToValue()
        {
            // Act
            _cell.SetCellValue((object)DateTime.Today);

            // Assert
            Assert.AreEqual(DateTime.Today, _cell.DateCellValue);
        }

        [Test]
        public void SetEmptyCell_FromDefaultStyleIsNull_ThrowException()
        {
            // Act
            TestDelegate testDelegate = () => _cell.SetEmptyCell(Arg.Any<bool>(), Arg.Any<ICellStyle>());

            // Assert
            Assert.Throws<NullReferenceException>(testDelegate);
        }

        [TestCase("Hello world")]
        [TestCase("This is string")]
        public void SetEmptyCell_FromStringObject_StringCellValueEqualToValue(string value)
        {
            // Arrange
            var defaultStyle = _sheet.GetCell(1, 0).CellStyle;

            // Act
            _cell.SetEmptyCell(value, defaultStyle);

            // Assert
            Assert.AreEqual(value, _cell.StringCellValue);
        }

        [TestCase(1000.1)]
        [TestCase(-1.20)]
        public void SetEmptyCell_FromDecimalObject_NumricCellValueEqualToValue(decimal value)
        {
            // Arrange
            var defaultStyle = _sheet.GetCell(1, 0).CellStyle;

            // Act
            _cell.SetEmptyCell(value, defaultStyle);

            // Assert
            Assert.AreEqual(value, _cell.NumericCellValue);
        }

        [Test]
        public void SetEmptyCell_Always_CellHasDefaultStyle()
        {
            // Arrange
            var defaultStyle = _sheet.GetCell(1, 0).CellStyle;

            // Act
            _cell.SetEmptyCell(Arg.Any<bool>(), defaultStyle);

            // Assert
            Assert.AreEqual(defaultStyle, _cell.CellStyle);
        }
    }

#pragma warning restore S101 // Types should be named in PascalCase
}