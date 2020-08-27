namespace Fanex.Result.Core.Casino.Tests
{
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.SabaClub;
    using Fanex.Result.Core.SabaClub.Ultilities.Serializer;
    using Fanex.Result.Core.SabaClub.Ultilities.WebClient;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class CasinoResultTests
    {
        private IWebClient _client;
        private ISerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IWebClient>();
            _serializer = Substitute.For<ISerializer>();
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_CustIdIsInvalid_ThrowException(string custId)
        {
            // Arrange
            var CasinoResult = new SabaClubResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetResult(custId, "0", "0", "0", 123)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_GameRoundIdIsInvalid_ThrowException(string gameRoundId)
        {
            // Arrange
            var CasinoResult = new SabaClubResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetResult("0", gameRoundId, "0", "0", 123)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_gameIdIsInvalid_ThrowException(string gameId)
        {
            // Arrange
            var CasinoResult = new SabaClubResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetResult("0", "0", gameId, "0", 123)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_siteIdIsInvalid_ThrowException(string siteId)
        {
            // Arrange
            var CasinoResult = new SabaClubResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetResult("0", "0", siteId, "0", 123)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_GamePlayIdIsInvalid_ThrowException(string gamePlayId)
        {
            // Arrange
            var CasinoResult = new SabaClubResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetResult("0", gamePlayId, "0", "0", 123)));
        }
    }
}