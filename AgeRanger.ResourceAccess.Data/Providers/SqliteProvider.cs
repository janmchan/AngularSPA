using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Config;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;

namespace AgeRanger.ResourceAccess.Data.Providers
{
    public class SqliteProvider: DataProviderBase, ISqliteProvider, IDependency
    {
        //Default
        private readonly string _connectionString;

        public SqliteProvider(IMessageFinder message, IConfig config) : base(message)
        {
            _connectionString = config.AgeRangerDb;
        }

        public void CreateAndOpen()
        {
            var connection  = new SQLiteConnection(_connectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory));
            Command = new SQLiteCommand(connection);
            Connection = connection;
            if(connection.State != ConnectionState.Open) Connection.Open();
        }
       
    }
}
