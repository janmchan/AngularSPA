using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using AgeRanger.ResourceAccess.Data.Mappers;

namespace AgeRanger.ResourceAccess.Data.Tests.Mappers
{
    [TestClass]
    public class AgeGroupMapperTests
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
            int minAge = 1;
            int maxAge = 99;
            string desc = "Old fellow";
            _mockReader.Setup(row => row["Id"])
               .Returns(id);
            _mockReader.Setup(row => row["MinAge"])
                .Returns(minAge);
            _mockReader.Setup(row => row["MaxAge"])
                .Returns(maxAge);
            _mockReader.Setup(row => row["Description"])
                .Returns(desc);
            var sut = new AgeGroupMapper();
            var result = sut.Map(_mockReader.Object);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(id, result[0].Id);
            Assert.AreEqual(minAge, result[0].MinAge);
            Assert.AreEqual(maxAge, result[0].MaxAge);
            Assert.AreEqual(desc, result[0].Description);
        }
    }
}
