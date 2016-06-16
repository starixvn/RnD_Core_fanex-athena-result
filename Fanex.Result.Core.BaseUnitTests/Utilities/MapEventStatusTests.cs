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
        public void MapEventStatusString_FromEventStatusIsNotInternal_ReturnsIsNotInternal(string status)
        {
            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(status, Arg.Any<bool>(), Arg.Any<bool>());

            // Assert
            var expectedEventStatus = "Internal";
            Assert.AreNotEqual(expectedEventStatus, eventStatus);
        }

        public void MapEventStatusString_FromEventStatusIsInternalVisibleInternalStatusIsFalse_ReturnsNotInternal()
        {
            // Arrange
            var eventstatus = "Internal";
            var visibleInternalEventStatus = false;

            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(eventstatus, Arg.Any<bool>(), visibleInternalEventStatus);

            // Assert
            var expectedEventStatus = "Internal";
            Assert.AreNotEqual(expectedEventStatus, eventStatus);
        }

        [TestCase("internal")]
        [TestCase("InTeRnal")]
        public void MapEventStatusString_FromEventStatusIsInternalAndVisibleInternalStatusIsTrue_ReturnsInternal(string status)
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
        public void MapEventStatusString_FromEventStatusIsNotInternalOrVisibleInternalStatusIsFalseAndPendingIsTrue_ReturnsPending(
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

        [Test]
        public void MapEventStatusString_FromEventStatusIsInternalAndVisibleInternalEventStatusIsFalseAndPendingIsFalse_ReturnsRunning()
        {
            // Arrange
            var eventStatus = "Internal";
            var visibleInternalEventStatus = false;
            var isPending = false;

            // Act
            var status = MapEventStatus.MapEventStatusString(eventStatus, isPending, visibleInternalEventStatus);

            // Assert
            var expectedStatus = "Running";
            Assert.AreEqual(expectedStatus, status);
        }

        [Test]
        public void MapEventStatusString_FromEventStatusIsNotInternalAndPendingIsFalse_ReturnsRunning()
        {
            // Arrange
            var eventStatus = "thirdParty";
            var isPending = false;

            // Act
            var status = MapEventStatus.MapEventStatusString(eventStatus, isPending, Arg.Any<bool>());

            // Assert
            var expectedStatus = "Thirdparty";
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