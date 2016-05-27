namespace Fanex.Result.Core.NumberGame.Tests.ViewModels
{
    using NumberGame.ViewModels;
    using NUnit.Framework;

    [TestFixture]
    public class NumberGameViewModelTests
    {
        [TestCase(0, "00")]
        [TestCase(01, "01")]
        [TestCase(10, "10")]
        [TestCase(100, "100")]
        public void ShowBall_FromBall_ReturnsExpectedFormattedTwoDigits(int ball, string expectedBall)
        {
            // Act
            var formattedBall = NumberGameViewModel.ShowBall(ball);

            // Assert
            Assert.AreEqual(expectedBall, formattedBall);
        }
    }
}