namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System;
    using System.IO;
    using Base.Extension;
    using NPOI.HSSF.UserModel;
    using NUnit.Framework;

    [TestFixture]
    public class HSSFWorkbookExtensionTests
    {
        private HSSFWorkbook _workbook;

        [SetUp]
        public void SetUp()
        {
            var templatePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "../../Data/Soccer.xls";
            var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
            _workbook = new HSSFWorkbook(stream);
        }

        [Test]
        public void CloneSheet_FromTemplatePathAndSourceSheetName_ReturnsSheetHasRightName()
        {
            // Arrange
            var sourceSheetName = "Template";
            var newSheetName = "Sport";

            // Act
            var newSheet = HSSFWorkbookExtension.CloneSheet(_workbook, sourceSheetName, newSheetName);

            // Arrange
            Assert.AreEqual(newSheetName, newSheet.SheetName);
        }

        [Test]
        public void RemoveSheet_FromInvalidSheetName_NumberOfSheetsDoesNotChange()
        {
            // Arrange
            var sheetName = "Hello World";

            // Act
            _workbook.RemoveSheet(sheetName);

            // Assert
            Assert.AreEqual(1, _workbook.NumberOfSheets);
        }

        [Test]
        public void RemoveSheet_FromValidSheetName_NumberOfSheetsIsZero()
        {
            // Arrange
            var sheetName = "Template";

            // Act
            _workbook.RemoveSheet(sheetName);

            // Assert
            Assert.AreEqual(0, _workbook.NumberOfSheets);
        }
    }
}