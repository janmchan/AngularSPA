using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.ResourceAccess.Data.Contracts;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;
using AgeRanger.ResourceAccess.Data.Contracts.Repositories;
using AgeRanger.ResourceAccess.Data.Mappers;
using AgeRanger.ResourceAccess.Data.Models;
using Dapper;

namespace AgeRanger.ResourceAccess.Data.Repositories
{
    public class PersonRepository : IPersonRepository, IDependency
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMapper<IDataReader, List<IPerson>> _mapper;
        public PersonRepository(IDataProviderFactory factory,
            IMapper<IDataReader, List<IPerson>> mapper)
        {
            _dataProvider = factory.CreateDataProvider();
            _mapper = mapper;
        }

        public async Task<int> Add(IPerson newEntity)
        {
            var query = "insert into Person (FirstName, LastName, Age) values(@FirstName, @LastName, @Age)";
			_dataProvider.CreateAndOpen();
			using (var connection = _dataProvider.Connection)
			{
				return await connection.ExecuteAsync(query, newEntity);
			}
        }

        public async Task<IEnumerable<IPerson>> Find()
        {
            var query = "Select Id, FirstName, LastName, Age from Person;";
			_dataProvider.CreateAndOpen();
            using (var connection = _dataProvider.Connection)
			{
				return await connection.QueryAsync<Person>(query);
			}
        }

        public async Task<IPerson> FindById(int id)
        {
            var query = "Select Id, FirstName, LastName, Age from Person where Id = @Id;";
			_dataProvider.CreateAndOpen();
			using (var connection = _dataProvider.Connection)
			{
				var personResult = await connection.QueryAsync<Person>(query, new { Id = id });
				return personResult.Any() ? personResult.First() : new Person();
			}
            
        }
        

        public async Task Set(IPerson updatedEntity)
        {
            var query = "Update Person set FirstName = @FirstName, LastName = @LastName, Age=@Age where Id = @Id";
			_dataProvider.CreateAndOpen();
			using (var connection = _dataProvider.Connection)
			{
				var personResult = await connection.ExecuteAsync(query, updatedEntity);
			}
        }
    }
}
