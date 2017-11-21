namespace Fanex.Result.AllBetCasino.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using Core.AllBetCasino;
    using NUnit.Framework;

    [TestFixture]
    public class AllBetCasinoExceptionTests
    {
        [Test]
        public void Message_Always_GetMessage()
        {
            // Act
            var exception = new AllBetCasinoException("test");

            // Assert
            Assert.AreEqual("AllBetCasino Result API Exception. Exception: test", exception.Message);
        }
    }
}