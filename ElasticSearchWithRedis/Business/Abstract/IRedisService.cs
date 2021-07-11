using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Abstract
{
    public interface IRedisService
    {
        Task<IResult<bool>> BulkCreate(List<Sensor> model);
        Task<IResult<bool>> Create(Sensor model);
        Task<IResult<bool>> Delete(string id);
        Task<IResult<bool>> Update(Sensor model);
        Task<IResult<Sensor>> Get(string id);
    }
}
