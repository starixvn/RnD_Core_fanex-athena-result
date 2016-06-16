namespace Fanex.Result.Core.Racing.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using DTO;
    using NUnit.Framework;
    using Racing.Models;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private Racing _racing;

        [SetUp]
        public void SetUp()
        {
            _racing = new Racing();
            _resultSetting = new ResultSetting();
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel<Racing>(null, _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotRacing_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = SportType.Soccer;

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(new List<Racing>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [TestCase(151, "RacingViewModel")]
        [TestCase(152, "RacingViewModel")]
        [TestCase(153, "RacingViewModel")]
        [TestCase(181, "VSHRacingViewModel")]
        [TestCase(182, "VSHRacingViewModel")]
        [TestCase(183, "VSSpeedWayViewModel")]
        [TestCase(184, "VSHRacingViewModel")]
        [TestCase(185, "VSHRacingViewModel")]
        public void GetViewModel_FromSportTypeIsRacing_ReturnsExpectedViewModelType(int sportType, string expectedViewModelType)
        {
            // Arrange
            _resultSetting.SportType = (SportType)sportType;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_racing, _resultSetting);

            // Assert
            Assert.AreEqual(expectedViewModelType, viewModel.GetType().Name);
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
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Racing>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeIsNotRacing_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = SportType.Soccer;

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Racing>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [TestCase(151, "RacingExcel")]
        [TestCase(152, "RacingExcel")]
        [TestCase(153, "RacingExcel")]
        [TestCase(181, "VSHRacingExcel")]
        [TestCase(182, "VSHRacingExcel")]
        [TestCase(183, "VSSpeedWayExcel")]
        [TestCase(184, "VSHRacingExcel")]
        [TestCase(185, "VSHRacingExcel")]
        public void GetExcelViewModel_FromSportType_ReturnsExpectedExcelModelType(int sportType, string expectedExcelModelType)
        {
            // Arrange
            _resultSetting.SportType = (SportType)sportType;

            // Act
            var excelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Racing>(), _resultSetting);

            // Assert
            Assert.AreEqual(expectedExcelModelType, excelViewModel.GetType().Name);
        }
    }
}