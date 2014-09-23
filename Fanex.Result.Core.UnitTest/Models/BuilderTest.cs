namespace Fanex.Result.Core.UnitTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.UnitTest.Stub;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Apply unit testing for BuilderTest class.
    /// </summary>
    [TestFixture]
    public class BuilderTest
    {
        private Builder _builder;
        private Sportbook _sportsBookInfo = null;
        private IList<Sportbook> _sportsbookList = new List<Sportbook>();
        private IList<OutRight> _outrightList = new List<OutRight>();
        private IList<Parlay> _parlayList = new List<Parlay>();
        private string _language = "en-US";

        /// <summary>
        /// Setup data.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _builder = new Builder();
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

            _sportsbookList.Add(_sportsBookInfo);

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

        #region Html Builder

        /// <summary>
        /// Render html result.
        /// </summary>
        [Test]
        public void HtmlBuilder_InputSportTypeAndLanguage_ReturnHtmlValue()
        {
            var sportType = SportType.Soccer;

            var expected = _builder.HtmlBuilder<Sportbook>(_sportsbookList, sportType, _language);
            Assert.IsNotNullOrEmpty(expected);
        }

        /// <summary>
        /// Render html for popup page when Outright is false.
        /// </summary>
        [Test]
        public void PopUpHtmlBuilder_InputSportTypeLanguageAndOutrightIsFalse_ReturnHtmlValue()
        {
            var sportType = SportType.Soccer;
            var isOutRight = false;

            var expected = _builder.PopUpHtmlBuilder<Sportbook>(_sportsbookList, sportType, isOutRight, _language);
            Assert.IsNotNullOrEmpty(expected);
        }

        /// <summary>
        /// Render html for popup page when Outright is true.
        /// </summary>
        [Test]
        public void PopUpHtmlBuilder_InputSportTypeLanguageAndOutrightIsTrue_ReturnHtmlValue()
        {
            var sportType = SportType.Soccer;
            var isOutRight = true;

            var expected = _builder.PopUpHtmlBuilder<OutRight>(_outrightList, sportType, isOutRight, _language);
            Assert.IsNotNullOrEmpty(expected);
        }

        /// <summary>
        /// Render html for popup parlay page.
        /// </summary>
        [Test]
        public void PopUpParalyBuilder_InputParlayListAndLanguage_ReturnHtmlValue()
        {
            var listParlay = new List<Parlay>();

            var expected = _builder.PopUpParalyBuilder<Parlay>(listParlay, _language);
            Assert.IsNotNullOrEmpty(expected);
        }

        /// <summary>
        /// Render html for Outright page.
        /// </summary>
        [Test]
        public void OutRightHtmlBuilder_InputOutrightListAndLanguage_ReturnHtmlValue()
        {
            var expected = _builder.OutRightHtmlBuilder<OutRight>(_outrightList, _language);
            Assert.IsNotNullOrEmpty(expected);
        }

        #endregion Html Builder

        #region ExcelBuilder

        /// <summary>
        /// Testing for normal method export to excel.
        /// </summary>
        [Test]
        public void ExcelBuilder_ReturnMemoryStream()
        {
            var sportType = SportType.Soccer;

            IExcelParser excelParser = new ExcelParserStub();
            var excelBuilder = new Builder(excelParser);

            var result = excelBuilder.ExcelBuilder(_sportsbookList, sportType, _language, string.Empty, string.Empty);

            Assert.IsInstanceOf<MemoryStream>(result);
        }

        /// <summary>
        /// Testing for Outright method export to Excel.
        /// </summary>
        [Test]
        public void OutRightExcelBuilder_ReturnMemoryStream()
        {
            IExcelParser excelParser = new ExcelParserStub();
            var excelBuilder = new Builder(excelParser);

            var result = excelBuilder.OutRightExcelBuilder(_outrightList, _language, string.Empty, string.Empty);

            Assert.IsInstanceOf<MemoryStream>(result);
        }

        #endregion ExcelBuilder

        #region JsonBuilder

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        /// <summary>
        /// Testing for normal method to render JSON result.
        /// </summary>
        [Test]
        public void JSONSportTypeBuilder_ReturnString()
        {
            var sportType = SportType.Soccer;
            var excelBuilder = new Builder();

            var result = excelBuilder.JSONSportTypeBuilder(_sportsbookList, sportType, _language);

            Assert.IsNotNullOrEmpty(result);
        }
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        /// <summary>
        /// Testing for Parlay method to render JSON result.
        /// </summary>
        [Test]
        public void JSONParlayBuilder_ReturnString()
        {
            var excelBuilder = new Builder();

            var result = excelBuilder.JSONParlayBuilder(_parlayList, _language);

            Assert.IsNotNullOrEmpty(result);
        }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        /// <summary>
        /// Testing for Outright method to render JSON result.
        /// </summary>
        [Test]
        public void JSONOutRightBuilder_ReturnString()
        {
            var excelBuilder = new Builder();

            var result = excelBuilder.JSONOutRightBuilder(_outrightList, _language);

            Assert.IsNotNullOrEmpty(result);
        }

        #endregion JsonBuilder

        [TearDown]
        public void Teardown()
        {
            _builder = null;
        }
    }
}