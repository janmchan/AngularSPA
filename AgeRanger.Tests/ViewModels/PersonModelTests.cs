using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRanger.ViewModels;

namespace AgeRanger.Tests.ViewModels
{
    /// <summary>
    /// Summary description for PersonModelTests
    /// </summary>
    [TestClass]
    public class PersonModelTests
    {
        private IQueryable<IAgeGroup> _ageGroups;
        private string[] ageGroups = new string[] { "GroupA", "GroupC", "GroupC" };
        [TestInitialize]
        public void Initialize()
        {
            var ageGroupList = new List<IAgeGroup>();
            ageGroupList.Add(new AgeGroup() { MaxAge = 40, MinAge = 1, Description = ageGroups[0] });
            ageGroupList.Add(new AgeGroup() { MaxAge = 60, MinAge = 41, Description = ageGroups[1] });
            ageGroupList.Add(new AgeGroup() { MaxAge = 80, MinAge = 61, Description = ageGroups[2] });
            _ageGroups = ageGroupList.AsQueryable();
        }

        [TestMethod]
        public void PersonModel_AgeGroupB_PersonAgeGroupCorrect()
        {
            int age = 50;
            string firstName = "First Name";
            string lastName = "Last Name";
            int id = 99;
            var person = new Person()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };
            var sut = new PersonModel(person, _ageGroups);

            Assert.AreEqual(ageGroups[1], sut.AgeGroup);

        }
        [TestMethod]
        public void PersonModel_AgeNotAssigned_LowestAgeGroupReturned()
        {
            string firstName = "First Name";
            string lastName = "Last Name";
            int id = 99;
            var person = new Person()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            var sut = new PersonModel(person, _ageGroups);

            Assert.AreEqual(ageGroups[0], sut.AgeGroup);

        }
        [TestMethod]
        public void PersonModel_AgeOutOfRange_NoDefaultAgegroupProvided()
        {
            int age = 99;
            string firstName = "First Name";
            string lastName = "Last Name";
            int id = 99;
            var person = new Person()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };
            var sut = new PersonModel(person, _ageGroups);

            Assert.IsNull(sut.AgeGroup);

        }
    }
}
