namespace Fanex.Result.Core.UnitTest.Models
{
    using System;
    using System.Reflection;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.UnitTest.Utilities;
    using NUnit.Framework;

    /// <summary>
    /// Apply unit testing for Resource class.
    /// </summary>
    [TestFixture]
    public class ResourcesTest
    {
        private IResource _resource = Resources.Instance;

        [TestCase("ABC")]
        public void GetLanguage_NotExistLanguageCode_ReturnNewLangResources(string languageCode)
        {
            var result = _resource.GetLanguage(languageCode);
            var expected = result.ID;
            Assert.IsNullOrEmpty(expected);
        }

        /// <summary>
        /// Get language resource.
        /// </summary>
        [Test]
        public void GetResources_EmptyLanguageCode_ThrowException()
        {
            var languageCode = string.Empty;

            var exception = Assert.Throws<TargetInvocationException>(() => Helper.RunStaticMethod(typeof(Resources), "GetResources", new object[1] { languageCode }));

            Assert.IsInstanceOf<Exception>(exception.InnerException);
        }
    }
}