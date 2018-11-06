namespace Fanex.Result.Core.LiveCasino.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using LiveCasino.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private LiveCasino _liveCasino;

        [SetUp]
        public void SetUp()
        {
            _liveCasino = new LiveCasino();
            _resultSetting = new ResultSetting();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetViewModel_FromSportTypeNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel<LiveCasino>(null, _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsLiveCasino_ReturnsLiveCasinoViewModel()
        {
            // Arrange
            _resultSetting.SportType = SportType.LiveCasino;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_liveCasino, _resultSetting);

            // Assert
            var expectViewModelTypeName = "LiveCasinoViewModel";

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
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<LiveCasino>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeIsLiveCasino_ReturnsLiveCasinoExcel()
        {
            // Arrange
            _resultSetting.SportType = SportType.LiveCasino;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<LiveCasino>(), _resultSetting);

            // Assert
            var expectViewModelTypeName = "LiveCasinoExcel";

            Assert.AreEqual(expectViewModelTypeName, viewModel.GetType().Name);
        }
    }
}