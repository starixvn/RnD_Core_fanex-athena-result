namespace Fanex.Result.BBin.Tests
{
    using Fanex.Result.Core.BBin;
    using NUnit.Framework;

    [TestFixture]
    public class BBinExceptionTests
    {
        [Test]
        public void Message_Always_GetMessage()
        {
            // Act
            var exception = new BBinException("test");

            // Assert
            Assert.AreEqual("BBin Result API Exception. Exception: test", exception.Message);
        }
    }
}