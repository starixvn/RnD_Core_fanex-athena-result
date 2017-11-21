namespace Fanex.Result.VoidBridge.Tests
{
    using Core.Voidbridge;
    using Core.Voidbridge.Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class VoidBridgeApiImpTests
    {
        [Test]
        public void GetBetHistoryUrl_CustIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new VoidbridgeApiImp();

            // Act
            var testDelegate = new TestDelegate(() => api.GetBetHistoryUrl("", ""));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<VoidbridgeException>().And
                    .Message.EqualTo("Voidbridge Result API Exception. Exception: custId is null or empty!"),
                testDelegate);
        }

        [Test]
        public void GetBetHistoryUrl_GamePlayIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new VoidbridgeApiImp();

            // Act
            var testDelegate = new TestDelegate(() => api.GetBetHistoryUrl("1", ""));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<VoidbridgeException>().And
                    .Message.EqualTo("Voidbridge Result API Exception. Exception: gamePlayId is null or empty!"),
                testDelegate);
        }

        [Test]
        public void GetBetHistoryUrl_SiteIdIsNullOrEmpty_ThrowException()
        {
            // Arrange
            var api = new VoidbridgeApiImp();

            // Act
            var testDelegate = new TestDelegate(() => api.GetBetHistoryUrl("1", "1"));

            // Assert
            var ex = Assert.Throws(
                Is.TypeOf<VoidbridgeException>().And
                    .Message.EqualTo("Voidbridge Result API Exception. Exception: siteId argument must have value or SiteId in Web.config file!"),
                testDelegate);
        }

        [Test]
        public void GetBetHistoryUrl_ValidData_ThrowException()
        {
            // Arrange
            var api = new VoidbridgeApiImp();

            // Act
            var url = api.GetBetHistoryUrl("1", "1", "en-US", "1");

            // Assert
            Assert.NotNull(url);
        }
    }
}