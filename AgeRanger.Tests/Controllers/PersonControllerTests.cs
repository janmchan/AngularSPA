using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRanger.Controllers;
using AgeRanger.Core.Contracts.Logging;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Contracts.Repositories;
using Moq;
using AgeRanger.ResourceAccess.Data.Models;
using AgeRanger.ViewModels;
using Newtonsoft.Json;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.Core.Tests;

namespace AgeRanger.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTests: TestBaseHelper
    {
        Mock<IPersonRepository> _personRepoMock;
        Mock<IAgeGroupRepository> _ageGroupRepoMock;
        Mock<ILogger> _logger;
        private Mock<IMessageFinder> _messageFinderMock;
        private Mock<IMapper<PersonModel, IPerson>>  _mapperMock;
        Person _samplePerson1;
        Person _samplePerson2;
        Person _samplePerson3;
        PersonModel _samplePersonModel1;
        PersonModel _samplePersonModel2;
        PersonModel _samplePersonModel3;
        private const string Group1 = "group1";
        private const string Group2 = "group2";
        private const string Group3 = "group3";
        [TestInitialize]
        public void Initialize()
        {
            _personRepoMock = new Mock<IPersonRepository>();
            _ageGroupRepoMock = new Mock<IAgeGroupRepository>();
            _mapperMock = new Mock<IMapper<PersonModel, IPerson>>();
            _logger = new Mock<ILogger>();
            _messageFinderMock = new Mock<IMessageFinder>();
            var samplePeople = new List<IPerson>();
            var sampleAgeGroups = new List<IAgeGroup>();
           var ageGroup1 = new AgeGroup() {MinAge = 1, MaxAge = 10, Description = Group1};
            var ageGroup2 = new AgeGroup() { MinAge = 11, MaxAge = 20, Description = Group2 };
            var ageGroup3 = new AgeGroup() { MinAge = 21, MaxAge = 30, Description = Group3 };
            _samplePersonModel1 = new PersonModel()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                Age = 25,
                Id = 0
            };
            _samplePersonModel2 = new PersonModel()
            {
                FirstName = null,
                LastName = "Last Name",
                Age = 25,
                Id = 0
            };
            _samplePersonModel3 = new PersonModel()
            {
                FirstName = "First Name",
                LastName = "",
                Age = 25,
                Id = 0
            };
            _samplePerson1 = new Person()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                Age = 25,
                Id = 0
            };
            _samplePerson2 = new Person()
            {
                FirstName = "Unang Ngalan",
                LastName = "Panghuling Ngalan",
                Age = 19,
                Id = 2
            };
            _samplePerson3 = new Person()
            {
                FirstName = "John",
                LastName = "Smith",
                Age = 1,
                Id = 3
            };
            samplePeople.Add(_samplePerson1);
            samplePeople.Add(_samplePerson2);
            samplePeople.Add(_samplePerson3);
            sampleAgeGroups.Add(ageGroup1);
            sampleAgeGroups.Add(ageGroup2);
            sampleAgeGroups.Add(ageGroup3);
            _personRepoMock.Setup(x => x.Find()).ReturnsAsync(samplePeople);
            _personRepoMock.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(_samplePerson3);
            _personRepoMock.Setup(x => x.Add(It.IsAny<IPerson>())).ReturnsAsync(99);
            _ageGroupRepoMock.Setup(x => x.Find()).ReturnsAsync(sampleAgeGroups);
			_mapperMock.Setup(x => x.Map(_samplePersonModel1)).Returns(_samplePerson1);
			_mapperMock.Setup(x => x.Map(_samplePersonModel2)).Returns(_samplePerson2);
			_mapperMock.Setup(x => x.Map(_samplePersonModel3)).Returns(_samplePerson3);
		}
        [TestMethod]
        public async Task Get_SetupWithTestData_ReturnsAllExpected()
        {
            // Arrange
            var sut = new PersonController(_personRepoMock.Object,
                _ageGroupRepoMock.Object, _logger.Object, _messageFinderMock.Object, _mapperMock.Object) {Request = new HttpRequestMessage()};
            sut.Request.SetConfiguration(new HttpConfiguration());
            // Act
            var deserializedResult = await sut.Get();
            //var stringResult = await ReadContent(result);
            //var deserializedResult = JsonConvert.DeserializeObject<PersonModel[]>(stringResult);
            Assert.AreEqual(3, deserializedResult.Count());

			var Person1= deserializedResult.ElementAt(0);
			var Person2 = deserializedResult.ElementAt(1);
			var Person3 = deserializedResult.ElementAt(2);


			Assert.AreEqual(_samplePerson1.FirstName, Person1.FirstName);
            Assert.AreEqual(_samplePerson1.LastName, Person1.LastName);
            Assert.AreEqual(_samplePerson1.Age, Person1.Age);
            Assert.AreEqual(Group3, Person1.AgeGroup);

            Assert.AreEqual(_samplePerson2.FirstName, Person2.FirstName);
            Assert.AreEqual(_samplePerson2.LastName, Person2.LastName);
            Assert.AreEqual(_samplePerson2.Age, Person2.Age);
            Assert.AreEqual(Group2, Person2.AgeGroup);

            Assert.AreEqual(_samplePerson3.FirstName, Person3.FirstName);
            Assert.AreEqual(_samplePerson3.LastName, Person3.LastName);
            Assert.AreEqual(_samplePerson3.Age, Person3.Age);
            Assert.AreEqual(Group1, Person3.AgeGroup);
        }
        [TestMethod]
        public async Task Post_SubmitUser_ReturnsSuccess()
        {
            // Arrange
            var sut = new PersonController(_personRepoMock.Object,
                _ageGroupRepoMock.Object, _logger.Object, _messageFinderMock.Object, _mapperMock.Object)
            { Request = new HttpRequestMessage() };
            sut.Request.SetConfiguration(new HttpConfiguration());
            // Act
            var result = await sut.Post(_samplePersonModel1);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task Post_SubmitUser_ReturnsFailure()
        {
            // Arrange
            _personRepoMock.Setup(x => x.Add(It.IsAny<IPerson>())).ReturnsAsync(0);
            var sut = new PersonController(_personRepoMock.Object,
                _ageGroupRepoMock.Object, _logger.Object, _messageFinderMock.Object, _mapperMock.Object)
            { Request = new HttpRequestMessage() };
            sut.Request.SetConfiguration(new HttpConfiguration());
            // Act
            var result = await sut.Post(_samplePersonModel1);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public async Task Put_UpdateUser_ReturnsSucceeds()
        {
            // Arrange
            _personRepoMock.Setup(x => x.Add(It.IsAny<IPerson>())).ReturnsAsync(0);
            var sut = new PersonController(_personRepoMock.Object,
                _ageGroupRepoMock.Object, _logger.Object, _messageFinderMock.Object, _mapperMock.Object)
            { Request = new HttpRequestMessage() };
            sut.Request.SetConfiguration(new HttpConfiguration());
            _samplePersonModel1.Id = 99;
            // Act
            var result = await sut.Put(_samplePersonModel1);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task Put_UpdateNonExistingUser_ReturnsFailed()
        {
            _personRepoMock.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(_samplePerson1);
            // Arrange
            _personRepoMock.Setup(x => x.Add(It.IsAny<IPerson>())).ReturnsAsync(0);
            var sut = new PersonController(_personRepoMock.Object,
                _ageGroupRepoMock.Object, _logger.Object, _messageFinderMock.Object, _mapperMock.Object)
            { Request = new HttpRequestMessage() };
            sut.Request.SetConfiguration(new HttpConfiguration());
            // Act
            var result = await sut.Put(_samplePersonModel1);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task Put_MissingRequiredUserFields_ReturnsFailed()
        {
            _personRepoMock.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(_samplePerson1);
            // Arrange

            _mapperMock.Setup(x => x.Map(_samplePersonModel1)).Returns(_samplePerson2);
            var sut = new PersonController(_personRepoMock.Object,
                _ageGroupRepoMock.Object, _logger.Object, _messageFinderMock.Object, _mapperMock.Object)
            { Request = new HttpRequestMessage() };
            sut.Request.SetConfiguration(new HttpConfiguration());
            // Act
            var result = await sut.Put(_samplePersonModel1);
            Assert.IsFalse(result.Success);
        }



    }
}
