namespace Fanex.Result.Core.Happy5.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Base;
    using Base.Exception;
    using Base.Utilities;
    using Base.ViewModels;
    using Data;
    using Fanex.Result.Core.Happy5.Models;
    using Fanex.Result.Core.Happy5.ViewModels;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelConcreteFactoryTests
    {
        private ResultSetting resultSetting;
        private DTO.Happy5 resultData;
        private CompareLogic comparer;

        [SetUp]
        public void Setup()
        {
            resultSetting = new ResultSetting { SportType = SportType.Happy5 };
            resultData = new DTO.Happy5();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
            comparer = new CompareLogic();
        }

        [Test]
        public void GetViewModel_SportTypeIsNotValid_ThrowException()
        {
            // Arrange
            resultSetting.SportType = (SportType)(-9999);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetViewModel(resultData, resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetViewModel_SportTypeIsValid_ReturnLucky3ViewModel()
        {
            // Arrange
            var expectedViewModel = new Happy5ViewModel(resultData, resultSetting, ResultCoreManager.RefDataHelper);

            // Act
            var actualViewModel = ViewModelConcreteFactory.Instance.GetViewModel(resultData, resultSetting);

            // Assert
            Assert.True(comparer.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Test]
        public void GetExcelViewModel_SportTypeIsNotValid_ThrowException()
        {
            // Arrange
            resultSetting.SportType = (SportType)(-9999);

            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<DTO.Happy5> { resultData }, resultSetting);

            // Assert
            Assert.Throws<ObjectNullException>(testDelegate);
        }

        [Test]
        public void GetExcelViewModel_SportTypeIsValid_ReturnLucky3ViewModel()
        {
            // Arrange
            IEnumerable<IResultViewModel> viewModels = new List<Happy5ViewModel> {
                new Happy5ViewModel(resultData, resultSetting, ResultCoreManager.RefDataHelper)
            };
            var expectedExcelViewModel = new Happy5ExcelViewModel(FilePath.ExcelTemplateFilePath, resultSetting, viewModels.ToList());

            // Act
            var actualExcelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(new List<DTO.Happy5> { resultData }, resultSetting);

            // Assert
            Assert.True(comparer.Compare(expectedExcelViewModel, actualExcelViewModel).AreEqual);
        }

        [Test]
        public void GetOutrightViewModel_Always_ThrowException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetOutrightViewModel(resultData, resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }

        [Test]
        public void GetParlayViewModel_Always_ThrowException()
        {
            // Act
            TestDelegate testDelegate = () => ViewModelConcreteFactory.Instance.GetParlayViewModel(resultData, resultSetting);

            // Assert
            Assert.Throws<NotImplementedException>(testDelegate);
        }
    }
}