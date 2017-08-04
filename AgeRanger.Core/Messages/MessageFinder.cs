
using System.Collections.Generic;
using AgeRanger.Core.Contracts.Messages;
using Newtonsoft.Json;
using AgeRanger.Core.Contracts;

namespace AgeRanger.Core.Messages
{
    public class MessageFinder: IMessageFinder, ISingletonDependency
    {
        private readonly Dictionary<MessageKey, string> _messages; 
        public MessageFinder(IMessageReader messageReader)
        {
            var messageJson = messageReader.ReadMessage();
            _messages  = JsonConvert.DeserializeObject<Dictionary<MessageKey, string>>(messageJson);
        }
        public string Find(MessageKey key)
        {
            if (_messages.ContainsKey(key))
            {
                return _messages[key];
            }
            throw new KeyNotFoundException(Constants.KeyDoesNotExists);
        }
    }
}
