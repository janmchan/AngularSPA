using System;
using AgeRanger.Core.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRanger.Core.Contracts.Messages;
using Moq;
using MSTestExtensions;
using System.Collections.Generic;

namespace AgeRanger.Core.Tests
{
    [TestClass]
    public class MessageFinderTests
    {
        Mock<IMessageReader> _messageReaderMock;
        [TestInitialize]
        public void Initialize()
        {
            _messageReaderMock = new Mock<IMessageReader>();
            _messageReaderMock.Setup(x => x.ReadMessage()).Returns(@"{
             ""TestResult"": ""Test Result"",
              ""UnknownProviderType"": ""The Provider specified is unknown""
            }
            ");
        }
        [TestMethod]
        public void Find_FindExistingKey_ReturnsRightValue()
        {
            var sut = new MessageFinder(_messageReaderMock.Object);
            Assert.AreEqual("Test Result", sut.Find(MessageKey.TestResult));
        }
        [TestMethod]
        public void Find_FindNonExistingKey_ThrowsException()
        {
            
            var sut = new MessageFinder(_messageReaderMock.Object);
            ThrowsAssert.Throws<KeyNotFoundException>(() => sut.Find(MessageKey.PersonDoesNotExist));
        }
    }
}
