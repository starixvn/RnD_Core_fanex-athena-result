namespace Fanex.Result.Core.Casino.Tests
{
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.VirtualGames;
    using Fanex.Result.Core.VirtualGames.Models;
    using Fanex.Result.Core.VirtualGames.Ultilities;
    using Fanex.Result.Core.VirtualGames.Ultilities.Serializer;
    using Fanex.Result.Core.VirtualGames.Ultilities.WebClient;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class VirtualGamesResultTests
    {
        private IWebClient _client;
        private ISerializer _serializer;
        private INewGui _newGui;
        private VirtualGamesResult result;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IWebClient>();
            _serializer = Substitute.For<ISerializer>();
            _newGui = Substitute.For<INewGui>();
            result = new VirtualGamesResult(_client, _serializer, _newGui);
        }

        [Test]
        public void GetResult_SeqIsInValid_ThrowException()
        {
            // Arrange            
            var response = new Response { Result = new Result { Seq = "test" } };
            _client.UploadString<Request, Response>(Arg.Any<string>(), Arg.Any<Request>())
                .Returns(response);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => result.GetResult("d")));
        }

        [TestCase("1000")]
        [TestCase("1007")]
        [TestCase("2001")]
        public void GetResult_ErrorCodeIsNotZero_ThrowException(string errorCode)
        {
            // Arrange
            var response = new Response { Result = new Result { ErrorCode = errorCode } };
            _client.UploadString<Request, Response>(Arg.Any<string>(), Arg.Any<Request>())
                .Returns(response);

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => result.GetResult("d")));
        }

        [Test]
        public void GetResult_Success_AddMoreInfo()
        {
            // Arrange
            _newGui.CreateNewId().Returns("abc");
            var response = new Response
            {
                Result = new Result
                {
                    Seq=_newGui.CreateNewId(),
                    ErrorCode = "0",
                    Url = "abc"
                }
            };
            _client.UploadString<Request, Response>(Arg.Any<string>(), Arg.Any<Request>())
                .Returns(response);

            // Act
            var act = result.GetResult("D", "en-US");

            // Assert
            Assert.AreEqual("abc&l=en&pl=d", act);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetDetailResult_GameIdIsInvalid_ThrowException(string gameId)
        {
            // Arrange

            // Act & Assert
            Assert.Throws<ResultCoreException>(new TestDelegate(() => result.GetDetailResult(gameId, "d")));
        }

        [Test]
        public void GetDetailResult_Success_AddMoreInfo()
        {
            // Arrange
            _newGui.CreateNewId().Returns("abc");
            var response = new Response
            {
                Result = new Result()
                {
                    Seq = _newGui.CreateNewId(),
                    ErrorCode = "0",
                    Url = "abc"
                }
            };
            _client.UploadString<Request, Response>(Arg.Any<string>(), Arg.Any<Request>())
                .Returns(response);

            // Act
            var act = result.GetResult("D", "en-US");

            // Assert
            Assert.AreEqual("abc&l=en&pl=d", act);
        }
    }
}