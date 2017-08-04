using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using AgeRanger.ResourceAccess.Data.Mappers;

namespace AgeRanger.ResourceAccess.Data.Tests.Mappers
{
    [TestClass]
    public class PersonMapperTests
    {
        Mock<IDataReader> _mockReader;
        [TestInitialize]
        public void Initialize()
        {
           _mockReader = new Mock<IDataReader>();

            bool readToggle = true;

            _mockReader.Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => readToggle = false);
            
        }
        [TestMethod]
        public void Map_ReturnsListOfRecords()
        {
            int id = 123;
            var firstName = "first name";
            var lastName = "Last Name";
            int age = 88;
            _mockReader.Setup(row => row["Id"])
               .Returns(id);
            _mockReader.Setup(row => row["FirstName"])
                .Returns(firstName);
            _mockReader.Setup(row => row["LastName"])
                .Returns(lastName);
            _mockReader.Setup(row => row["Age"])
                .Returns(age);
            var sut = new PersonMapper();
            var result = sut.Map(_mockReader.Object);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(id, result[0].Id);
            Assert.AreEqual(firstName, result[0].FirstName);
            Assert.AreEqual(lastName, result[0].LastName);
            Assert.AreEqual(age, result[0].Age);
        }
    }
}
