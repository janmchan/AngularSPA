using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Core.Contracts.Messages
{
    public enum MessageKey
    {
        TestResult = 0,
        UnknownProviderType,
        UnopenedProvider,
        MissingConnectionString,
        AddUserFail,
        GetUserFail,
        PutUserFail,
        PersonDoesNotExist
    }
}
