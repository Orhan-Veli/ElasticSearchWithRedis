using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dal.Abstract
{
    public interface IElasticRepository<T> 
    {
        Task<bool> Create(string indexName,T model);

        Task<T> Update(string indexName, T model);

        Task Delete(string indexName, Guid id);

        Task<T> Get(string indexName,Guid id);

        Task<List<T>> GetAll(string indexName);
    }
}
