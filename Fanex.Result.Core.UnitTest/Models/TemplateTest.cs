namespace Fanex.Result.Core.UnitTest.Models
{
    using System;
    using Fanex.Result.Core.Models;
    using NUnit.Framework;

    /// <summary>
    /// Apply unit testing for Template class.
    /// </summary>
    [TestFixture]
    public class TemplateTest
    {
        private ITemplate _template = Template.Instance;

        /// <summary>
        /// Testing for empty the template name.
        /// </summary>
        [Test]
        public void GetRazorTemplate_EmptyTemplate_ThrowsException()
        {
            // Arrange
            var templateName = string.Empty;
            var expected = "templateName";

            // ACT
            var exception = Assert.Catch<Exception>(() => _template.GetRazorTemplate(templateName));

            // Assert
            StringAssert.Contains(expected, exception.Message, "Throw exception when template name is empty.");
        }

        /// <summary>
        /// Testing for not empty the template name, but not exist on the views folder.
        /// </summary>
        [Test]
        public void GetRazorTemplate_NotEmptyTemplate_ThrowsException()
        {
            // Arrange
            var templateName = "NotExistTemplateOnViews";
            var expected = "was not found";

            // ACT
            var exception = Assert.Catch<Exception>(() => _template.GetRazorTemplate(templateName));

            // Assert
            Assert.That(exception.Message, Is.StringContaining(expected), "Input template name, but not found on view folder.");
        }

        /// <summary>
        /// Testing for exist a template on the views folder.
        /// </summary>
        /// <param name="templateName">Template Name.</param>
        [TestCase("Badminton")]
        [TestCase("BaseBall")]
        [TestCase("BasketBall")]
        [TestCase("Billiard")]
        [TestCase("Hockey")]
        [TestCase("LiveCasino")]
        [TestCase("NumberGame")]
        [TestCase("OutRight")]
        [TestCase("Parlay")]
        [TestCase("Racing")]
        [TestCase("Soccer")]
        [TestCase("Tennis")]
        [TestCase("VSHRacing")]
        [TestCase("VSSpeedWay")]
        [TestCase("VSTennis")]
        public void GetRazorTemplate_ExistTemplate_HaveContent(string templateName)
        {
            var result = _template.GetRazorTemplate(templateName);

            Assert.IsNotNullOrEmpty(result, "Method should have returned a content of a template.");
        }
    }
}