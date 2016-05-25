namespace Fanex.Result.Core.Base.Tests.Extension
{
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class StringValueAttributeTests
    {
        [TestCase("Hello")]
        [TestCase("World")]
        public void ToString_FromValue_ReturnsSameValue(string value)
        {
            // Arrang
            var stringValueAttribute = new StringValueAttribute(value);

            // Act
            var attrValue = stringValueAttribute.ToString();

            // Assert
            Assert.AreEqual(value, attrValue);
        }
    }
}