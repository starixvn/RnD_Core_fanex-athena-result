namespace Fanex.Result.CockFighting.Tests
{
    using System.Net;
    using Core.CockFighting;
    using Core.CockFighting.Exceptions;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class CockFightingAPITests
    {
        private Config _config;

        [SetUp]
        public void Setup()
        {
            _config = new Config { ExtendedAPI = "1", AuthorizedAPI = "http://www.google.com", SiteId = "1", Vendor = "!1", VendorKey = "1" };
        }

        [Test]
        public void GetResultToken_AuthorizedAPIIsNullOrEmpty_ThrowException()
        {
            // Arrange
            _config.AuthorizedAPI = string.Empty;
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Wrong Config!Not found CockFightingAuthorizedAPI key !"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_ExtendedAPIIsNullOrEmpty_ThrowException()
        {
            // Arrange
            _config.ExtendedAPI = string.Empty;
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Wrong Config! Not found CockFightingExtendedAPI key !"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_SiteIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            _config.SiteId = string.Empty;
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Wrong Config! Not found CockFightingSiteId key !"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_VendorIsNullOrEmpty_ThrowException()
        {
            // Arrange
            _config.Vendor = string.Empty;
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Wrong Config! Not found CockFightingVendor key !"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_VendorKeyIsNullOrEmpty_ThrowException()
        {
            // Arrange
            _config.VendorKey = string.Empty;
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Wrong Config! Not found CockFightingVendorKey key!"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_CustomerIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken(""));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Invalid customer ID"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_LanguageCodeIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1", string.Empty));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Invalid language code"),
                testDelegate);
        }

        [Test]
        public void GetResultToken_ConfigIsValid_ResultIsValid_GetResultToken()
        {
            // Arrange
            var client = Substitute.For<WebClient>();
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.GetResultToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>(),
                testDelegate);
        }

        [Test]
        public void ExtendToken_AuthorizedAPIIsNullOrEmpty_ThrowException()
        {
            // Arrange
            _config.AuthorizedAPI = string.Empty;
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.ExtendToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Wrong Config!Not found CockFightingAuthorizedAPI key !"),
                testDelegate);
        }

        [Test]
        public void ExtendToken_CustomerIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.ExtendToken(""));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>().And
                    .Message.EqualTo("Cock Fighting Result API Exception. Exception: Invalid customer ID"),
                testDelegate);
        }

        [Test]
        public void ExtendToken_ValidConfig_ThrowException()
        {
            // Arrange
            var api = new CockFightingAPI(_config);

            // Act
            var testDelegate = new TestDelegate(() => api.ExtendToken("1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<CockFightingException>(),
                testDelegate);
        }

        [Test]
        public void Ctor_Always_GetConfigFromConfigFile()
        {
            // Arrange
            var api = new CockFightingAPI();

            // Act
            var config = CockFightingAPI.GetConfig();

            // Assert
            Assert.AreEqual("1", config.AuthorizedAPI);
            Assert.AreEqual("2", config.ExtendedAPI);
            Assert.AreEqual("3", config.SiteId);
            Assert.AreEqual("4", config.Vendor);
            Assert.AreEqual("5", config.VendorKey);
        }
    }
}