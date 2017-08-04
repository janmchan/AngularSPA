using System;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.Core.Contracts;

namespace AgeRanger.Core.Messages
{
    public class MessageReader: IMessageReader, ISingletonDependency
    {
        
        
        public string ReadMessage()
        {   //Did not set in web.config to avoid circular dependency
            string messageLocation = Constants.MessageFilePath;
            return System.IO.File.ReadAllText(messageLocation.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory));
        }
    }
}
