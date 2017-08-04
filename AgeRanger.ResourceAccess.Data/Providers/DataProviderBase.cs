using System;
using System.Data;
using System.Data.Common;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.ResourceAccess.Data.Contracts;
using Newtonsoft.Json;
using System.Linq;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Providers
{
	public abstract class DataProviderBase
	{
		private IMessageFinder _message;
		public DataProviderBase(IMessageFinder message)
		{
			_message = message;
		}
		public virtual async Task<IDataReader> ExecuteReader(string query, DbParameter[] parameters = null)
		{
			return await ExecuteCommand(query, command => command.ExecuteReaderAsync(),parameters);
		}
		public virtual async Task<int> ExecuteNonQuery<T>(string query, T entity, DbParameter[] parameters = null)
		{
			if (typeof(T).GetInterfaces().Contains(typeof(IAuditable)))
			{
				await Audit<T>(entity);
			}
			return await ExecuteCommand(query, command => command.ExecuteNonQueryAsync(), parameters);
		}
		public virtual async Task<object> ExecuteScalar(string query, DbParameter[] parameters = null)
		{
			return await ExecuteCommand(query, command => command.ExecuteScalarAsync(), parameters);
		}

		public virtual DbParameter CreateParameter()
		{
			return Command.CreateParameter();
		}

		public virtual DbCommand Command { get; set; }
		public virtual DbConnection Connection { get; set; }
		public virtual void Dispose()
		{
			Command = null;
			Connection = null;
		}

		protected virtual async Task<T> ExecuteCommand<T>(string query,
													   Func<DbCommand, Task<T>> commandAction,
													   IDbDataParameter[] parameters,
													   CommandType commandType = CommandType.Text)
		{
			if (Command == null)
			{
				throw new NullReferenceException(_message.Find(MessageKey.UnopenedProvider));
			}
			Command.CommandText = query;
			Command.CommandType = commandType;
			if (parameters != null) Command.Parameters.AddRange(parameters);
			return await commandAction(Command);
		}

		private Task<int> Audit<T>(T dao)
		{
			var auditQuery = "INSERT INTO Audit (TableName, Data, TaskId, TaskName, CreatedAt, CreatedBy)  VALUES(@tableName, @data, @taskId, @taskName, @createdAt, @createdBy)";
			var auditProperties = dao as IAuditable;

			var tableName = CreateParameter();
			tableName.ParameterName = "tableName";
			tableName.Value = typeof(T).Name;

			var data = CreateParameter();
			data.ParameterName = "data";
			data.Value = JsonConvert.SerializeObject(dao);

			var taskId = CreateParameter();
			taskId.ParameterName = "taskId";
			taskId.Value = auditProperties.TaskId.ToString();

			var taskName = CreateParameter();
			taskName.ParameterName = "taskName";
			taskName.Value = auditProperties.TaskName;

			var createdAt = CreateParameter();
			createdAt.ParameterName = "createdAt";
			createdAt.Value = auditProperties.CreatedAt;

			var createdBy = CreateParameter();
			createdBy.ParameterName = "createdBy";
			createdBy.Value = auditProperties.CreatedBy;

			var auditParameters = new DbParameter[] { tableName, data, createdAt, taskId, taskName, createdBy };
			return ExecuteCommand(auditQuery, command => command.ExecuteNonQueryAsync(), auditParameters);
		}


	}
}
