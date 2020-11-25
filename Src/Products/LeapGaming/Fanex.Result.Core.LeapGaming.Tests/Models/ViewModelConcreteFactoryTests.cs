namespace Fanex.Result.Core.LeapGaming.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using LeapGaming.DTO;
    using LeapGaming.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private LeapGaming _leapGaming;

        [SetUp]
        public void SetUp()
        {
            _leapGaming = new LeapGaming();
            _resultSetting = new ResultSetting();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(new List<LeapGaming>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsLeapGaming_ReturnsLeapGamingViewModel()
        {
            // Arrange
            _resultSetting.SportType = SportType.LeapGamingVirtualSports;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_leapGaming, _resultSetting);

            // Assert
            var expectViewModelTypeName = "LeapGamingViewModel";

            Assert.AreEqual(expectViewModelTypeName, viewModel.GetType().Name);
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
        public void GetExcelViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = SportType.LeapGamingVirtualSports;

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<LeapGaming>(), _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}