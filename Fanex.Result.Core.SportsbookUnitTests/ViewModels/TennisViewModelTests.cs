namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DataDomain;
    using DTO;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;
    using Sportsbook.ViewModels.TennisDetail;

    [TestFixture]
    public class TennisViewModelTests
    {
        private Sportsbook _sportsbook;
        private ResultSetting _setting;
        private CompareLogic _comparer;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new Sportsbook();
            _setting = new ResultSetting();
            _comparer = new CompareLogic();
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan4_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new TennisViewModel(_sportsbook, _setting);
            var index = 7;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndGameDetailsDoesNotHaveAnyElement_ReturnsHyphen()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>();
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(5, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndFirstElementOfGameDetailsHasResourceIdNotEqualToIndexPlus1_ReturnsHyphen()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>();
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(5, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(true, "10")]
        [TestCase(false, "12")]
        public void GetHomeAwaySet_FromIndexLessThan5AndFirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndSettlementStatusIDIsRunning_ReturnsExpectedSet(
            bool isHome,
            string expectedSet)
        {
            // Arrange
            var index = 3;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RunningSettlementStatusID
                }
            }; ;

            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, isHome);

            // Assert
            Assert.AreEqual(expectedSet, homeAwaySet);
        }

        [TestCase(true, "10")]
        [TestCase(false, "12")]
        public void GetHomeAwaySet_FromIndexLessThan5AndFirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndSettlementStatusIDIsCompleted_ReturnsExpectedSet(
            bool isHome,
            string expectedSet)
        {
            // Arrange
            var index = 3;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                }
            };

            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, isHome);

            // Assert
            Assert.AreEqual(expectedSet, homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5AndFirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndSettlementStatusIDIsRefund_ReturnsRefund()
        {
            // Arrange
            var index = 3;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RefundSettlementStatusID
                }
            };

            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("Refund", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5FirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndAndSettlementStatusIDIsRefundHT_ReturnsHTRefund()
        {
            // Arrange
            var index = 3;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RefundHTSettlementStatusID
                }
            };

            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("Refund HT", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5FirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndAndSettlementStatusIDIsRefundFT_ReturnsRefundFT()
        {
            // Arrange
            var index = 3;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RefundFTSettlementStatusID
                }
            };

            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("Refund FT", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5FirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndAndSettlementStatusIDIsOtherCases_ReturnsHyphen()
        {
            // Arrange
            var index = 3;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = 100
                }
            };

            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void VisibleSet_FromBetIDIsNotInteger_ThrowsException()
        {
            // Arrange
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            TestDelegate testDelegate = () => viewModel.VisibleSet(0);

            // Assert
            Assert.Throws<ArgumentNullException>(testDelegate);
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void VisibleSet_FromSetIsOutRange1To5_ReturnsFalse(int set)
        {
            // Arrange
            _sportsbook.BetID = "1";
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var visibleSet = viewModel.VisibleSet(set);

            // Assert
            Assert.IsFalse(visibleSet);
        }

        [TestCase(1, "80102")]
        [TestCase(2, "80205")]
        [TestCase(3, "80310")]
        [TestCase(4, "80412")]
        [TestCase(5, "80555")]
        public void VisibleSet_FromSetIsFrom1To5AndBetIDIsInRangeOfSet_ReturnsTrue(int set, string betId)
        {
            // Arrange
            _sportsbook.BetID = betId;
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var visibleSet = viewModel.VisibleSet(set);

            // Assert
            Assert.IsTrue(visibleSet);
        }

        [TestCase(1, "80100")]
        [TestCase(2, "80214")]
        [TestCase(3, "80410")]
        [TestCase(4, "80499")]
        [TestCase(5, "80099")]
        public void VisibleSet_FromSetIsFrom1To5AndBetIDIsOutRangeOfSet_ReturnsFalse(int set, string betId)
        {
            // Arrange
            _sportsbook.BetID = betId;
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Act
            var visibleSet = viewModel.VisibleSet(set);

            // Assert
            Assert.IsFalse(visibleSet);
        }

        [Test]
        public void EventStatus_GameDetailsExistMatchResultRefund_ReturnRefund()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail { ResourceId = TennisBetID.TotalSet, SettlementStatusID = EventStatusID.RefundSettlementStatusID }
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("Refund", viewModel.EventStatus);
        }

        [TestCase("Running", false, true, "Running")]
        [TestCase("Running", true, true, "Pending")]
        [TestCase("Internal", false, true, "Internal")]
        [TestCase("Internal", false, true, "Internal")]
        [TestCase("Internal", false, true, "Internal")]
        [TestCase("Internal", false, false, "Running")]
        [TestCase("Completed", false, false, "Completed")]
        public void EventStatus_GameDetailsNotExistMatchResultRefund_ReturnEventStatus(string status, bool isPending, bool visibleInternal, string expectedStatus)
        {
            // Arrange
            _sportsbook.EventStatus = status;
            _sportsbook.IsPending = isPending;
            _setting.VisibleInternalEventStatus = visibleInternal;

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedStatus, viewModel.EventStatus);
        }

        [Test]
        public void TotalGameHome_SetIsNull_ReturnHyphen()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> { };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.TotalGameHome);
        }

        [TestCase(EventStatusID.RefundFTSettlementStatusID, "Refund FT")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "Refund HT")]
        [TestCase(EventStatusID.RefundSettlementStatusID, "Refund")]
        [TestCase(EventStatusID.RunningSettlementStatusID, "1")]
        [TestCase(10, "-")]
        public void TotalGameHome_SetStatusIsNotCompleted_ReturnBySettlementStatusId(int settlementStatusId, string expectedResult)
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1" , SettlementStatusID = settlementStatusId }
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.TotalGameHome);
        }

        [Test]
        public void TotalGameHome_SetStatusIsCompleted_ReturnTotalGameHomeValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "01" ,  HomeValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "02" ,  HomeValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "03" ,  HomeValue = "1" , SettlementStatusID = EventStatusID.RefundFTSettlementStatusID },
                  new GameDetail { ResourceId = "04" ,  HomeValue = "1" , SettlementStatusID = EventStatusID.RefundSettlementStatusID }
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("2", viewModel.TotalGameHome);
        }

        [Test]
        public void TotalGameHome_TotalGameValueIsNotGreaterThanZero_ReturnHyphen()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "01" ,  HomeValue = "0" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "02" ,  HomeValue = "0" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "03" ,  HomeValue = "1" , SettlementStatusID = EventStatusID.RefundFTSettlementStatusID },
                  new GameDetail { ResourceId = "04" ,  HomeValue = "1" , SettlementStatusID = EventStatusID.RefundSettlementStatusID }
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.TotalGameHome);
        }

        [Test]
        public void TotalGameAway_SetStatusIsCompleted_ReturnTotalGameAwayValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "01" ,  AwayValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "02" ,  AwayValue = "2" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "03" ,  AwayValue = "1" , SettlementStatusID = EventStatusID.RefundFTSettlementStatusID },
                  new GameDetail { ResourceId = "04" ,  AwayValue = "1" , SettlementStatusID = EventStatusID.RefundSettlementStatusID }
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("3", viewModel.TotalGameAway);
        }

        [Test]
        public void TotalGameAway_TotalGameValueIsNotGreaterThanZero_ReturnHyphen()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "01" ,  AwayValue = "0" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "02" ,  AwayValue = "0" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "03" ,  AwayValue = "1" , SettlementStatusID = EventStatusID.RefundFTSettlementStatusID },
                  new GameDetail { ResourceId = "04" ,  AwayValue = "1" , SettlementStatusID = EventStatusID.RefundSettlementStatusID }
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.TotalGameAway);
        }

        [TestCase(EventStatusID.RefundSettlementStatusID, "Refund")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "Refund HT")]
        [TestCase(EventStatusID.RefundFTSettlementStatusID, "Refund FT")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "1")]
        [TestCase(EventStatusID.RunningSettlementStatusID, "1")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "-")]
        public void TotalSetHome_InCaseOfSettlementStatusId_GetExpectedResult(int settlementStatusId, string expectedResult)
        {
            // Arrange
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1" , SettlementStatusID = settlementStatusId },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.TotalSetHome);
        }

        [TestCase(EventStatusID.RefundSettlementStatusID, "Refund")]
        [TestCase(EventStatusID.RefundHTSettlementStatusID, "Refund HT")]
        [TestCase(EventStatusID.RefundFTSettlementStatusID, "Refund FT")]
        [TestCase(EventStatusID.CompletedSettlementStatusID, "1")]
        [TestCase(EventStatusID.RunningSettlementStatusID, "1")]
        [TestCase(EventStatusID.Abandoned1HSettlementStatusID, "-")]
        public void TotalSetAway_InCaseOfSettlementStatusId_GetExpectedResult(int settlementStatusId, string expectedResult)
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore , AwayValue = "1" , SettlementStatusID = settlementStatusId },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.TotalSetAWay);
        }

        [Test]
        public void SetDetail_Always_GetListSetValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = "0108" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.Abandoned1HSettlementStatusID },
                  new GameDetail { ResourceId = "0109" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.RefundFTSettlementStatusID },
                  new GameDetail { ResourceId = "0110" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.RefundHTSettlementStatusID },
                  new GameDetail { ResourceId = "0111" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.RefundSettlementStatusID },
                  new GameDetail { ResourceId = "0112" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0113" , HomeValue = "40", AwayValue = "15" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0212" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0213" , HomeValue = "40", AwayValue = "15" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0311" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0312" , HomeValue = "40", AwayValue = "15" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0412" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
                  new GameDetail { ResourceId = "0414" , HomeValue = "40", AwayValue = "15" , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
            };

            var expectedSet1 = GenerateSetValue(1, 13);
            expectedSet1.Games[7].HomeValue = "-";
            expectedSet1.Games[7].AwayValue = "-";
            expectedSet1.Games[8].HomeValue = "Refund FT";
            expectedSet1.Games[8].AwayValue = "Refund FT";
            expectedSet1.Games[9].HomeValue = "Refund HT";
            expectedSet1.Games[9].AwayValue = "Refund HT";
            expectedSet1.Games[10].HomeValue = "Refund";
            expectedSet1.Games[10].AwayValue = "Refund";
            expectedSet1.Games[11].HomeValue = "40";
            expectedSet1.Games[11].AwayValue = "30";
            expectedSet1.Games[12].HomeValue = "40";
            expectedSet1.Games[12].AwayValue = "15";

            var expectedSet2 = GenerateSetValue(2, 13);
            expectedSet2.Games[11].HomeValue = "40";
            expectedSet2.Games[11].AwayValue = "30";
            expectedSet2.Games[12].HomeValue = "40";
            expectedSet2.Games[12].AwayValue = "15";

            var expectedSet3 = GenerateSetValue(3, 13);
            expectedSet3.Games[10].HomeValue = "40";
            expectedSet3.Games[10].AwayValue = "30";
            expectedSet3.Games[11].HomeValue = "40";
            expectedSet3.Games[11].AwayValue = "15";

            var expectedSet4 = GenerateSetValue(4, 26);
            expectedSet4.Games[11].HomeValue = "40";
            expectedSet4.Games[11].AwayValue = "30";
            expectedSet4.Games[13].HomeValue = "40";
            expectedSet4.Games[13].AwayValue = "15";

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.True(_comparer.Compare(expectedSet1, viewModel.SetDetail[0]).AreEqual &&
                _comparer.Compare(expectedSet2, viewModel.SetDetail[1]).AreEqual &&
                _comparer.Compare(expectedSet3, viewModel.SetDetail[2]).AreEqual &&
                _comparer.Compare(expectedSet4, viewModel.SetDetail[3]).AreEqual &&
                _comparer.Compare(null, viewModel.SetDetail[4]).AreEqual);
        }

        [Test]
        public void VisibleDetailedLink_SettingVisibleDetailedLinkIsFalse_ReturnFalse()
        {
            // Arrange
            _setting.VisibleDetailedLink = false;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = "0108" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.Abandoned1HSettlementStatusID },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.False(viewModel.VisibleDetailedLink);
        }

        [TestCase("")]
        [TestCase("01")]
        public void VisibleDetailedLink_GameDetailsHasNullResourceIdOrHasNotCorrectLength_ReturnFalse(string resourceId)
        {
            // Arrange
            _setting.VisibleDetailedLink = true;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = resourceId , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.Abandoned1HSettlementStatusID },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.False(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void VisibleDetailedLink_GameDetailsResourceIdLengthIs4_ReturnTrue()
        {
            // Arrange
            _setting.VisibleDetailedLink = true;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = "0111" , HomeValue = "40", AwayValue = "30" , SettlementStatusID = EventStatusID.Abandoned1HSettlementStatusID },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.True(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void GetWonTeam_IsNotHighlightWinnerTeam_ReturnEmpty()
        {
            // Arrange
            _setting.HighlightWinnerTeam = false;

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.GetWonTeam(true));
        }

        [Test]
        public void GetWonTeam_TotalSetHomeAndTotalSetAwayIsNotInteger_ReturnEmpty()
        {
            // Arrange
            _setting.HighlightWinnerTeam = true;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = "1", AwayValue = "2" , SettlementStatusID = EventStatusID.Abandoned1HSettlementStatusID },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.GetWonTeam(true));
        }

        [TestCase("2", "1", true, "win")]
        [TestCase("2", "1", false, "")]
        [TestCase("1", "2", true, "")]
        [TestCase("1", "2", false, "win")]
        public void GetWonTeam_HighlightWinnerTeam_TotalSetHomeAwayIsInteger_HomeTeam_ReturnWinFormat(string homeValue, string awayValue, bool isHome, string expectedResult)
        {
            // Arrange
            _setting.HighlightWinnerTeam = true;
            _sportsbook.GameDetails = new List<GameDetail>
            {
                  new GameDetail { ResourceId = GameResourceID.FinalScore ,  HomeValue = homeValue, AwayValue = awayValue , SettlementStatusID = EventStatusID.CompletedSettlementStatusID },
            };

            // Act
            var viewModel = new TennisViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.GetWonTeam(isHome));
        }

        private SetValue GenerateSetValue(int setId, int numberOfGames)
        {
            var setValue = new SetValue
            {
                SetId = setId,
                Games = new List<GameValue>()
            };

            for (var gameId = 1; gameId <= numberOfGames; gameId++)
            {
                setValue.Games.Add(new GameValue
                {
                    GameId = gameId,
                    HomeValue = ConstantValue.Hyphen,
                    AwayValue = ConstantValue.Hyphen
                });
            }

            return setValue;
        }
    }
}