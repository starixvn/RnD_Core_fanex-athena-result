namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.BasketBallDetail
{
    using Base.Extension;
    using NUnit.Framework;
    using Sportsbook.ViewModels.BasketBallDetail;

    [TestFixture]
    public class BasketBallDetailViewModelTests
    {
        [Test]
        public void DetailItems_Always_SetValueFromCtor()
        {
            // Arrange
            var detailItems = new SafeList<BasketBallGameDetailItem>
            {
                new BasketBallGameDetailItem { ResourceID = "01", HomeValue = "20" }
            };

            // Act
            var viewModel = new BasketBallDetailViewModel(detailItems);

            // Assert
            Assert.AreEqual(viewModel.DetailItems, detailItems);
        }
    }
}