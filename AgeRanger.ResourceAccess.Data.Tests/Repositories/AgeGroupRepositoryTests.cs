using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.Core.Tests;
using AgeRanger.ResourceAccess.Data.Contracts;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;
using AgeRanger.ResourceAccess.Data.Models;
using AgeRanger.ResourceAccess.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Tests.Repositories
{
    /// <summary>
    /// Summary description for AgeGroupRepositoryTests
    /// </summary>
    [TestClass]
    public class AgeGroupRepositoryTests: TestBaseHelper
    {
        private Mock<DbCommand> _mockCommand;
        private Mock<IDataProvider> _mockProvider;
        private Mock<IDataProviderFactory> _mockProviderFactory;
        private Mock<IMapper<IDataReader, List<IAgeGroup>>> _mockMapper;
        private DbParameter[] _resultParameters;
        private List<IAgeGroup> _ageGroupList;
        [TestInitialize]
        public void Initialize()
        {
            _mockCommand = new Mock<DbCommand>();
            var mockParameter = new Mock<DbParameter>();
            mockParameter.SetupAllProperties();
            _mockProvider = new Mock<IDataProvider>();
            _mockProvider.SetupAllProperties();
            _mockProvider.Setup(x => x.Command).Returns(_mockCommand.Object);
            //Creates a new instance of DbParameter everytime
            _mockProvider.Setup(x => x.CreateParameter()).Returns(() => CreateParameter());
            //Provides a way to determine if the input was properly mapped
			_mockProvider.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<AgeGroup>(), It.IsAny<DbParameter[]>()))
					.Callback<string, AgeGroup, DbParameter[]>((query, AgeGroup, param) => _resultParameters = param);
			_mockProviderFactory = new Mock<IDataProviderFactory>();
            _mockProviderFactory.Setup(x => x.CreateDataProvider()).Returns(_mockProvider.Object);
            _mockMapper = new Mock<IMapper<IDataReader, List<IAgeGroup>>>();
            _ageGroupList = new List<IAgeGroup>();
            _ageGroupList.Add(new AgeGroup()
            {
                Description = "A",
                MaxAge = 0,
                MinAge = 10
            });
            _ageGroupList.Add(new AgeGroup()
            {
                Description = "B",
                MaxAge = 11,
                MinAge = 20
            });
            _mockMapper.Setup(x => x.Map(It.IsAny<IDataReader>())).Returns(_ageGroupList);
        }

        [TestMethod]
        public async Task Find_ReturnsAllResult()
        {
            var sut = new AgeGroupRepository(_mockProviderFactory.Object, _mockMapper.Object);
            var results = await sut.Find();
            Assert.AreEqual(_ageGroupList.Count, results.Count());
        }
    }
}
