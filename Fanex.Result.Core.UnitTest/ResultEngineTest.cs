namespace Fanex.Result.Core.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Exception;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.UnitTest.Stub;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Apply unit testing for ResultEngineTest class.
    /// </summary>
    [TestFixture]
    public class ResultEngineTest
    {
        private const string ARGUEMENTMESOUTRIGHT = "DTO ({0}) is invalid";
        private Sportbook _sportsbookInfo = null;
        private IList<Sportbook> _sportsbookList = new List<Sportbook>();
        private IList<OutRight> _outrightList = new List<OutRight>();
        private IList<Parlay> _parlayList = new List<Parlay>();

        /// <summary>
        /// Setup data.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _sportsbookInfo = new Sportbook
            {
                AwayId = 1,
                HomeId = 2,
                BetStatus = string.Empty,
                LeagueId = 1,
                MatchId = 1,
                EventStatus = string.Empty,
                FinalAwayScore = 1,
                HtAwayScore = 2,
                Color = string.Empty,
                FinalHomeScore = 1,
                HtHomeScore = 2,
                EventDate = DateTime.Now,
                IsNeutral = false,
                IsPending = false,
                KickOffTime = DateTime.Now,
                MatchCode = "01",
                GoalRecord = string.Empty,
                ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                IsHTCal = true,
                Calculate = true,
                RefNo = "12"
            };

            _sportsbookList.Add(_sportsbookInfo);

            var outright = new OutRight
            {
                EventDate = DateTime.Now,
                SportType = (int)SportType.Soccer,
                LeagueId = 1,
                TeamId = 1,
                Status = string.Empty
            };
            _outrightList.Add(outright);

            var parlay = new Parlay
            {
                SportId = (int)SportType.Soccer,
                LeagueId = 1,
                Sportbook = _sportsbookList
            };
            _parlayList.Add(parlay);
        }

        #region HtmlParser

        /// <summary>
        /// Testing for render html result. When result setting or data list parameter is null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ObjectNullException), ExpectedMessage = "The data is null or setting is null")]
        public void HtmlParser_ResultSettingOrDataListIsNull_ThrowsException()
        {
            List<Sportbook> dataList = null;
            ResultSetting setting = null;
            ResultEngine.HtmlParser(dataList, setting);
        }

        /// <summary>
        /// Testing for render html result. When Sport Type is invalid or DTO name doesn't match with sportType.
        /// </summary>
        [Test]
        public void HtmlParser_SportTypeOrDTONameInvalid_ThrowsException()
        {
            var setting = new ResultSetting
            {
                SportType = (SportType)(-1),
            };
            var expected = "Sport Type is invalid or DTO";

            // ACT
            var exception = Assert.Catch<Exception>(() => ResultEngine.HtmlParser(_sportsbookList, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Sport Type is invalid or DTO name doesn't match with sportType.");
        }

        /// <summary>
        /// Testing for render html result. When setting sports book, popup is false.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsSportsbookAndDataListIsNotNull_ReturnHtmlValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsPopUp = false
            };

            var result = ResultEngine.HtmlParser(_sportsbookList, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for render html result. When setting outright is true.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsOutrightAndDataListIsNotNull_ReturnHtmlValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsPopUp = false,
                IsOutRight = true
            };

            var result = ResultEngine.HtmlParser<OutRight>(_outrightList, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for render html result. When setting parlay popup is true.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsParlayPopupAndDataListIsNotNull_ReturnHtmlValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsParlayPopUp = true
            };

            var result = ResultEngine.HtmlParser<Parlay>(_parlayList, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for render html result. When setting Popup is true.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsPopupAndDataListIsNotNull_ReturnHtmlValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsPopUp = true
            };

            var result = ResultEngine.HtmlParser(_sportsbookList, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        #endregion HtmlParser

        #region ExcelParser

        /// <summary>
        /// Testing for ExcelParser. When result setting or data list parameter is null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ObjectNullException), ExpectedMessage = "The data is null or setting is null")]
        public void ExcelParser_ResultSettingOrDataListIsNull_ThrowsException()
        {
            List<Sportbook> dataList = null;
            ResultSetting setting = null;

            ResultEngine.ExcelParser(dataList, setting);
        }

        /// <summary>
        /// Testing for ExcelParser. When Sport Type is invalid or DTO name doesn't match with sportType.
        /// </summary>
        [Test]
        public void ExcelParser_SportTypeOrDTONameInvalid_ThrowsException()
        {
            var setting = new ResultSetting
            {
                SportType = (SportType)(-1),
            };
            var expected = "Sport Type is invalid or DTO";

            // ACT
            var exception = Assert.Catch<Exception>(() => ResultEngine.ExcelParser(_sportsbookList, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Sport Type is invalid or DTO name doesn't match with sportType.");
        }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        /// <summary>
        /// We need to implement method testing by Stub. When setting Outright is true.
        /// </summary>
        [Test]
        public void ExcelParser_ResultSettingIsOutrightDTOIsOutrightAndDataListIsNotNull_ReturnMemoryStream()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsOutRight = true
            };

            IBuilder builder = new BuilderStub();
            ResultEngine.Reset(builder);

            var result = ResultEngine.ExcelParser(_outrightList, setting);

            Assert.IsInstanceOf<MemoryStream>(result);
        }

        /// <summary>
        /// We need to implement method testing by Stub.
        /// When the parameters are outright setting, DTO Name doesn't match with sportType.
        /// </summary>
       [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [Test]
        public void ExcelParser_ResultSettingIsOutrightDTOIsNotOutrightAndDataListIsNotNull_ThrowsException()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsOutRight = true
            };
            var expected = string.Format(ARGUEMENTMESOUTRIGHT, "Sportbook");

            IBuilder builder = new BuilderStub();
            ResultEngine.Reset(builder);

            // ACT
            var exception = Assert.Catch<Exception>(() => ResultEngine.ExcelParser(_sportsbookList, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Sport Type is invalid or DTO name doesn't match with sportType.");
        }

        /// <summary>
        /// When result setting is IsPopUp or ParlayPopUp, DTO Name not OutRight and data list.
        /// </summary>
        [Test]
        public void ExcelParser_ResultSettingIsPopUpOrIsParlayPopUpAndDataListIsNotNull_ReturnNull()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsParlayPopUp = true,
                IsPopUp = true
            };

            var result = ResultEngine.ExcelParser(_sportsbookList, setting);

            Assert.IsNull(result);
        }

        /// <summary>
        /// We need to implement method testing by Stub.
        /// </summary>
        [Test]
        public void ExcelParser_ResultSettingSportsbookAndDataListIsNotNull_ReturnMemoryStream()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
            };

            IBuilder builder = new BuilderStub();
            ResultEngine.Reset(builder);

            var result = ResultEngine.ExcelParser(_sportsbookList, setting);

            Assert.IsInstanceOf<MemoryStream>(result);
        }

        #endregion ExcelParser

        #region JsonParser

        /// <summary>
        /// Testing for JSONParser method. When result setting or data list parameter is null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ObjectNullException), ExpectedMessage = "The data is null or setting is null")]
        public void JsonParser_ResultSettingOrDataListIsNull_ThrowsException()
        {
            List<Sportbook> dataList = null;
            ResultSetting setting = null;

            ResultEngine.JsonParser(dataList, setting);
        }

        /// <summary>
        /// Testing for JSONParser method. When Sport Type is invalid or DTO name doesn't match with sportType.
        /// </summary>
        [Test]
        public void JsonParser_SportTypeOrDTONameInvalid_ThrowsException()
        {
            var setting = new ResultSetting
            {
                SportType = (SportType)(-1),
            };
            var expected = "Sport Type is invalid or DTO";

            // ACT
            var exception = Assert.Catch<Exception>(() => ResultEngine.JsonParser(_sportsbookList, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Sport Type is invalid or DTO name doesn't match with sportType.");
        }

        /// <summary>
        /// Testing for JSONParser method. When Setting Outright is true and DTO name doesn't match with sportType.
        /// </summary>
        [Test]
        public void JsonParser_ResultSettingIsOutrightAndDTONameInvalid_ThrowsException()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsOutRight = true
            };
            var expected = "DTO (Sportbook) is invalid";

            // ACT
            var exception = Assert.Catch<Exception>(() => ResultEngine.JsonParser(_sportsbookList, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "DTO name doesn't match with sportType. When setting result is Outright.");
        }

        /// <summary>
        /// Testing for JSONParser method. When Setting Outright is true and DTO name does match with sportType.
        /// </summary>
        [Test]
        public void JsonParser_ResultSettingIsOutrightAndDTONameValid_ReturnValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsOutRight = true
            };

            // ACT
            var result = ResultEngine.JsonParser(_outrightList, setting);

            // Assert
            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for JSONParser method. When Setting Parlay Popup is true and DTO name doesn't match with sportType.
        /// </summary>
        [Test]
        public void JsonParser_ResultSettingIsParlayAndDTONameInvalid_ThrowsException()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsParlayPopUp = true
            };
            var expected = "Sport Type is invalid or DTO (Sportbook) doesn't match with sportType";

            // ACT
            var exception = Assert.Catch<Exception>(() => ResultEngine.JsonParser(_sportsbookList, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "DTO name doesn't match with sportType. When setting result is ParlayPopup.");
        }

        /// <summary>
        /// Testing for JSONParser method. When Setting Parlay Popup is true and DTO name does match with sportType.
        /// </summary>
        [Test]
        public void JsonParser_ResultSettingIsParlayAndDTONameValid_ReturnValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsParlayPopUp = true
            };

            // ACT
            var result = ResultEngine.JsonParser(_parlayList, setting);

            // Assert
            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for JSONParser method. When Setting Parlay Popup and Outright is false.
        /// </summary>
        [Test]
        public void JsonParser_ResultSettingIsParlayAndIsOutrightFalse_ReturnSportsbookValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
            };

            // ACT
            var result = ResultEngine.JsonParser(_sportsbookList, setting);

            // Assert
            Assert.IsNotNullOrEmpty(result);
        }

        #endregion JsonParser

        [TearDown]
        public void TearDown()
        {
            _sportsbookInfo = null;
        }
    }
}