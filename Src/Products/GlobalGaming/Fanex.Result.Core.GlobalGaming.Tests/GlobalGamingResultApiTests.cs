namespace Fanex.Result.Core.GlobalGaming.Tests
{
    using DTO;
    using Exceptions;
    using GlobalGaming;
    using NUnit.Framework;

    [TestFixture]
    internal class GlobalGamingResultApiTests
    {
        private GlobalGamingDTO _data;

        [SetUp]
        public void Setup()
        {
            _data = new GlobalGamingDTO
            {
                CustId = "1",
                GameId = "1",
                SiteId = "1"
            };
        }

        [Test]
        public void GetResultUrl_CustIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new GlobalGamingResultApi();
            _data.CustId = "";

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultUrl(_data));

            // Assert
            Assert.Throws(
                Is.TypeOf<GlobalGamingException>().And
                    .Message.EqualTo("Global Gaming Result API Exception. Exception: custId is null or empty!"),
                testDelegate);
        }

        [Test]
        public void GetResultUrl_GameIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new GlobalGamingResultApi();
            _data.GameId = "";

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultUrl(_data));

            // Assert
            Assert.Throws(
                Is.TypeOf<GlobalGamingException>().And
                    .Message.EqualTo("Global Gaming Result API Exception. Exception: gameId is null or empty!"),
                testDelegate);
        }

        [Test]
        public void GetResultUrl_SiteIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new GlobalGamingResultApi();
            _data.SiteId = "";

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultUrl(_data));

            // Assert
            Assert.Throws(
                Is.TypeOf<GlobalGamingException>().And
                    .Message.EqualTo("Global Gaming Result API Exception. Exception: siteId argument must have value or SiteId in Web.config file!"),
                testDelegate);
        }

        [Test]
        public void GetResultUrl_DataIsValid_DebugMode_ThrowException()
        {
            // Arrange
            var api = new GlobalGamingResultApi();

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultUrl(_data));

            // Assert
            Assert.Throws(
              Is.TypeOf<GlobalGamingException>(),
              testDelegate);
        }
    }
}