namespace Fanex.Result.Core.Sportsbook.Tests.Exceptions
{
    using NUnit.Framework;
    using Sportsbook.Exceptions;

    [TestFixture]
    public class SportsbookExceptionTests
    {
        [Test]
        public void Message_Always_GetExpectedMessage()
        {
            // Act
            var exception = new SportsbookException("Message");

            // Assert
            Assert.AreEqual("Sportsbook Result Exception. Exception: Message", exception.Message);
        }
    }
}