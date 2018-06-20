namespace Fanex.Result.Core.GoldDeluxe.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using GoldDeluxe.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;
        private GoldDeluxe _goldDeluxe;

        [SetUp]
        public void SetUp()
        {
            _goldDeluxe = new GoldDeluxe();
            _resultSetting = new ResultSetting();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetViewModel_FromSportTypeIsNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(new List<GoldDeluxe>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsGoldDeluxe_ReturnsGoldDeluxeViewModel()
        {
            // Arrange
            _resultSetting.SportType = SportType.GoldDeluxe;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_goldDeluxe, _resultSetting);

            // Assert
            var expectViewModelTypeName = "GoldDeluxeViewModel";

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
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<GoldDeluxe>(), _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_FromSportTypeIsGoldDeluxe_ReturnsGoldDeluxeExcel()
        {
            // Arrange
            _resultSetting.SportType = SportType.GoldDeluxe;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<GoldDeluxe>(), _resultSetting);

            // Assert
            var expectViewModelTypeName = "GoldDeluxeExcel";

            Assert.AreEqual(expectViewModelTypeName, viewModel.GetType().Name);
        }
    }
}