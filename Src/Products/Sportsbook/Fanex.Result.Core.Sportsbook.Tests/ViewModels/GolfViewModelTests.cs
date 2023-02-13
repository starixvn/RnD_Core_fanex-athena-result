namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class GolfViewModelTests
    {
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(_refDataHelper);
        }

        [TestCase(1, 0, "Lose")]
        [TestCase(0, 1, "Won")]
        [TestCase(999, 1, "Dnf")]
        [TestCase(999, 2, "Dnf")]
        public void FinalHomeScore_ScoreHasScoreText_ShowText(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, 1, "1")]
        [TestCase(2, 2, "2")]
        [TestCase(0, 2, "0")]
        [TestCase(null, null, "-")]
        public void FinalHomeScore_HasScore_ShowScore(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, 0, "Won")]
        [TestCase(0, 1, "Lose")]
        [TestCase(0, 999, "Dnf")]
        [TestCase(2, 999, "Dnf")]
        public void FinalAwayScore_ScoreHasScoreText_ShowText(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, 1, "1")]
        [TestCase(2, 2, "2")]
        [TestCase(0, 2, "2")]
        [TestCase(null, null, "-")]
        public void FinalAwayScore_HasScore_ShowScore(int? homeScoreValue, int? awayScoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = homeScoreValue, FinalAwayScore = awayScoreValue };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [Test]
        public void FinalHomeScore_MatchRefund_ShowRefund()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 2, EventStatus = "Completed", BetStatus = "Refund" };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalHomeScore);
        }

        [Test]
        public void FinalHomeScore_MatchIsResultPedingAndRefund_ShowRefund()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 2, EventStatus = "Completed", SettlementStatus = 1, BetStatus = "Refund" };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalHomeScore);
            Assert.AreEqual("Result Pending", viewModel.EventStatus);
        }

        [Test]
        public void FinalHomeScore_MatchIsResultPedingAndScore_ShowScore()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 2, EventStatus = "Completed", SettlementStatus = 1 };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("1", actualFinalHomeScore);
            Assert.AreEqual("Result Pending", viewModel.EventStatus);
        }

        [Test]
        public void FinalAwayScore_MatchRefund_ShowRefund()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 2, EventStatus = "Completed", BetStatus = "Refund" };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalHomeScore);
        }

        [Test]
        public void FinalAwayScore_MatchIsResultPedingAndRefund_ShowRefund()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 2, EventStatus = "Completed", SettlementStatus = 1, BetStatus = "Refund" };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("Refund", actualFinalHomeScore);
            Assert.AreEqual("Result Pending", viewModel.EventStatus);
        }

        [Test]
        public void FinalAwayScore_MatchIsResultPedingAndScore_ShowScore()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 2, EventStatus = "Completed", SettlementStatus = 1 };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("2", actualFinalHomeScore);
            Assert.AreEqual("Result Pending", viewModel.EventStatus);
        }

        [Test]
        public void FinalAwayScore_MatchIsResultPedingAndScoreText_ShowText()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 0, EventStatus = "Completed", SettlementStatus = 1 };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualFinalHomeScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("Won", actualFinalHomeScore);
            Assert.AreEqual("Result Pending", viewModel.EventStatus);
        }

        [Test]
        public void FinalHomeScore_MatchIsResultPedingAndScoreText_ShowText()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = 1, FinalAwayScore = 0, EventStatus = "Completed", SettlementStatus = 1 };
            var viewModel = new GolfViewModel(sportsbookDto, new ResultSetting());

            // Act
            var score = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("Lose", score);
            Assert.AreEqual("Result Pending", viewModel.EventStatus);
        }
    }
}