using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.ResourceAccess.Data.Contracts;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Contracts.Providers;
using AgeRanger.ResourceAccess.Data.Contracts.Repositories;
using AgeRanger.ResourceAccess.Data.Mappers;
using AgeRanger.ResourceAccess.Data.Models;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Repositories
{
    public class AgeGroupRepository : IAgeGroupRepository, IDependency
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMapper<IDataReader, List<IAgeGroup>> _mapper;
        public AgeGroupRepository(IDataProviderFactory factory,
                                    IMapper<IDataReader, List<IAgeGroup>> mapper)
        {
            _dataProvider = factory.CreateDataProvider();
            _mapper = mapper;
        }
        public async Task<IEnumerable<IAgeGroup>> Find()
        {
            var query = "Select Id, MinAge, MaxAge, Description from AgeGroup;";
            _dataProvider.Open();
            using (_dataProvider)
            {
                var result = await _dataProvider.ExecuteReader(query);
                return _mapper.Map(result);
            }
        }

        public async Task<IAgeGroup> FindById(int id)
        {
            var query = string.Concat("Select Id, MinAge, MaxAge, Description from AgeGroup "
                                ,"where Id = @Id;");
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
                return mappedResult.Count > 0 ? mappedResult[0] : new AgeGroup();
            }
        }
    }
}
