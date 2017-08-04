using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRanger.Mappers;
using AgeRanger.ViewModels;

namespace AgeRanger.Tests.Mappers
{
    [TestClass]
    public class PersonModelToPersonMapperTests
    {
        [TestMethod]
        public void Map_SuppliedValues_ValuesProperlySet()
        {
            int testAge = 50;
            string firstName = "First Name";
            string lastName = "Last Name";
            int id = 99;
            var personModel = new PersonModel()
            {
                Age = testAge, FirstName = firstName, LastName = lastName, Id = id
            };
            
            var sut = new PersonModelToPersonMapper();
            var result = sut.Map(personModel);

            Assert.AreEqual(testAge, result.Age);
            Assert.AreEqual(firstName, result.FirstName);
            Assert.AreEqual(lastName, result.LastName);
            Assert.AreEqual(id, result.Id);

        }
    }
}
