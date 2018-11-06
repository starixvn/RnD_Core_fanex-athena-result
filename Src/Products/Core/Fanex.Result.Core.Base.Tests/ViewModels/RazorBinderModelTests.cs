namespace Fanex.Result.Core.Base.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Base.ViewModels;
    using Sportsbook.ViewModels;
    using NUnit.Framework;

    [TestFixture]
    public class RazorBinderModelTests
    {
        [Test]
        public void VisibleMainResult_IsDetail_IsNotPopup_DataCountIsNotOne_ReturnTrue()
        {
            // Arrange
            var razorBinderModel = new RazorBinderModel
            {
                IsDetail = true,
                IsPopUp = false,
                Data = new List<GroupData>()
            };

            // Act
            var actualResult = razorBinderModel.VisibleMainResult;

            // Assert
            Assert.True(actualResult);
        }

        [Test]
        public void VisibleMainResult_IsNotDetail_IsPopup_DataCountIsOne_ReturnDataHasAnyVisibleMatchResult()
        {
            // Arrange
            var razorBinderModel = new RazorBinderModel
            {
                IsDetail = false,
                IsPopUp = true,
                Data = new List<GroupData>()
            };

            razorBinderModel.Data.Add(new GroupData());

            // Act
            var actualResult = razorBinderModel.VisibleMainResult;

            // Assert
            Assert.False(actualResult);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VisibleDetailResult_IsDetail_ReturnIsDetail(bool expectedResult)
        {
            // Arrange
            var razorBinderModel = new RazorBinderModel
            {
                IsDetail = expectedResult,
            };

            // Act
            var actualResult = razorBinderModel.VisibleDetailResult;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void VisibleDetailResult_IsNotDetail_IsPopup_DataCountIsOne_ReturnDataHasAnyVisibleGameDetail()
        {
            // Arrange
            var razorBinderModel = new RazorBinderModel
            {
                IsDetail = false,
                IsPopUp = true,
                Data = new List<GroupData>()
            };

            razorBinderModel.Data.Add(new GroupData());

            // Act
            var actualResult = razorBinderModel.VisibleDetailResult;

            // Assert
            Assert.False(actualResult);
        }
    }
}