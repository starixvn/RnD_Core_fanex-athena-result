namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.DataDomain;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class TennisViewModelTests
    {
        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan4_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new TennisViewModel(Arg.Any<Sportsbook>(), new ResultSetting());
            var index = 7;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndGameDetailsDoesNotHaveAnyElement_ReturnsHyphen()
        {
            // Arrange
            var sportsbook = new Sportsbook
            {
                GameDetails = new List<GameDetail>()
            };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(5, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndFirstElementOfGameDetailsHasResourceIdNotEqualToIndexPlus1_ReturnsHyphen()
        {
            // Arrange
            var sportsbook = new Sportsbook
            {
                GameDetails = new List<GameDetail>()
            };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(5, Arg.Any<bool>());

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
            var gameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RunningSettlementStatusID
                }
            };

            var sportsbook = new Sportsbook { GameDetails = gameDetails };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

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
            var gameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                }
            };

            var sportsbook = new Sportsbook { GameDetails = gameDetails };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

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
            var gameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RefundStettlementStatusID
                }
            };

            var sportsbook = new Sportsbook { GameDetails = gameDetails };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("Refund", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5FirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndAndSettlementStatusIDIsRefundHT_ReturnsHTRefund()
        {
            // Arrange
            var index = 3;
            var gameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RefundHTSettlementStatusID
                }
            };

            var sportsbook = new Sportsbook { GameDetails = gameDetails };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("Refund HT", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5FirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndAndSettlementStatusIDIsRefundFT_ReturnsRefundFT()
        {
            // Arrange
            var index = 3;
            var gameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = EventStatusID.RefundFTSettlementStatusID
                }
            };

            var sportsbook = new Sportsbook { GameDetails = gameDetails };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("Refund FT", homeAwaySet);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexLessThan5FirstElementOfGameDetailsHasResourceIdIsEqualToIndexPlus1AndAndSettlementStatusIDIsOtherCases_ReturnsHyphen()
        {
            // Arrange
            var index = 3;
            var gameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = (index + 1).ToString("D2"),
                    HomeValue = "10",
                    AwayValue = "12",
                    SettlementStatusID = 100
                }
            };

            var sportsbook = new Sportsbook { GameDetails = gameDetails };

            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void VisibleSet_FromBetIDIsNotInteger_ThrowsException()
        {
            // Arrange
            var viewModel = new TennisViewModel(Arg.Any<Sportsbook>(), new ResultSetting());

            // Act
            TestDelegate testDelegate = () => viewModel.VisibleSet(Arg.Any<int>());

            // Assert
            Assert.Throws<NullReferenceException>(testDelegate);
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void VisibleSet_FromSetIsOutRange1To5_ReturnsFalse(int set)
        {
            // Arrange
            var sportsbook = new Sportsbook { BetID = "1" };
            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

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
            var sportsbook = new Sportsbook { BetID = betId };
            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

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
            var sportsbook = new Sportsbook { BetID = betId };
            var viewModel = new TennisViewModel(sportsbook, new ResultSetting());

            // Act
            var visibleSet = viewModel.VisibleSet(set);

            // Assert
            Assert.IsFalse(visibleSet);
        }
    }
}