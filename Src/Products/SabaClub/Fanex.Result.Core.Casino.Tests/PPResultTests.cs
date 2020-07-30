namespace Fanex.Result.Core.Casino.Tests
{
    using System;
    using System.Configuration;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Casino;
    using Fanex.Result.Core.Casino.Models;
    using Fanex.Result.Core.Casino.Ultilities.Serializer;
    using Fanex.Result.Core.Casino.Ultilities.WebClient;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class CasinoResultTests
    {        
        private IWebClient _client;
        private ISerializer _serializer;
        private const string sectionName = "CasinoResultConfigs";
        private readonly CasinoConfigs _configs = ConfigurationManager.GetSection(sectionName) as CasinoConfigs;

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
            var CasinoResult = new CasinoResult(sectionName, _client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetCasinoResult(custId, "0")));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCasinoResult_GamePlayIdIsInvalid_ThrowException(string gamePlayId)
        {
            // Arrange
            var CasinoResult = new CasinoResult(sectionName, _client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetCasinoResult("0", gamePlayId)));
        }

        [Test]
        public void GetCasinoResult_IsNotDebugMode_ResponseDataTimestampIsInvalid_ThrowException()
        {
            // Arrange
            var response = CreateReponseData();
            _client.UploadString<Request, Response>(_configs.Url, Arg.Any<Request>()).Returns(response);
            _serializer.Serialize(Arg.Any<object>()).Returns("serialized");
            var CasinoResult = new CasinoResult(sectionName, _client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => CasinoResult.GetCasinoResult("1", "2")), "Casino Result send request incorrectly!");
        }

        private Response CreateReponseData()
        {
            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{_configs.SiteId}-{"1"}-{"2"}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";
            var request = new Request
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password,
                    SiteId = _configs.SiteId
                },
                Call = new Call
                {
                    Seq = seq,
                    OBCustId = "1",
                    GamePlayId = "2",
                    VendorId = _configs.VendorId,
                    Language = "en"
                }
            };
            var response = new Response
            {
                TimeStamp = request.TimeStamp,
                Result = new Result
                {
                    Seq = request.Call.Seq,
                    Url = "https://nexdev.net/projects/p111023/wiki/201801_Pragmatic_Betlist",
                    ErrorCode = "0"
                }
            };
            return response;
        }
    }
}