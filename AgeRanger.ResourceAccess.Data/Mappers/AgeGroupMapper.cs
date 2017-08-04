using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Models;

namespace AgeRanger.ResourceAccess.Data.Mappers
{

    public class AgeGroupMapper: IMapper<IDataReader, List<IAgeGroup>>, ISingletonDependency
    {
        public List<IAgeGroup> Map(IDataReader from)
        {
            var list = new List<IAgeGroup>();

            if (from == null)
                return list;
            while (from.Read())
            {
                var item = new AgeGroup();
                if (!from.IsDBNull(0))
                    item.Id = Convert.ToInt32(from["Id"]);
                if (!from.IsDBNull(1))
                    item.MinAge = Convert.ToInt32(from["MinAge"]);
                if (!from.IsDBNull(2))
                    item.MaxAge = Convert.ToInt32(from["MaxAge"]);
                if (!from.IsDBNull(3))
                    item.Description = from["Description"].ToString(); ;

                list.Add(item);
            }
            return list;
        }
    }
}
