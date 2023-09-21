namespace Fanex.Result.Core.LeapGaming.Tests.ViewModels
{
    using Base;
    using Base.Utilities;
    using LeapGaming.DTO;
    using LeapGaming.ViewModels;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class LeapGamingViewModelTests
    {
        private LeapGaming _leapGaming;
        private IRefDataHelper _refDataHelper;
        private ResultSetting _setting;

        [SetUp]
        public void Setup()
        {
            _leapGaming = new LeapGaming();
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _setting = new ResultSetting { LanguageCode = "en-US" };

            _refDataHelper.GetResourceDataByRefIdAndResourceId("LG_STMT", "md", "en-US", string.Empty).Returns("Match day");
            _refDataHelper.GetResourceDataByRefIdAndResourceId("LG_STMT", "mid", "en-US", string.Empty).Returns("Match");
            _refDataHelper.GetResourceDataByRefIdAndResourceId("LG_STMT", "se", "en-US", string.Empty).Returns("Season");
        }
    }
}