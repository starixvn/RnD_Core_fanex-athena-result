namespace Fanex.Result.Core.Base.Tests.Exception
{
    using System.Collections.Generic;
    using Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    internal class ResultCoreExceptionTests
    {
        [Test]
        public void Message_Always_GetExactMessage()
        {
            // Arrange
            var exception = new ResultCoreException("Message");

            // Act
            var actualMessage = exception.Message;

            // Assert
            Assert.AreEqual("Result.Core Exception: Message  ", actualMessage);
        }

        [Test]
        public void Message_Always_GetExactMessageWithTemplateAndBinderModel()
        {
            // Arrange
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
            var exception = new ResultCoreException(
                "Message",
                new System.Exception("Test"),
                "templateName",
                new RazorBinderModel
                {
                    IsDetail = true,
                    VisibleMatchCodeIDColumn = true,
                    Data = new List<GroupData>
                    {
                        new GroupData
                        {
                            LeagueId = 1,
                            Data = new List<IResultViewModel>
                            {
                                new SoccerViewModel(new Sportsbook.DTO.Sportsbook
                                {
                                }, new ResultSetting
                                {
                                    LanguageCode = "en-US"
                                })
                            }
                        }
                    }
                });

            // Act
            var actualMessage = exception.Message;

            // Assert
            Assert.AreEqual(
                "Result.Core Exception: Message \r\nTemplate: templateName \r\nBinderModel: {\"Data\":[{\"LeagueId\":1,\"Data\":[{\"IsShowLiveScore\":false,\"PenaltyMatchDetail\":\"\",\"HomeId\":0,\"AwayId\":0,\"VisibleDetailedLink\":false,\"VisibleGameDetail\":false,\"IsShowFGHome\":false,\"IsShowFGAway\":false,\"IsShowLGHome\":false,\"IsShowLGAway\":false,\"IsShowedFirstHalfFGHome\":false,\"IsShowedFirstHalfFGAway\":false,\"IsShowedFirstHalfLGHome\":false,\"IsShowedFirstHalfLGAway\":false,\"HTHomeScore\":\"-\",\"FinalHomeScore\":\"-\",\"HTAwayScore\":\"-\",\"FinalAwayScore\":\"-\",\"LiveHomeScore\":\"-\",\"LiveAwayScore\":\"-\",\"GoalRecord\":\"\",\"VisibleGoalRecord\":false,\"DetailedResult\":\"\",\"HomeSet\":null,\"AwaySet\":null,\"KickOffTime\":\"01/01/0001<br/>12:00:00 AM\",\"LeagueId\":0,\"LeagueName\":\"\",\"EventStatus\":\"-\",\"VisibleMatchResult\":true,\"MatchCode\":\"\",\"MatchId\":\"0\",\"HomeTeam\":\"\",\"AwayTeam\":\"\",\"HomeWin\":\"\",\"AwayWin\":\"\",\"DateTimeFormat\":\"{0:MM/dd/yyyy<br/>hh:mm:ss tt}\",\"RefDataHelper\":{},\"GetSupportedLanguage\":\"en-US\",\"IsPending\":false,\"EventStatusBackground\":\"\",\"SportName\":\"\"}]}],\"IsPopUp\":false,\"VisiblePopUpTitle\":false,\"SportType\":0,\"VisibleMatchCodeIDColumn\":true,\"VisibleDetailedLink\":false,\"VisibleFreeHeader\":false,\"IsDetail\":true,\"VisibleMainResult\":true,\"VisibleDetailResult\":true}", actualMessage);
        }
    }
}