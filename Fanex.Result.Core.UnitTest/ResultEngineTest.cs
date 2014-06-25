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

    /// <summary>
    /// Apply unit testing for ResultEngineTest class.
    /// </summary>
    [TestFixture]
    public class ResultEngineTest
    {
        private const string ARGUEMENTMESOUTRIGHT = "DTO ({0}) is invalid";
        private Sportbook _sportsBookInfo = null;
        private IList<Sportbook> _listData = new List<Sportbook>();
        private ResultEngine _resultEngine = null;

        /// <summary>
        /// Setup data.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _sportsBookInfo = new Sportbook
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
                EventDate = DateTime.Parse("01-01-2014"),
                IsNeutral = false,
                IsPending = false,
                KickOffTime = DateTime.Parse("01-01-2014"),
                MatchCode = "01",
                GoalRecord = string.Empty,
                ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                IsHTCal = true,
                Calculate = true,
                RefNo = "12"
            };

            _listData.Add(_sportsBookInfo);
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
            ResultEngine.HtmlParser<Sportbook>(dataList, setting);
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
            var exception = Assert.Catch<Exception>(() => ResultEngine.HtmlParser<Sportbook>(_listData, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Sport Type is invalid or DTO name doesn't match with sportType.");
        }

        /// <summary>
        /// Testing for render html result. When setting sports book, popup is false and data list parameter is null.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsSportsbookAndDataListIsNotNull_ReturnHtmlValue()
        {
            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsPopUp = false
            };

            var result = ResultEngine.HtmlParser<Sportbook>(_listData, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for render html result. When setting outright and data list parameter is null.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsOutrightAndDataListIsNotNull_ReturnHtmlValue()
        {
            var _listOutright = new List<OutRight>();

            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsPopUp = false,
                IsOutRight = true
            };

            var result = ResultEngine.HtmlParser<OutRight>(_listOutright, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for render html result. When setting parlay and data list parameter is null.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsParlayPopupAndDataListIsNotNull_ReturnHtmlValue()
        {
            var _listParlay = new List<Parlay>();

            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsParlayPopUp = true
            };

            var result = ResultEngine.HtmlParser<Parlay>(_listParlay, setting);

            Assert.IsNotNullOrEmpty(result);
        }

        /// <summary>
        /// Testing for render html result. When setting Popup and data list parameter is null.
        /// </summary>
        [Test]
        public void HtmlParser_ResultSettingIsPopupAndDataListIsNotNull_ReturnHtmlValue()
        {
            var _listParlay = new List<Sportbook>();

            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsPopUp = true
            };

            var result = ResultEngine.HtmlParser<Sportbook>(_listParlay, setting);

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
            _resultEngine = new ResultEngine();

            _resultEngine.ExcelParser<Sportbook>(dataList, setting);
        }

        /// <summary>
        /// Testing for ExcelParser. When Sport Type is invalid or DTO name doesn't match with sportType.
        /// </summary>
        [Test]
        public void ExcelParser_SportTypeOrDTONameInvalid_ThrowsException()
        {
            _resultEngine = new ResultEngine();
            var setting = new ResultSetting
            {
                SportType = (SportType)(-1),
            };
            var expected = "Sport Type is invalid or DTO";

            // ACT
            var exception = Assert.Catch<Exception>(() => _resultEngine.ExcelParser<Sportbook>(_listData, setting));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Sport Type is invalid or DTO name doesn't match with sportType.");
        }

        /// <summary>
        /// We need to implement method testing by Stub. When outright setting and data list parameter is null.
        /// </summary>
        [Test]
        public void ExcelParser_ResultSettingIsOutrightDTOIsOutrightAndDataListIsNotNull_ReturnMemoryStream()
        {
            var _listOutright = new List<OutRight>();

            var setting = new ResultSetting
            {
                SportType = SportType.Soccer,
                IsOutRight = true
            };

            IBuilder builder = new BuilderStub();
            _resultEngine = new ResultEngine(builder);

            var result = _resultEngine.ExcelParser<OutRight>(_listOutright, setting);

            Assert.IsInstanceOf<MemoryStream>(result);
        }

        /// <summary>
        /// We need to implement method testing by Stub.
        /// When the parameters are outright setting, DTO Name not OutRight and data list.
        /// </summary>
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
            _resultEngine = new ResultEngine(builder);

            // ACT
            var exception = Assert.Catch<Exception>(() => _resultEngine.ExcelParser<Sportbook>(_listData, setting));

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

            _resultEngine = new ResultEngine();

            var result = _resultEngine.ExcelParser<Sportbook>(_listData, setting);

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
            _resultEngine = new ResultEngine(builder);

            var result = _resultEngine.ExcelParser<Sportbook>(_listData, setting);

            Assert.IsInstanceOf<MemoryStream>(result);
        }

        #endregion ExcelParser

        [TearDown]
        public void TearDown()
        {
            _sportsBookInfo = null;
        }
    }
}