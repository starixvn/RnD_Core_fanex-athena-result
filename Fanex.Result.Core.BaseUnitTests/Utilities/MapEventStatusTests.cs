namespace Fanex.Result.Core.Base.Tests.Utilities
{
    using Base.Resources;
    using Base.Utilities;
    using NUnit.Framework;

    [TestFixture]
    public class MapEventStatusTests
    {
        [TestCase("running")]
        [TestCase("pending")]
        public void MapEventStatusString_IsPending_ReturnsPending(string status)
        {
            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(status, true);

            // Assert
            Assert.AreEqual("Pending", eventStatus);
        }

        [TestCase("internal")]
        [TestCase("InTeRnal")]
        public void MapEventStatusString_FromEventStatusIsInternal_ReturnsRunning(string status)
        {
            // Act
            var eventStatus = MapEventStatus.MapEventStatusString(status, false);

            // Assert
            var expectedEventStatus = "Running";
            Assert.AreEqual(expectedEventStatus, eventStatus);
        }

        [TestCase("completed", "Completed")]
        [TestCase("running", "Running")]
        public void MapEventStatusString_NotPendingAndAnotherStatus_ReturnsExpectedStatus(string status, string expectedStatus)
        {
            // Arrange
            var isPending = false;

            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(status, isPending);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        public void SecondMapEventStatusString_IsPending_ReturnsPending(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "completed", string.Empty);

            // Assert
            Assert.AreEqual("Result Pending", actualStatus);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsRunning_ReturnUnderSettlement(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Running", "Refund HT");

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase("Running")]
        [TestCase("Internal")]
        [TestCase("Postponed")]
        public void SecondMapEventStatusString_IsUnderSettlement_EventStatusIsNotCompleted_ReturnUnderSettlement(string status)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(4, status, string.Empty);

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase("Running")]
        [TestCase("Internal")]
        [TestCase("Postponed")]
        public void SecondMapEventStatusString_IsNotUnderSettlement_EventStatusIsNotCompleted_BetStatusContainsRefundHT_ReturnExpectedStatus(string status)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, status, "Refund HT");

            // Assert
            Assert.AreEqual("Refund HT", actualStatus);
        }

        [TestCase("Running", "Running")]
        [TestCase("Internal", "Running")]
        [TestCase("Postponed", "Postponed")]
        public void SecondMapEventStatusString_IsNotUnderSettlement_EventStatusIsNotCompleted_ReturnExpectedStatus(string status, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, status, string.Empty);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Refund;Abandoned 1H;", "Abandoned 1H")]
        [TestCase("Refund;Abandoned 2H;", "Abandoned 2H")]
        [TestCase("Refund;Refund FT;", "Refund FT")]
        [TestCase("Refund HT;Refund;", "Refund HT")]
        [TestCase("Refund;", "Refund")]
        public void SecondMapEventStatusString_IsUndecided_EventStatusIsCompleted_BetStatusHasRefundOrAbandoned_ReturnExpectedStatus(
            string betStatus, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(2, "completed", betStatus);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Refund;Abandoned 1H;", "Abandoned 1H")]
        [TestCase("Refund;Abandoned 2H;", "Abandoned 2H")]
        [TestCase("Refund;Refund FT;", "Refund FT")]
        [TestCase("Refund HT;Refund;", "Refund HT")]
        [TestCase("Refund;", "Refund")]
        public void SecondMapEventStatusString_IsUnderSettlement_EventStatusIsCompleted_BetStatusHasRefundOrAbandoned_ReturnExpectedStatus(
           string betStatus, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(4, "completed", betStatus);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Refund;Abandoned 1H;", "Abandoned 1H")]
        [TestCase("Refund;Abandoned 2H;", "Abandoned 2H")]
        [TestCase("Refund;Refund FT;", "Refund FT")]
        [TestCase("Refund HT;Refund;", "Refund HT")]
        [TestCase("Refund;", "Refund")]
        public void SecondMapEventStatusString_IsNormal_EventStatusIsCompleted_BetStatusHasRefundOrAbandoned_ReturnExpectedStatus(
            string betStatus, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, "completed", betStatus);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Running", "Running")]
        [TestCase("Internal", "Running")]
        [TestCase("Postponed", "Postponed")]
        public void SecondMapEventStatusString_IsNormal_EventStatusIsNotCompleted_ReturnExpectedStatus(
           string status, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, status, string.Empty);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
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