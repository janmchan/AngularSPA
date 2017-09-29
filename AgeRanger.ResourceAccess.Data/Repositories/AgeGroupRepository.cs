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
using Dapper;
using System.Linq;

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
            var query = "Select Id, MinAge, MaxAge, Description from AgeGroup";
            _dataProvider.CreateAndOpen();
            using (_dataProvider.Connection)
            {
                return await _dataProvider.Connection.QueryAsync<AgeGroup>(query);
            }
        }

        public async Task<IAgeGroup> FindById(int id)
        {
            var query = "Select Id, MinAge, MaxAge, Description from AgeGroup where Id = @Id";
			_dataProvider.CreateAndOpen();
			using (_dataProvider.Connection)
			{
				var result = await _dataProvider.Connection.QueryAsync<AgeGroup>(query, new { Id = id });
				return result.Any() ? result.Single() : new AgeGroup();
			}
        }
    }
}
