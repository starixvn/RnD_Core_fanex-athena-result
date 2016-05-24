namespace Fanex.Result.Core.Base.Tests.Utilities
{
    using Base.Resources;
    using Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class MapEventStatusTests
    {
        [TestCase("running")]
        [TestCase("pending")]
        public void MapEventStatusString_FromInvalidEventStatus_ReturnsIsNotInternal(string status)
        {
            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(status, Arg.Any<bool>(), Arg.Any<bool>());

            // Assert
            var expectedEventStatus = "Internal";
            Assert.AreNotEqual(expectedEventStatus, eventStatus);
        }

        public void MapEventStatusString_FromVisibleInternalStatusIsFalse_ReturnsNotInternal()
        {
            // Arrange
            var visibleInternalEventStatus = true;

            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(Arg.Any<string>(), Arg.Any<bool>(), visibleInternalEventStatus);

            // Assert
            var expectedEventStatus = "Internal";
            Assert.AreNotEqual(expectedEventStatus, eventStatus);
        }

        [TestCase("internal")]
        [TestCase("InTeRnal")]
        public void MapEventStatusString_FromValidEventStatusAndVisibleInternalStatusIsTrue_ReturnsInternal(string status)
        {
            // Arrange
            var visibleInternalEventStatus = true;

            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(status, Arg.Any<bool>(), visibleInternalEventStatus);

            // Assert
            var expectedEventStatus = "Internal";
            Assert.AreEqual(expectedEventStatus, eventStatus);
        }

        [TestCase("rUnNing", true)]
        [TestCase("pending", true)]
        [TestCase("internal", false)]
        public void MapEventStatusString_FromInvalidEventStatusOrVisibleInternalStatusIsFalseAndPendingIsTrue_ReturnsPending(
            string status,
            bool visibleInternalEventStatus)
        {
            // Arrange
            var isPending = true;

            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(status, isPending, visibleInternalEventStatus);

            // Assert
            var expectedEventStatus = "Pending";
            Assert.AreEqual(expectedEventStatus, eventStatus);
        }

        [TestCase("Internal", "Running")]
        [TestCase("thirdParty", "Thirdparty")]
        public void MapEventStatusString_FromEventStatusAndVisibleInternalEventStatusIsFalseAndPendingIsFalse_ReturnsExpectedStatus(
                string eventStatus, string expectedStatus)
        {
            // Arrange
            var visibleInternalEventStatus = false;
            var isPending = false;

            // Act
            var status = MapEventStatus.MapEventStatusString(eventStatus, isPending, visibleInternalEventStatus);

            // Assert
            Assert.AreEqual(expectedStatus, status);
        }

        [Test]
        public void GetLogResource_FromNullKey_ReturnsHyphen()
        {
            // Arrange
            string key = null;

            // Act
            var logResource = MapEventStatus.GetLogResource(key);

            // Assert
            Assert.AreEqual("-", logResource);
        }

        [Test]
        public void GetLogResource_FromEmptyKey_ReturnsHyphen()
        {
            // Arrange
            string key = string.Empty;

            // Act
            var logResource = MapEventStatus.GetLogResource(key);

            // Assert
            Assert.AreEqual("-", logResource);
        }

        [TestCase("third Party", "Third party")]
        [TestCase("HELLO WORLD", "Hello world")]
        public void GetLogResource_FromKeyNotInLangResource_ReturnsUpperCaseFirst(string key, string expectedLogSource)
        {
            // Act
            var logResource = MapEventStatus.GetLogResource(key);

            // Assert
            Assert.AreEqual(expectedLogSource, logResource);
        }

        [Test]
        public void GetLogResource_FromKeyInLangResource_ReturnsFromLangResource()
        {
            // Arrange
            var key = "Away";

            // Act
            var logResource = MapEventStatus.GetLogResource(key);

            // Assert
            var expectedLogSource = LangResource.Away;
            Assert.AreEqual(expectedLogSource, logResource);
        }
    }
}