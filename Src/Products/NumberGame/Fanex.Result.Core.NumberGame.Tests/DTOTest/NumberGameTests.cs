namespace Fanex.Result.Core.NumberGame.Tests.DTOTest
{
    using System;
    using NUnit.Framework;
    using DTO;

    [TestFixture]
    internal class NumberGameTests
    {
        [Test]
        public void EventDate_Always_GetSetValue()
        {
            // Arrange
            var numberGame = new NumberGame { EventDate = DateTime.Parse("10/10/2017") };

            // Assert
            Assert.AreEqual("10/10/2017 12:00:00 AM", numberGame.EventDate.ToString());
        }
    }
}