namespace Fanex.Result.Core.PP.Tests
{
    using System;
    using System.Configuration;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.PP;
    using Fanex.Result.Core.PP.Models;
    using Fanex.Result.Core.PP.Ultilities.Serializer;
    using Fanex.Result.Core.PP.Ultilities.WebClient;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class PPResultTests
    {
        private readonly PPConfigs _configs = PPConfigs.Configs;
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
        public void GetPPResult_CustIdIsInvalid_ThrowException(string custId)
        {
            // Arrange
            var ppResult = new PPResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => ppResult.GetPPResult(custId, "0")));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetPPResult_GamePlayIdIsInvalid_ThrowException(string gamePlayId)
        {
            // Arrange
            var ppResult = new PPResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => ppResult.GetPPResult("0", gamePlayId)));
        }

        [Test]
        public void GetPPResult_IsNotDebugMode_ResponseDataTimestampIsInvalid_ThrowException()
        {
            // Arrange
            var response = CreateReponseData();
            _client.UploadString<Request, Response>(_configs.Url, Arg.Any<Request>()).Returns(response);
            _serializer.Serialize(Arg.Any<object>()).Returns("serialized");
            var ppResult = new PPResult(_client, _serializer);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => ppResult.GetPPResult("1", "2")), "PP Result send request incorrectly!");
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