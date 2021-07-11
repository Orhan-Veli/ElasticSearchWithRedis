using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dal.Concrete
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IDistributedCache _distributed;
        public RedisRepository(IDistributedCache distributed)
        {
            _distributed = distributed;
        }
        public async Task<bool> BulkCreate(List<Sensor> models)
        {
            var data = JsonConvert.SerializeObject(models);
            await _distributed.SetStringAsync("model", data);
            return true;
        }

        public async Task<bool> Create(Sensor model)
        {
            var data = JsonConvert.SerializeObject(model);
            var dataByte = Encoding.UTF8.GetBytes(data);          
            await _distributed.SetAsync("model", dataByte);                      
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await _distributed.RemoveAsync(id.ToString());
            return true;
        }

        public async Task<Sensor> Get(string id)
        {
            string result = await _distributed.GetStringAsync("model");
            return JsonConvert.DeserializeObject<Sensor>(result);           
        }

        public async Task<bool> Update(Sensor model)
        {
            string result = await _distributed.GetStringAsync(model.SensorId.ToString());
            var batiAgil = JsonConvert.DeserializeObject<Sensor>(result);
            batiAgil.Name = model.Name;
            batiAgil.SensorId = model.SensorId;
            var data = JsonConvert.SerializeObject(batiAgil);
            var dataByte = Encoding.UTF8.GetBytes(data);
            await _distributed.SetAsync("model", dataByte);
            return true;
        }
    }
}
