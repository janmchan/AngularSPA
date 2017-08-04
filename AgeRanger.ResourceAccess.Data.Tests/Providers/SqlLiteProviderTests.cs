using AgeRanger.Core.Contracts.Config;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.ResourceAccess.Data.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AgeRanger.ResourceAccess.Data.Tests.Providers
{
    [TestClass]
    public class SqlLiteProviderTests
    {
        [TestMethod]
        public void Ctor_SuccessfullyCreated()
        {
            var mockMessager = new Mock<IMessageFinder>();
            var mockConfig = new Mock<IConfig>();
            var sut = new SqliteProvider(mockMessager.Object, mockConfig.Object);
            Assert.IsNotNull(sut);
        }
    }
}
