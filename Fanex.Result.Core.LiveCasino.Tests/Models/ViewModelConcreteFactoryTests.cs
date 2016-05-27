namespace Fanex.Result.Core.LiveCasino.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using DTO;
    using LiveCasino.Models;
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
        }

        [Test]
        public void GetViewModel_FromSportTypeLessThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(new List<LiveCasino>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsEqualOrGreaterThanZero_ReturnsNumberGame()
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
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetOutrightViewModel(new List<LiveCasino>(), _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void GetParlayViewModel_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetParlayViewModel(new List<LiveCasino>(), _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeLessThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<LiveCasino>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeIsEqualOrGreaterThanZero_ReturnsNumberGame()
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