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

    public class PersonMapper: IMapper<IDataReader, List<IPerson>>, ISingletonDependency
    {
        public List<IPerson> Map(IDataReader from)
        {
            var list = new List<IPerson>();

            if (from == null)
                return list;
            while (from.Read())
            {
                var item = new Person();
                if (!from.IsDBNull(0))
                    item.Id = Convert.ToInt32(from["Id"]);
                if (!from.IsDBNull(1))
                    item.FirstName = from["FirstName"].ToString();
                if (!from.IsDBNull(2))
                    item.LastName = from["LastName"].ToString();
                if (!from.IsDBNull(3))
                    item.Age = Convert.ToInt32(from["Age"]);

                list.Add(item);
            }
            return list;
        }
    }
}
