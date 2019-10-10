namespace Fanex.Result.Core.MetricGaming.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using MetricGaming.DTO;
    using MetricGaming.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private MetricGaming _metricGaming;

        [SetUp]
        public void SetUp()
        {
            _metricGaming = new MetricGaming();
            _resultSetting = new ResultSetting();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel<MetricGaming>(null, _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }
        
        [Test]
        public void GetOutrightViewModel_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetOutrightViewModel(null, _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void GetParlayViewModel_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetParlayViewModel(null, _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_Always_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = SportType.AGCasino;

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<MetricGaming>(), _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}