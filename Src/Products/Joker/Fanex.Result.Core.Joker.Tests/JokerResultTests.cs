namespace Fanex.Result.Core.Casino.Tests
{
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Joker;
    using Fanex.Result.Core.Joker.Ultilities.Serializer;
    using Fanex.Result.Core.Joker.Ultilities.WebClient;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class JokerResultTests
    {
        private IWebClient _client;
        private ISerializer _serializer;
        private readonly JokerConfigs _configs = JokerConfigs.Configs;

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
            var result = new JokerResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => result.GetJokerResult(custId, "0", "0")));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_GamePlayIdIsInvalid_ThrowException(string gamePlayId)
        {
            // Arrange
            var result = new JokerResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => result.GetJokerResult("0", gamePlayId, "0")));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_GameCodeIsInvalid_ThrowException(string gameCode)
        {
            // Arrange
            var result = new JokerResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => result.GetJokerResult("0", "0", gameCode)));
        }
    }
}