namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.SoccerDetail
{
    using NUnit.Framework;
    using Sportsbook.ViewModels.SoccerInfo;

    [TestFixture]
    public class SoccerDetailItemTests
    {
        [Test]
        public void OrderNo_Always_GetSetValue()
        {
            // Arrange
            var soccerDetailItem = new SoccerGameDetailItem { OrderNo = 1 };

            // Act & Assert
            Assert.AreEqual(1, soccerDetailItem.OrderNo);
        }
    }
}