using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Business.Utilities.Concrete;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Concrete
{
    public class RedisService : IRedisService
    {
        private readonly IRedisRepository _redisRepository;
        public RedisService(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }
        public async Task<IResult<bool>> BulkCreate(List<Sensor> model)
        {
            if (model == null || model.Count == 0) return new Result<bool>(false);
            await _redisRepository.BulkCreate(model);
            return new Result<bool>(true);
        }

        public async Task<IResult<bool>> Create(Sensor model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.SensorId)) return new Result<bool>(false);
            await _redisRepository.Create(model);
            return new Result<bool>(true);
        }

        public async Task<IResult<bool>> Delete(string id)
        {
            if(string.IsNullOrEmpty(id)) return new Result<bool>(false);
            await _redisRepository.Delete(id);
            return new Result<bool>(true);
        }

        public async Task<IResult<Sensor>> Get(string id)
        {
            if(string.IsNullOrEmpty(id)) return new Result<Sensor>(false);
            var result = await _redisRepository.Get(id);
            if(result == null) return new Result<Sensor>(false);
            return new Result<Sensor>(true, result);
        }


        public async Task<IResult<bool>> Update(Sensor model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.SensorId)) return new Result<bool>(false);
            await _redisRepository.Update(model);
            return new Result<bool>(true);
        }
    }
}
