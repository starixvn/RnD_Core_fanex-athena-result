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
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(15)]
        public void SecondMapEventStatusString_IsPending_EventStatusIsClosed_ReturnsPending(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Closed", string.Empty);

            // Assert
            Assert.AreEqual("Result Pending", actualStatus);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(15)]
        public void SecondMapEventStatusString_IsPending_EventStatusIsRunning_ReturnsPending(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Running", string.Empty);

            // Assert
            Assert.AreEqual("Result Pending", actualStatus);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(15)]
        public void SecondMapEventStatusString_IsPending_EventStatusIsInternal_ReturnsPending(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Internal", string.Empty);

            // Assert
            Assert.AreEqual("Result Pending", actualStatus);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(15)]
        public void SecondMapEventStatusString_IsPending_EventStatusIsPostponed_ReturnsPending(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Postponed", string.Empty);

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

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsRunning_NotRefundHT_ReturnUnderSettlement(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Running", string.Empty);

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsInternal_ReturnUnderSettlement(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Internal", "Refund HT");

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsInternal_NotRefundHT_ReturnUnderSettlement(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Internal", string.Empty);

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsPostponed_ReturnUnderSettlement(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Postponed", "Refund HT");

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsPostponed_NotRefundHT_ReturnUnderSettlement(byte settlementStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "Postponed", string.Empty);

            // Assert
            Assert.AreEqual("Under Settlement", actualStatus);
        }

        [TestCase(4, "Abandoned 1H;", "Abandoned 1H")]
        [TestCase(6, "Abandoned 1H;", "Abandoned 1H")]
        [TestCase(8, "Abandoned 1H;", "Abandoned 1H")]
        [TestCase(10, "Abandoned 1H;", "Abandoned 1H")]
        [TestCase(12, "Abandoned 1H;", "Abandoned 1H")]
        [TestCase(14, "Abandoned 1H;", "Abandoned 1H")]
        [TestCase(4, "Abandoned 2H;", "Abandoned 2H")]
        [TestCase(6, "Abandoned 2H;", "Abandoned 2H")]
        [TestCase(8, "Abandoned 2H;", "Abandoned 2H")]
        [TestCase(10, "Abandoned 2H;", "Abandoned 2H")]
        [TestCase(12, "Abandoned 2H;", "Abandoned 2H")]
        [TestCase(14, "Abandoned 2H;", "Abandoned 2H")]
        [TestCase(4, "Refund FT;", "Refund FT")]
        [TestCase(6, "Refund FT;", "Refund FT")]
        [TestCase(8, "Refund FT;", "Refund FT")]
        [TestCase(10, "Refund FT;", "Refund FT")]
        [TestCase(12, "Refund FT;", "Refund FT")]
        [TestCase(14, "Refund FT;", "Refund FT")]
        [TestCase(4, "Refund HT;", "Refund HT")]
        [TestCase(6, "Refund HT;", "Refund HT")]
        [TestCase(8, "Refund HT;", "Refund HT")]
        [TestCase(10, "Refund HT;", "Refund HT")]
        [TestCase(12, "Refund HT;", "Refund HT")]
        [TestCase(14, "Refund HT;", "Refund HT")]
        [TestCase(4, "Refund;", "Refund")]
        [TestCase(6, "Refund;", "Refund")]
        [TestCase(8, "Refund;", "Refund")]
        [TestCase(10, "Refund;", "Refund")]
        [TestCase(12, "Refund;", "Refund")]
        [TestCase(14, "Refund;", "Refund")]
        [TestCase(4, "", "Completed")]
        [TestCase(6, "", "Completed")]
        [TestCase(8, "", "Completed")]
        [TestCase(10, "", "Completed")]
        [TestCase(12, "", "Completed")]
        [TestCase(14, "", "Completed")]
        public void SecondMapEventStatusString_IsUnderSettlementOrSystemSettlement_EventStatusIsCompleted_ReturnExpectedStatus(
          byte settlementStatus, string betStatus, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(settlementStatus, "completed", betStatus);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Running")]
        [TestCase("Internal")]
        [TestCase("Postponed")]
        public void SecondMapEventStatusString_IsUndecided_EventStatusIsNotCompleted_BetStatusContainsRefundHT_ReturnExpectedStatus(string status)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(2, status, "Refund HT");

            // Assert
            Assert.AreEqual("Refund HT", actualStatus);
        }

        [TestCase("Running")]
        [TestCase("Internal")]
        [TestCase("Postponed")]
        public void SecondMapEventStatusString_IsNotUnderSettlementOrSystemSettlement_EventStatusIsNotCompleted_BetStatusContainsRefundHT_ReturnExpectedStatus(string status)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, status, "Refund HT");

            // Assert
            Assert.AreEqual("Refund HT", actualStatus);
        }

        [TestCase("Running", "Running")]
        [TestCase("Internal", "Running")]
        [TestCase("Postponed", "Postponed")]
        public void SecondMapEventStatusString_IsUndecided_EventStatusIsNotCompleted_ReturnExpectedStatus(string status, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(2, status, string.Empty);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Running", "Running")]
        [TestCase("Internal", "Running")]
        [TestCase("Postponed", "Postponed")]
        public void SecondMapEventStatusString_IsNotUnderSettlementOrSystemSettlement_EventStatusIsNotCompleted_ReturnExpectedStatus(string status, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, status, string.Empty);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Abandoned 1H;", "Abandoned 1H")]
        [TestCase("Abandoned 2H;", "Abandoned 2H")]
        [TestCase("Refund FT;", "Refund FT")]
        [TestCase("Refund HT;", "Refund HT")]
        [TestCase("Refund;", "Refund")]
        [TestCase("", "Completed")]
        public void SecondMapEventStatusString_IsUndecided_EventStatusIsCompleted_ReturnExpectedStatus(
           string betStatus, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(2, "completed", betStatus);

            // Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestCase("Abandoned 1H;", "Abandoned 1H")]
        [TestCase("Abandoned 2H;", "Abandoned 2H")]
        [TestCase("Refund FT;", "Refund FT")]
        [TestCase("Refund HT;", "Refund HT")]
        [TestCase("Refund;", "Refund")]
        [TestCase("", "Completed")]
        public void SecondMapEventStatusString_IsNotUnderSettlementOrSystemSettlement_EventStatusIsCompleted_ReturnExpectedStatus(
            string betStatus, string expectedStatus)
        {
            // Act
            var actualStatus = MapEventStatus.MapEventStatusString(0, "completed", betStatus);

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