using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Contracts
{
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> FindById(TKey id);
        Task<IEnumerable<TEntity>> Find();
    }
}
