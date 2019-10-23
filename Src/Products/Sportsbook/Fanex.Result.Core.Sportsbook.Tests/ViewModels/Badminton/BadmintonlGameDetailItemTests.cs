namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.Badminton
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.Badminton;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;

    [TestFixture]
    public class BadmintonlGameDetailItemTests
    {
        private IRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsBookHelper;
        private Sportsbook _sportsBook;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _sportsBookHelper = Substitute.For<ISportsbookHelperWraper>();
            _sportsBook = new Sportsbook();
            _setting = new ResultSetting();
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void CreateGameDetailItem_HasMoreResultResources_FillResoureNameAndGame()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="7120103", HomeValue="A", SettlementStatusID=4, MatchId=1111}
            };
            _refDataHelper.GetBetTypeNameById("712", Arg.Any<string>(), "0103").Returns("Game 01 race to 3 points");
            _sportsBook.GameDetails = details;
            var model = new BadmintonResultDetail(_sportsBook, _setting, _refDataHelper, null);

            // Act
            var actual = model.GetDetailItems().First();

            // Assert
            Assert.AreEqual("Game 01 race to 3 points", actual.ResourceName);
            Assert.AreEqual("Game 1", actual.GameName);
            Assert.AreEqual("01", actual.GameNumber);
        }
    }
}
