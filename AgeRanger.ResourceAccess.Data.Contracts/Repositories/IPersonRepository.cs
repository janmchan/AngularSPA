using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.ResourceAccess.Data.Contracts.Models;

namespace AgeRanger.ResourceAccess.Data.Contracts.Repositories
{
    public interface IPersonRepository : IReadWriteRepository<IPerson, int>
    {
    }
}
