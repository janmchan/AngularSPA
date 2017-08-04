using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRanger.Controllers;
using System.Web.Mvc;

namespace AgeRanger.Tests.Controllers
{
    /// <summary>
    /// Summary description for PersonMgmtControllerTests
    /// </summary>
    [TestClass]
    public class PersonMgmtControllerTests
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Add_InvokeView_ViewReturned()
        {
            var controller = new PersonMgmtController();

            // Act
            ViewResult result = controller.Add() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Update_InvokeView_ViewReturned()
        {
            var controller = new PersonMgmtController();

            // Act
            ViewResult result = controller.Update() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Index_InvokeView_ViewReturned()
        {
            var controller = new PersonMgmtController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
