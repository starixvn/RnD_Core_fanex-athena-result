namespace Fanex.Result.Core.MetricGaming.Tests.DTO
{
    using NUnit.Framework;
    using MetricGaming.DTO;

    [TestFixture]
    public class MetricGamingTests
    {
        [Test]
        public void MatchId_Always_GetSetValue()
        {
            // Arrange
            var metricGaming = new MetricGaming();

            // Act
            metricGaming.MatchId = 1;

            // Assert
            Assert.AreEqual(1, metricGaming.MatchId);
        }
    }
}