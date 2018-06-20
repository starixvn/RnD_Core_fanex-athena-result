namespace Fanex.Result.Core.NumberGame.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NumberGame.Models;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private NumberGame _numberGame;

        [SetUp]
        public void SetUp()
        {
            _numberGame = new NumberGame();
            _resultSetting = new ResultSetting();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel<NumberGame>(null, _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNumberGame_ReturnsNumberGameViewModel()
        {
            // Arrange
            _resultSetting.SportType = SportType.NumberGame;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_numberGame, _resultSetting);

            // Assert
            var expectViewModelTypeName = "NumberGameViewModel";
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
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<NumberGame>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeIsNumberGame_ReturnsNumberGameExcel()
        {
            // Arrange
            _resultSetting.SportType = SportType.NumberGame;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<NumberGame>(), _resultSetting);

            // Assert
            var expectViewModelTypeName = "NumberGameExcel";

            Assert.AreEqual(expectViewModelTypeName, viewModel.GetType().Name);
        }
    }
}