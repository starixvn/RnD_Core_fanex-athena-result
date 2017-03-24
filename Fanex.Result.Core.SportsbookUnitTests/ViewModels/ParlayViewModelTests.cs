namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.Exception;
    using Base.Extension;
    using Base.ViewModels;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Models;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class ParlayViewModelTests
    {
        [Test]
        public void Render_FromSportIdIsZero_ReturnsEmpty()
        {
            // Arrage
            var parlay = new Parlay { SportId = 0 };
            var resultSetting = new ResultSetting();
            var viewModel = new ParlayViewModel(parlay, resultSetting);

            // Act
            var result = viewModel.Render(new RazorBinderModel());

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Render_FromInvalidSportIdNotZero_ThrowsException()
        {
            // Arrage
            var parlay = new Parlay { SportId = 99999 };
            var resultSetting = new ResultSetting();
            var viewModel = new ParlayViewModel(parlay, resultSetting);

            // Act
            TestDelegate testDelegate = () => viewModel.Render(new RazorBinderModel());

            // Assert
            Assert.Throws<ResultCoreException>(testDelegate);
        }
    }
}