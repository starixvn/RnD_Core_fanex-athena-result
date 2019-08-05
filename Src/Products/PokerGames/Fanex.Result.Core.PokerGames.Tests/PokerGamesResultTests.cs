namespace Fanex.Result.Core.PokerGames.Tests
{
    using System;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.PokerGames.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class PokerGamesResultTests
    {
        private IWebClient _client;
        private ISerializer _serializer;
        private IPokerGamesResultConfigs _configs;
        private PokerGamesResult _result;
        private Func<DateTime> _getDatimeNow;
        private Func<int> _randomInt;

        [SetUp]
        public void Setup()
        {
            _configs = Substitute.For<IPokerGamesResultConfigs>();
            _client = Substitute.For<IWebClient>();
            _serializer = Substitute.For<ISerializer>();
            _getDatimeNow = Substitute.For<Func<DateTime>>();
            _randomInt = Substitute.For<Func<int>>();
            _result = new PokerGamesResult(_client, _serializer, _configs, _getDatimeNow, _randomInt);
        }

        [TestCase]
        public void GetResult_InDebugMode_ReturnRequestSerializedObject()
        {
            var custId = "12";
            var gameTypeId = "34343";
            var transId3rd = "1232";
            var viewerLevel = ViewerLevel.Super;
            var language = "en-US";
            var siteId = 0;
            _configs.DebugMode.Returns(1);
            _configs.Login.Returns("Login");
            _configs.Password.Returns("GetResult_InDebugMode_ReturnRequestSerializedObject");
            _serializer.Serialize(
                Arg.Is<Request>(
                    request => request.Auth.Login == "Login"
                                && request.Auth.Password == "GetResult_InDebugMode_ReturnRequestSerializedObject"))
                                .Returns("Serialized");

            var result = _result.GetResult(custId, gameTypeId, transId3rd, viewerLevel, language, siteId);

            Assert.AreEqual("Serialized", result);
        }

        [TestCase(ViewerLevel.Company, "C")]
        [TestCase(ViewerLevel.Super, "S")]
        [TestCase(ViewerLevel.Master, "M")]
        [TestCase(ViewerLevel.Agent, "A")]
        [TestCase(ViewerLevel.Member, "P")]
        [TestCase(100, "P")]
        public void GetResult_Always_CallUploadString(ViewerLevel viewerLevel, string expectedType)
        {
            var custId = "12";
            var gameTypeId = "34343";
            var transId3rd = "1232";
            var language = "en-US";
            var siteId = 3434;
            var apiUrl = "apiUrl";
            _configs.DebugMode.Returns(0);
            _configs.Login.Returns("Login");
            _configs.Password.Returns("GetResult_Always_CallUploadString");
            _configs.Url.Returns(apiUrl);
            var now = new DateTime(1989, 5, 28);
            _randomInt.Invoke().Returns(10000);
            _getDatimeNow.Invoke().Returns(now);
            _client.UploadString<Request, Response>(
                Arg.Any<string>(),
                Arg.Any<Request>()).Returns(new Response
                {
                    Result = new Result
                    {
                        Url = "ResultUrl",
                        Seq = "627479136000000000-10000-0-12-34343-10000"
                    },
                    TimeStamp = now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                });

            _result.GetResult(custId, gameTypeId, transId3rd, viewerLevel, language, siteId);

            _client.Received().UploadString<Request, Response>(
                apiUrl,
                Arg.Is<Request>(
                    request =>
                        request.Auth.Login == "Login"
                        && request.Auth.Password == "GetResult_Always_CallUploadString"
                        && request.Auth.SiteId == siteId
                        && request.Call.OBCustId == custId
                        && request.Call.GameTypeId == gameTypeId
                        && request.Call.TransId3rd == transId3rd
                        && request.Call.Type == expectedType));
        }

        [TestCase(1901, "P")]
        public void GetResult_VideoPoker_CallUploadString(int bettype, string expectedType)
        {
            var custId = "12";
            var gameTypeId = bettype.ToString();
            var transId3rd = "1232";
            var language = "en-US";
            var siteId = 3434;
            var apiUrl = "apiUrl";
            _configs.DebugMode.Returns(0);
            _configs.Login.Returns("Login");
            _configs.Password.Returns("GetResult_Always_CallUploadString");
            _configs.Url.Returns(apiUrl);
            var now = new DateTime(1989, 5, 28);
            _randomInt.Invoke().Returns(10000);
            _getDatimeNow.Invoke().Returns(now);
            _client.UploadString<Request, Response>(
                Arg.Any<string>(),
                Arg.Any<Request>()).Returns(new Response
                {
                    Result = new Result
                    {
                        Url = "ResultUrl",
                        Seq = "627479136000000000-10000-0-12-1901-10000"
                    },
                    TimeStamp = now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                });

            _result.GetResult(custId, gameTypeId, transId3rd, ViewerLevel.Company, language, siteId);

            _client.Received().UploadString<Request, Response>(
                apiUrl,
                Arg.Is<Request>(
                    request =>
                        request.Auth.Login == "Login"
                        && request.Auth.Password == "GetResult_Always_CallUploadString"
                        && request.Auth.SiteId == siteId
                        && request.Call.OBCustId == custId
                        && request.Call.GameTypeId == gameTypeId
                        && request.Call.TransId3rd == transId3rd
                        && request.Call.Type == expectedType));
        }

        [Test]
        public void GetResult_InvalidRequest_ThrowResultCoreException()
        {
            var custId = "12";
            var gameTypeId = "34343";
            var transId3rd = "1232";
            var language = "en-US";
            var siteId = 3434;
            var apiUrl = "apiUrl";
            _configs.DebugMode.Returns(0);
            _configs.Login.Returns("Login");
            _configs.Password.Returns("GetResult_Always_CallUploadString");
            _configs.Url.Returns(apiUrl);
            var now = new DateTime(1989, 5, 28);
            _randomInt.Invoke().Returns(10000);
            _getDatimeNow.Invoke().Returns(now);
            _client.UploadString<Request, Response>(
                Arg.Any<string>(),
                Arg.Any<Request>()).Returns(new Response
                {
                    Result = new Result
                    {
                        Url = "ResultUrl"
                    },
                    TimeStamp = now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                });

            Assert.Throws<ResultCoreException>(() =>
                _result.GetResult(custId, gameTypeId, transId3rd, ViewerLevel.Super, language, siteId));
        }
    }
}