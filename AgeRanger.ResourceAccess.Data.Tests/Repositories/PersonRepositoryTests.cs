using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.ResourceAccess.Data.Contracts;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using System.Data.Common;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;
using AgeRanger.ResourceAccess.Data.Models;
using AgeRanger.Core.Tests;

namespace AgeRanger.ResourceAccess.Data.Tests.Repositories
{
    [TestClass]
    public class PersonRepositoryTests: TestBaseHelper
    {
        private Mock<DbCommand> _mockCommand;
        private Mock<IDataProvider> _mockProvider;
        private Mock<IDataProviderFactory> _mockProviderFactory;
        private Mock<IMapper<IDataReader, List<IPerson>>> _mockMapper;
        private DbParameter[] _resultParameters;
        [TestInitialize]
        public void Initialize()
        {
            _mockCommand = new Mock<DbCommand>();
            var mockParameter = new Mock<DbParameter>();
            _mockProvider = new Mock<IDataProvider>();
			_mockProviderFactory = new Mock<IDataProviderFactory>();
			mockParameter.SetupAllProperties();
			_mockProvider.SetupAllProperties();
            _mockProvider.Setup(x => x.Command).Returns(_mockCommand.Object);
            //Creates a new instance of DbParameter everytime
            _mockProvider.Setup(x => x.CreateParameter()).Returns(() => CreateParameter());
			//Provides a way to determin if the input was properly mapped
			_mockProvider.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<DbParameter[]>()))
					.Callback<string, IPerson, DbParameter[]>((query, personp, param) => _resultParameters = param)
					.ReturnsAsync(() => 0);
			_mockProviderFactory.Setup(x => x.CreateDataProvider()).Returns(_mockProvider.Object);
			_mockMapper = new Mock<IMapper<IDataReader, List<IPerson>>>();
            
        }

      
        [TestMethod]
        public async Task Add_SuppliedWithParameters_ParametersProperlyAdded()
        {
            var testAge = 20;
            var testFirstName = "First Name";
            var testLastName = "Last Name";
			var person = new Person()
            {
                Age = testAge,
                FirstName = testFirstName,
                LastName = testLastName
            };
			
			var sut = new PersonRepository(_mockProviderFactory.Object, _mockMapper.Object);
            await sut.Add(person);
            Assert.AreEqual(3, _resultParameters.Count());

            var list = _resultParameters.ToList();
            Assert.AreEqual(testFirstName, list.Find(x => x.ParameterName == "FirstName").Value);
            Assert.AreEqual(testLastName, list.Find(x => x.ParameterName == "LastName").Value);
            Assert.AreEqual(testAge, list.Find(x => x.ParameterName == "Age").Value);
        }
        [TestMethod]
        public void Set_SuppliedWithParameters_ParametersProperlyAdded()
        {
            var testId = 99;
            var testAge = 50;
            var testFirstName = "Unang Ngalan";
            var testLastName = "Huling Ngalan";
            var person = new Person()
            {
                Id = testId,
                Age = testAge,
                FirstName = testFirstName,
                LastName = testLastName
            };

            var sut = new PersonRepository(_mockProviderFactory.Object, _mockMapper.Object);
            sut.Set(person);
            Assert.AreEqual(4, _resultParameters.Length);
            var list = _resultParameters.ToList();
            Assert.AreEqual(testFirstName, list.Find(x => x.ParameterName =="FirstName").Value);
            Assert.AreEqual(testLastName, list.Find(x => x.ParameterName == "LastName").Value);
            Assert.AreEqual(testAge, list.Find(x => x.ParameterName == "Age").Value);
            Assert.AreEqual(testId, list.Find(x => x.ParameterName == "Id").Value);
        }
    }
}
