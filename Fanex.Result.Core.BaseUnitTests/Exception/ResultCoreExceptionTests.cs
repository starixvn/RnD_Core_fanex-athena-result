using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Fanex.Result.Core.Base.Exception;
using Fanex.Result.Core.Base.ViewModels;
using NUnit.Framework;

namespace Fanex.Result.Core.Base.Tests.Exception
{
    [TestFixture]
    internal class ResultCoreExceptionTests
    {
        [Test]
        public void Message_Always_GetExactMessage()
        {
            // Arrange
            var exception = new ResultCoreException("Message");

            // Act
            var actualMessage = exception.Message;

            // Assert
            Assert.AreEqual("Result.Core Exception. Exception: Message", actualMessage);
        }
    }
}