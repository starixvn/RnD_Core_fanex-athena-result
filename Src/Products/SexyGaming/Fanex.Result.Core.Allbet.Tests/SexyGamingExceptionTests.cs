namespace Fanex.Result.AllBetCasino.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using Core.SexyGaming;
    using NUnit.Framework;

    [TestFixture]
    public class SexyGamingExceptionTests
    {
        [Test]
        public void Message_Always_GetMessage()
        {
            // Act
            var exception = new SexyGamingException("test");

            // Assert
            Assert.AreEqual("SexyGaming Result API Exception. Exception: test", exception.Message);
        }
    }
}