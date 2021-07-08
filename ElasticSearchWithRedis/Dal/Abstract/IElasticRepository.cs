using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dal.Abstract
{
    public interface IElasticRepository<T> 
    {
        Task<bool> Create(T model);

        Task<T> Update(T model);

        Task<bool> Delete(Guid id);

        Task<T> Get(Guid id);

        Task<List<T>> GetAll();
    }
}
