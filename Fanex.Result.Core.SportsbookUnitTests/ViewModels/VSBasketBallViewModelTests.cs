namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;
    using Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail;
    using Fanex.Result.Core.Sportsbook.ViewModels.VSBasketBallDetail;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class VSBasketBallViewModelTests
    {
        private DTO.Sportsbook _sportsbook;
        private ResultSetting _setting;
        private IResultDetail<BasketBallGameDetailItem> _resultDetail;
        private ISportsbookRefDataHelper _sportsbookRefDataHelper;
        private CompareLogic _compareLogic;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new DTO.Sportsbook
            {
                HomeId = 1,
                AwayId = 2,
                GameDetails = new List<DTO.GameDetail>
                {
                    new DTO.GameDetail {ResourceId = GameResourceID.FirstHalfScore, HomeValue = "2", AwayValue = "4",  SettlementStatusID = 4},
                    new DTO.GameDetail {ResourceId = GameResourceID.FinalScore, HomeValue = "2", AwayValue = "4"  ,  SettlementStatusID = 4},
                    new DTO.GameDetail {ResourceId = VSBasketBallResourceID.FTRaceTo20Points, HomeValue = "h", SettlementStatusID = 4},
                }
            };
            _setting = new ResultSetting { LanguageCode = "en-US", VisibleDetailedLink = true };

            _resultDetail = Substitute.For<IResultDetail<BasketBallGameDetailItem>>();
            _sportsbookRefDataHelper = Substitute.For<ISportsbookRefDataHelper>();
            _sportsbookRefDataHelper.GetTeamName(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>()).Returns("HomeTeam");
            _sportsbookRefDataHelper.GetBetRadarMatchCodeFormat(Arg.Any<string>(), Arg.Any<string>()).Returns("matchinfo");

            _compareLogic = new CompareLogic();
            var resultDetailItems = new VSBasketBallResultDetail(_sportsbook, _setting, _sportsbookRefDataHelper).GetDetailItems();
            _resultDetail.GetDetailItems().Returns(resultDetailItems);
        }

        [Test]
        public void ResultDetailItems_Always_GetFromGameDetails()
        {
            // Arrange
            var expectedResultDetailItems = new VSBasketBallResultDetail(_sportsbook, _setting).GetDetailItems();
            _resultDetail.GetDetailItems().Returns(expectedResultDetailItems);

            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual(expectedResultDetailItems, viewModel.ResultDetailItems);
        }

        [Test]
        public void MoreResultDetailItems_Always_GetFromGameDetailsExceptFinalScore()
        {
            // Arrange
            var resultDetailItems = new VSBasketBallResultDetail(_sportsbook, _setting, _sportsbookRefDataHelper).GetDetailItems();
            _resultDetail.GetDetailItems().Returns(resultDetailItems);
            var expectedMoreResultDetailItems = new SafeList<BasketBallGameDetailItem>
            {
                new BasketBallGameDetailItem { ResourceID = VSBasketBallResourceID.FTRaceTo20Points, HomeValue= "HomeTeam", AwayValue = "-", IsHome = true, ResourceName = string.Empty }
            };

            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.True(_compareLogic.Compare(expectedMoreResultDetailItems, viewModel.MoreResultDetailItems).AreEqual);
        }

        [Test]
        public void HTHomeScore_Always_GetFromGameDetails()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual("2", viewModel.HTHomeScore);
        }

        [Test]
        public void HTAwayScore_Always_GetFromGameDetails()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual("4", viewModel.HTAwayScore);
        }

        [Test]
        public void FinalAwayScore_Always_GetFromGameDetails()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual("4", viewModel.FinalAwayScore);
        }

        [Test]
        public void FinalHomeScore_Always_GetFromGameDetails()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual("2", viewModel.FinalHomeScore);
        }

        [Test]
        public void MatchInfo_Always_GetFromMatchCode()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual("(matchinfo)", viewModel.MatchInfo);
        }

        [Test]
        public void VisibleDetailedLink_Always_GetFromMatchCode()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.True(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void VisibleGameDetail_Always_GetFromMatchCode()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.True(viewModel.VisibleGameDetail);
        }

        [Test]
        public void HomeId_Always_GetFromMatchCode()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual(1, viewModel.HomeId);
        }

        [Test]
        public void AwayId_Always_GetFromMatchCode()
        {
            // Act
            var viewModel = new VSBasketBallViewModel(_sportsbook, _setting, _sportsbookRefDataHelper, _resultDetail);

            // Assert
            Assert.AreEqual(2, viewModel.AwayId);
        }
    }
}