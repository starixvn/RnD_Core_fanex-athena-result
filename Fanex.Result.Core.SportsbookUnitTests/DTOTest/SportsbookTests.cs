namespace Fanex.Result.Core.Sportsbook.Tests.DTOTest
{
    using System;
    using DTO;
    using NUnit.Framework;

    [TestFixture]
    public class SportsbookTests
    {
        [Test]
        public void EventDate_Always_GetSetValue()
        {
            // Arrange
            var sportsbook = new Sportsbook { EventDate = DateTime.Today };

            // Act & Assert
            Assert.AreEqual(DateTime.Today, sportsbook.EventDate);
        }
    }
}