namespace Fanex.Result.Core.Bitcoin.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using Bitcoin.Models;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private Bitcoin _Bitcoin;

        [SetUp]
        public void SetUp()
        {
            _Bitcoin = new Bitcoin();
            _resultSetting = new ResultSetting();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel<Bitcoin>(null, _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsBitcoin_ReturnsBitcoinViewModel()
        {
            // Arrange
            _resultSetting.SportType = SportType.Bitcoin;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_Bitcoin, _resultSetting);

            // Assert
            var expectViewModelTypeName = "BitcoinViewModel";
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
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Bitcoin>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeIsBitcoin_ReturnsBitcoinExcel()
        {
            // Arrange
            _resultSetting.SportType = SportType.Bitcoin;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<Bitcoin>(), _resultSetting);

            // Assert
            var expectViewModelTypeName = "BitcoinExcel";

            Assert.AreEqual(expectViewModelTypeName, viewModel.GetType().Name);
        }
    }
}