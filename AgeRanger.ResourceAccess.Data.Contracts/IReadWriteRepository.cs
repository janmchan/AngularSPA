using AgeRanger.ResourceAccess.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Contracts
{
    public interface IReadWriteRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : class
    {
        Task<TKey> Add(TEntity newEntity);
		Task Set(TEntity updatedEntity);
    }
}
