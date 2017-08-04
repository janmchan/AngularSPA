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
            var query = string.Concat("insert into Person ",
                "(FirstName, LastName, Age) ",
                "values(@FirstName, @LastName, @Age);");
            var parameters = new DbParameter[3];
            _dataProvider.Open();
            using (_dataProvider)
            {
                var firstNameParam = _dataProvider.CreateParameter();
                firstNameParam.Value = newEntity.FirstName;
                firstNameParam.ParameterName = nameof(newEntity.FirstName);
                parameters[0] = firstNameParam;
                var lastNameParam = _dataProvider.CreateParameter();
                lastNameParam.Value = newEntity.LastName;
                lastNameParam.ParameterName = nameof(newEntity.LastName);
                lastNameParam.Value = newEntity.LastName;
                parameters[1] = lastNameParam;
                var ageParam = _dataProvider.CreateParameter();
                ageParam.ParameterName = nameof(newEntity.Age);
                ageParam.Value = newEntity.Age;
                parameters[2] = ageParam;

                var result = await _dataProvider.ExecuteNonQuery(query, newEntity, parameters);
                return result;
            }
        }

        public async Task<IEnumerable<IPerson>> Find()
        {
            var query = "Select Id, FirstName, LastName, Age from Person;";
            _dataProvider.Open();
            using (_dataProvider)
            {
                var result = await _dataProvider.ExecuteReader(query);
                return _mapper.Map(result);
            }
        }

        public async Task<IPerson> FindById(int id)
        {
            var query = "Select Id, FirstName, LastName, Age from Person where Id = @Id;";
            var parameters = new List<DbParameter>();
            _dataProvider.Open();
            using (_dataProvider)
            {
                var idParameter = _dataProvider.CreateParameter();
                idParameter.Value = id;
                idParameter.ParameterName = "Id";
                parameters.Add(idParameter);

                var result = await _dataProvider.ExecuteReader(query, parameters.ToArray());
                var mappedResult = _mapper.Map(result);
                return mappedResult.Count > 0 ? mappedResult[0] : new Person();
            }
            
        }
        

        public async Task Set(IPerson updatedEntity)
        {
            var query = string.Concat("Update Person set FirstName = @FirstName, LastName = @LastName, Age=@Age "
                        ," where Id = @Id");
            var parameters = new List<DbParameter>();
            _dataProvider.Open();
            using (_dataProvider)
            {
                var idParameter = _dataProvider.CreateParameter();
                idParameter.Value = updatedEntity.Id;
                idParameter.ParameterName = "Id";
                parameters.Add(idParameter);
                var firstNameParam = _dataProvider.CreateParameter();
                firstNameParam.Value = updatedEntity.FirstName;
                firstNameParam.ParameterName = nameof(updatedEntity.FirstName);
                parameters.Add(firstNameParam);
                var lastNameParam = _dataProvider.CreateParameter();
                lastNameParam.Value = updatedEntity.LastName;
                lastNameParam.ParameterName = nameof(updatedEntity.LastName);
                lastNameParam.Value = updatedEntity.LastName;
                parameters.Add(lastNameParam);
                var ageParam = _dataProvider.CreateParameter();
                ageParam.ParameterName = nameof(updatedEntity.Age);
                ageParam.Value = updatedEntity.Age;
                parameters.Add(ageParam);

                await _dataProvider.ExecuteNonQuery(query, updatedEntity, parameters.ToArray());
                
            }
        }
    }
}
