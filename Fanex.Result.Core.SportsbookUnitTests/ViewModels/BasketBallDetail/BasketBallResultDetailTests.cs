namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.SoccerDetail
{
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels.BasketBallDetail;

    [TestFixture]
    public class BasketBallResultDetailTests
    {
        private ISportsbookRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsBookHelper;
        private Sportsbook _sportsBook;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
            _sportsBookHelper = Substitute.For<ISportsbookHelperWraper>();
            _sportsBook = new Sportsbook();
        }

        [Test]
        public void CreateGameDetailItem_ItemHasMoreResultResourceId_SetResourceNameByResourceId()
        {
        }
    }
}