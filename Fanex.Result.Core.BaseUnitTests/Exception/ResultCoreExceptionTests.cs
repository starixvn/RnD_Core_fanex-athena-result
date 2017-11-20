namespace Fanex.Result.Core.Base.Tests.Exception
{
    using Base.Exception;
    using NUnit.Framework;

    [TestFixture]
    internal class ResultCoreExceptionTests
    {
        [Test]
        public void Message_Always_GetExactMessage()
        {
            // Arrange
            var exception = new ResultCoreException("Message");

            // Act
            var actualMessage = exception.Message;

            // Assert
            Assert.AreEqual("Result.Core Exception. Exception: Message", actualMessage);
        }
    }
}