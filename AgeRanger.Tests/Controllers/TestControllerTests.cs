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
    public class TestControllerTests
    {
       
        [TestMethod]
        public void Index_InvokeView_ViewReturned()
        {
            var controller = new TestController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
