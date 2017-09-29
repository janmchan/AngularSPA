using AgeRanger.ResourceAccess.Data.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Contracts
{
    public interface IDataProvider: IDisposable
    {
        Task<IDataReader> ExecuteReader(string query, DbParameter[] parameters = null);
		Task<int> ExecuteNonQuery<IDataAccessObject>(string query, IDataAccessObject entity, DbParameter[] parameters = null);

		Task<object> ExecuteScalar(string query, DbParameter[] parameters = null);
        void CreateAndOpen();
        DbParameter CreateParameter();

        DbCommand Command { get; set; }
        DbConnection Connection { get; set; }

    }
}
