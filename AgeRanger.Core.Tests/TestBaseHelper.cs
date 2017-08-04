using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using AgeRanger.Extensions;
using System.Net.Http;

namespace AgeRanger.Core.Tests
{
    public abstract class TestBaseHelper
    {
        public async Task<string> ReadContent(JsonStringResult result)
        {
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var content = httpResponse.Content as StringContent;

            // Assert
            Assert.IsNotNull(content);
            return await content.ReadAsStringAsync();
        }

        public DbParameter CreateParameter()
        {
            var mockParam = new Mock<DbParameter>();
            mockParam.SetupAllProperties();
            return mockParam.Object;
        }
    }
}
