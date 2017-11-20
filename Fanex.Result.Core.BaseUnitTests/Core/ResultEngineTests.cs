namespace Fanex.Result.Core.Base.Tests.Core
{
    using System.Collections.Generic;
    using Base.Exception;
    using NUnit.Framework;
    using Result.Core;
    using TestUtils;
    using Sportsbook.DTO;

    [TestFixture]
    public class ResultEngineTests
    {
        private ResultSetting _resultSetting;
        private IList<Sportsbook> _dataList;
        private Sportsbook _data;

        [SetUp]
        public void Setup()
        {
            _resultSetting = new ResultSetting();
            _dataList = new List<Sportsbook>();
            _data = new Sportsbook();
        }

        [TestCase(SportType.Badminton)]
        [TestCase(SportType.VirtualSports)]
        [TestCase(SportType.BaseBall)]
        public void HtmlGameDetailParser_SportTypeIsNotSupportGameDetail_ThrowObjectUnSupportedException(SportType sportType)
        {
            // Arrange
            _resultSetting.SportType = sportType;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlGameDetailParser(new Sportsbook(), _resultSetting));

            // Assert
            Assert.Throws<ObjectUnSupportedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailParser_DataIsNull_ObjectNullException()
        {
            // Arrange
            _resultSetting.SportType = SportType.Soccer;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlGameDetailParser(null, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailParser_GameDetailsIsEmptyAndSportTypeIsTennis_ObjectNullException()
        {
            // Arrange
            _resultSetting.SportType = SportType.Tennis;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlGameDetailParser(_data, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailParser_LanguageCodeIsNotValid_ObjectUnSupportedException()
        {
            // Arrange
            _resultSetting.SportType = SportType.Soccer;
            _resultSetting.LanguageCode = " ";

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlGameDetailParser(_data, _resultSetting));

            // Assert
            Assert.Throws<ObjectUnSupportedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailParser_LanguageCodeIsValid_GetExpectedHtml()
        {
            // Arrange
            _resultSetting.SportType = SportType.Soccer;
            _resultSetting.LanguageCode = "34234";

            // Act
            var expectedHtml = ResultEngine.HtmlGameDetailParser(_data, _resultSetting);

            // Assert
            Assert.IsNotNull(expectedHtml);
        }

        [Test]
        public void HtmlGameDetailParser_ValidData_GetExpectedHtml()
        {
            // Arrange
            _resultSetting.SportType = SportType.Soccer;
            _resultSetting.LanguageCode = "";

            // Act
            var expectedHtml = ResultEngine.HtmlGameDetailParser(_data, _resultSetting);

            // Assert
            Assert.IsNotNull(expectedHtml);
        }

        [Test]
        public void HtmlParser_DataListIsNull_ThrowObjectNullException()
        {
            //  Arrange && Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlParser<Sportsbook>(null, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void HtmlParser_SettingLanguageCodeIsWhiteSpace_ThrowObjectUnSupportedException()
        {
            // Arrange
            _resultSetting.LanguageCode = " ";

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlParser(_dataList, _resultSetting));

            // Assert
            Assert.Throws<ObjectUnSupportedException>(testDelegate);
        }

        [Test]
        public void HtmlParser_IsOutright_DTOIsNotOutright_ThrowObjectNullException()
        {
            // Arrange
            _resultSetting.LanguageCode = "";
            _resultSetting.IsOutright = true;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlParser(_dataList, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void HtmlParser_IsOutright_DTOIsOutright_ReturnExpectedHtml()
        {
            // Arrange
            _resultSetting.LanguageCode = "";
            _resultSetting.IsOutright = true;
            var dataList = new List<Outright>();

            // Act
            var actualHtml = ResultEngine.HtmlParser(dataList, _resultSetting);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("Outright.html");
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void HtmlParser_IsParlay_DTOIsNotParlay_ThrowObjectNullException()
        {
            // Arrange
            _resultSetting.LanguageCode = "";
            _resultSetting.IsParlayPopUp = true;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlParser(_dataList, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void HtmlParser_IsParlay_DTOIsParlay_ReturnExpectedHtml()
        {
            // Arrange
            _resultSetting.LanguageCode = "";
            _resultSetting.IsParlayPopUp = true;
            var dataList = new List<Parlay> { new Parlay { Sportbook = _dataList } };

            // Act
            var actualHtml = ResultEngine.HtmlParser(dataList, _resultSetting);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("Parlay.html");
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [TestCase(12323)]
        [TestCase(255)]
        public void HtmlParser_SportTypeIsNotValidWithDTO_RenderSoccerHtml(int sportType)
        {
            // Arrange
            _resultSetting.LanguageCode = "en";
            _resultSetting.SportType = (SportType)sportType;

            // Act
            var actualHtml = ResultEngine.HtmlParser(_dataList, _resultSetting);

            // Assert
            var expectedHtml = TemplateFileUtils.GetHtmlContentFromTestTemplate("Soccer.html");
            Assert.AreEqual(expectedHtml, TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void HtmlParser_ProductAssemblyIsNull_ThrowResultCoreException()
        {
            // Arrange
            _resultSetting.LanguageCode = "en";
            _resultSetting.SportType = SportType.Soccer;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.HtmlParser(new List<int>(), _resultSetting));

            // Assert
            Assert.Throws<ResultCoreException>(testDelegate);
        }

        [Test]
        public void ExcelParser_DataListIsNull_ThrowObjectNullException()
        {
            //  Arrange && Act
            var testDelegate = new TestDelegate(() => ResultEngine.ExcelParser<Sportsbook>(null, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void ExcelParser_SettingLanguageCodeIsWhiteSpace_ThrowObjectUnSupportedException()
        {
            // Arrange
            _resultSetting.LanguageCode = " ";

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.ExcelParser(_dataList, _resultSetting));

            // Assert
            Assert.Throws<ObjectUnSupportedException>(testDelegate);
        }

        [Test]
        public void ExcelParser_IsOutright_DTOIsNotOutright_ThrowObjectNullException()
        {
            // Arrange
            _resultSetting.LanguageCode = "";
            _resultSetting.IsOutright = true;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.ExcelParser(_dataList, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void ExcelParser_IsOutright_DTOIsOutright_ReturnExpectedExcel()
        {
            // Arrange
            _resultSetting.LanguageCode = "";
            _resultSetting.IsOutright = true;
            _resultSetting.SportType = SportType.Soccer;
            _resultSetting.ExcelTemplatePath = TemplateFileUtils.GetExcelTemplateFolderPath();
            var dataList = new List<Outright>();

            // Act
            var actualExcelMemoryStream = ResultEngine.ExcelParser(dataList, _resultSetting);

            // Assert
            Assert.NotNull(actualExcelMemoryStream);
        }

        [Test]
        public void ExcelParser_IsPopup_ReturnNull()
        {
            // Arrange
            _resultSetting.IsPopUp = true;

            // Act
            var actualExcelMemoryStream = ResultEngine.ExcelParser(_dataList, _resultSetting);

            // Assert
            Assert.Null(actualExcelMemoryStream);
        }

        [Test]
        public void ExcelParser_IsParlay_ReturnNull()
        {
            // Arrange
            _resultSetting.IsParlayPopUp = true;

            // Act
            var actualExcelMemoryStream = ResultEngine.ExcelParser(_dataList, _resultSetting);

            // Assert
            Assert.Null(actualExcelMemoryStream);
        }

        [TestCase(12323)]
        [TestCase(255)]
        public void ExcelParser_SportTypeIsNotValidWithDTO_ThrowObjectNullException(int sportType)
        {
            // Arrange
            _resultSetting.LanguageCode = "en";
            _resultSetting.SportType = (SportType)sportType;

            // Act
            var testDelegate = new TestDelegate(() => ResultEngine.ExcelParser(_dataList, _resultSetting));

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void ExcelParser_ValidData_ReturnNotNullExcelMemoryStream()
        {
            // Arrange
            _resultSetting.LanguageCode = "en";
            _resultSetting.SportType = SportType.Soccer;
            _resultSetting.ExcelTemplatePath = TemplateFileUtils.GetExcelTemplateFolderPath();

            // Act
            var actualExcelMemoryStream = ResultEngine.ExcelParser(_dataList, _resultSetting);

            // Assert
            Assert.NotNull(actualExcelMemoryStream);
        }
    }
}