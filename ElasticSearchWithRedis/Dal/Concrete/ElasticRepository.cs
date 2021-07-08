using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Concrete;
using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using ElasticSearchWithRedis.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dal.Concrete
{
    public class ElasticRepository : IElasticRepository<MachineConnectionInformation>
    {
        public ElasticSearchService _client;
        public ElasticRepository(IConfiguration configuration)
        {           
            _client = new ElasticSearchService(configuration);           
        }
        public async Task<bool> Create(string indexName, MachineConnectionInformation model)
        {
           
           var response = await _client.CreateInstance().CreateAsync(model, q => q.Index(indexName).Id(model.SensorId));
            return response.IsValid;
        
        }

        public Task Delete(string indexName, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MachineConnectionInformation> Get(string indexName, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MachineConnectionInformation>> GetAll(string indexName)
        {
            throw new NotImplementedException();
        }

        public Task<MachineConnectionInformation> Update(string indexName, MachineConnectionInformation model)
        {
            throw new NotImplementedException();
        }
    }
}
