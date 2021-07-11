using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dal.Abstract
{
    public interface IRedisRepository
    {
        Task<bool> Create(Sensor model);

        Task<bool> Update(Sensor model);

        Task<bool> Delete(string id);

        Task<Sensor> Get(string id);

        Task<bool> BulkCreate(List<Sensor> models);

    }
}
