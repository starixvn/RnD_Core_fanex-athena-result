namespace Fanex.Result.AGCasino.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Core.AGCasino.DTO;
    using Core.AGCasino.Models;
    using Core.Base;
    using Core.Base.Exception;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting _resultSetting;

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private AGCasino _aGCasino;

        [SetUp]
        public void SetUp()
        {
            _aGCasino = new AGCasino();
            _resultSetting = new ResultSetting();
        }

        [Test]
        public void GetViewModel_FromSportTypeNotGreaterThanZero_ThrowsException()
        {
            // Arrange
            _resultSetting.SportType = (SportType)(-1);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel<AGCasino>(null, _resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_FromSportTypeIsAGCasino_ReturnsAGCasinoViewModel()
        {
            // Arrange
            _resultSetting.SportType = SportType.AGCasino;

            // Act
            var viewModel = ViewModelConcreteFactory.Instance.GetViewModel(_aGCasino, _resultSetting);

            // Assert
            var expectViewModelTypeName = "AGCasinoViewModel";

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
        public void GetExcelViewModel_Always_ThrowsException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<AGCasino>(), _resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}