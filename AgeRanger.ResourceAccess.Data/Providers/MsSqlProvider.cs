using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;
using AgeRanger.Core.Config;
using AgeRanger.Core.Contracts.Config;

namespace AgeRanger.ResourceAccess.Data.Providers
{
    public class MsSqlProvider : DataProviderBase, IMsSqlProvider, IDependency
    {
        private readonly string connectionString;
        public MsSqlProvider(IMessageFinder message, IConfig config) : base(message)
        {
            connectionString = config.AgeRangerDb;
        }
        public void Open()
        {
            var connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
            Connection = connection;
            Command.Connection = Connection;
            if(connection.State != ConnectionState.Open) Connection.Open();
        }
    }
}
