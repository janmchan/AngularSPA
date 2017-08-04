using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.ResourceAccess.Data.Contracts;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;

namespace AgeRanger.ResourceAccess.Data.Providers
{
    public class DataProviderFactory: IDataProviderFactory, ISingletonDependency
    {
        private readonly IMsSqlProvider _msSqlProvider;
        private readonly ISqliteProvider _sqlLiteProvider;
        private readonly IMessageFinder _messageFinder;
        public DataProviders DataProviderType { get; set;  }

        public DataProviderFactory(IMsSqlProvider msSqlProvider,
                                    ISqliteProvider sqlLiteProvider,
                                    IMessageFinder messageFinder)
        {
            DataProviderType = DataProviders.SQLite;
            _msSqlProvider = msSqlProvider;
            _sqlLiteProvider = sqlLiteProvider;
            _messageFinder = messageFinder;
        }

        public IDataProvider CreateDataProvider()
        {
            switch (DataProviderType)
            {
                case DataProviders.SQLite:
                    return _sqlLiteProvider;
                case DataProviders.MsSql:
                    return _msSqlProvider;
                default:
                    throw new ArgumentOutOfRangeException(_messageFinder.Find(MessageKey.UnknownProviderType));
            }
        }
    }
}
