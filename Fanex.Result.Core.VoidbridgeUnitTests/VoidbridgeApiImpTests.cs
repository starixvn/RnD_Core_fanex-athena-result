namespace Fanex.Result.Core.VoidbridgeUnitTests
{
    using NUnit.Framework;
    using Voidbridge;
    [TestFixture]
    public class VoidbridgeApiImpTests
    {
        [Test]
        public void GetBetHistoryUrl()
        {
            // Arrange
            VoidbridgeApi voidbridgeApi = new VoidbridgeApiImp();
            var custId = "1242426";
            var gamePlayId = "5";

            // Act
            voidbridgeApi.GetBetHistoryUrl(custId, gamePlayId);
        }
    }
}