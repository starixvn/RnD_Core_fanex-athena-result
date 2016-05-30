namespace Fanex.Result.Core.Sportsbook.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.DTO;
    using Sportsbook.Models;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private Sportsbook _sportsbook;
        private TestDelegate _testDelegate;

        [SetUp]
        public void SetUp()
        {
            _resultSetting = new ResultSetting();
            _sportsbook = new Sportsbook();
        }

        [Test]
        public void GetViewModel_FromSportTypeLessThanZero_ThrowException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            _testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(Arg.Any<object>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(_testDelegate);
        }

        [TestCase(100110)]
        [TestCase(99999)]
        public void GetViewModel_FromSportTypeUndefined_ThrowException(int sportType)
        {
            // Arrange
            _resultSetting.SportType = (SportType)sportType;

            // Act
            _testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(Arg.Any<object>(), _resultSetting);

            // Assert
            Assert.Throws<NullReferenceException>(_testDelegate);
        }

        [TestCase(1, "SoccerViewModel")]
        [TestCase(2, "BasketBallViewModel")]
        [TestCase(10, "BilliardViewModel")]
        [TestCase(32, "BasketBallViewModel")]
        [TestCase(44, "MuayThaiViewModel")]
        [TestCase(186, "VSTennisViewModel")]
        public void GetViewModel_FromSportTypeIsDefined_ReturnsExpectedViewModelType(int sportType, string expectedViewModelType)
        {
            // Arrange
            _resultSetting.SportType = (SportType)sportType;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_sportsbook, _resultSetting);

            // Assert
            Assert.AreEqual(expectedViewModelType, viewModel.GetType().Name);
        }

        [Test]
        public void GetOutrightViewModel_Always_ReturnsOutrightViewModel()
        {
            // Arrange
            var outright = new Outright();

            // Act
            var outrightViewModel = ViewModelConcreteFactory.Instance.GetOutrightViewModel(outright, _resultSetting);

            // Assert
            var expectedViewModelType = "OutrightViewModel";
            Assert.AreEqual(expectedViewModelType, outrightViewModel.GetType().Name);
        }

        [Test]
        public void GetParlayViewModel_Always_ReturnsOutrighttParlayViewModel()
        {
            // Arrange
            var parlay = new Parlay();

            // Act
            var parlayViewModel = ViewModelConcreteFactory.Instance.GetParlayViewModel(parlay, _resultSetting);

            // Assert
            var expectedViewModelType = "ParlayViewModel";
            Assert.AreEqual(expectedViewModelType, parlayViewModel.GetType().Name);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeLessThanZero_ThrowException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            _testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Sportsbook>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(_testDelegate);
        }

        [TestCase(100110)]
        [TestCase(99999)]
        public void GetExcelViewModel_FromSportTypeIsUndefined_ThrowException(int sportType)
        {
            // Arrange
            _resultSetting.SportType = (SportType)sportType;

            // Act
            _testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Sportsbook>(), _resultSetting);

            // Assert
            Assert.Throws<NullReferenceException>(_testDelegate);
        }

        [TestCase(1, "SoccerExcel")]
        [TestCase(2, "BasketBallExcel")]
        [TestCase(4, "HockeyExcel")]
        [TestCase(5, "TennisExcel")]
        [TestCase(6, "BadmintonExcel")]
        [TestCase(8, "BaseBallExcel")]
        [TestCase(10, "BilliardExcel")]
        [TestCase(32, "BasketBallExcel")]
        [TestCase(44, "MuayThaiExcel")]
        [TestCase(186, "VSTennisExcel")]
        public void GetExcelViewModel_FromSportTypeIsDefined_ReturnsExpectedExcelModelType(int sportType, string expectedExcelModelType)
        {
            // Arrange
            _resultSetting.SportType = (SportType)sportType;

            // Act
            var excelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Sportsbook>(), _resultSetting);

            // Assert
            Assert.AreEqual(expectedExcelModelType, excelViewModel.GetType().Name);
        }
    }
}