namespace Fanex.Result.Core.LeapGaming.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Fanex.Result.Core.Base.Utilities;
    using LeapGaming.DTO;
    using LeapGaming.Models;
    using NSubstitute;
    using NUnit.Framework;
    using TestUtils;

    [TestFixture]
    public class BuilderTests
    {
        private Builder _builder;
        private IList<LeapGaming> _dataList;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _builder = new Builder();
            _dataList = new List<LeapGaming>() { new LeapGaming { TransDesc = "md=10;mid=745237;se=3097;m=Turin:Glasgow;s=S03 : Glasgow;o=4.75;ht=0-0;ft=1-0;mo=LOSE" } };
            _setting = new ResultSetting { SportType = SportType.LeapGamingVirtualSports, LanguageCode = "en-US"};
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
            ResultCoreManager.RefDataHelper.GetResourceDataByRefIdAndResourceId("LG_STMT", "md", "en-US", string.Empty).Returns("Match day");
            ResultCoreManager.RefDataHelper.GetResourceDataByRefIdAndResourceId("LG_STMT", "mid", "en-US", string.Empty).Returns("Match");
            ResultCoreManager.RefDataHelper.GetResourceDataByRefIdAndResourceId("LG_STMT", "se", "en-US", string.Empty).Returns("Season");
        }

        [Test]
        public void HtmlBuilder_Always_ReturnExpectedHtml()
        {
            // Arrange
            var builder = new Builder();

            // Act
            var actualHtml = builder.HtmlBuilder(_dataList, _setting);

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("LeapGaming.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(actualHtml));
        }

        [Test]
        public void ExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<LeapGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightHtmlBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightHtmlBuilder(new List<LeapGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void PopUpParalyBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.PopUpParalyBuilder(new List<LeapGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void HtmlGameDetailBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.HtmlGameDetailBuilder(new List<LeapGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void OutrightExcelBuilder_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => _builder.OutrightExcelBuilder(new List<LeapGaming>(), null);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}