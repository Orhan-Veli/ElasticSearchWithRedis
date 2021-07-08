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

        public async Task<bool> Delete(string indexName, Guid id)
        {
            var response = await _client.CreateInstance().DeleteAsync<MachineConnectionInformation>(id, x=>x.Index(indexName));
            return response.IsValid;
        }

        public async Task<MachineConnectionInformation> Get(string indexName, Guid id)
        {
            var response = await _client.CreateInstance().GetAsync<MachineConnectionInformation>(id, q => q.Index(indexName));
            return response.Source;
        }

        public async Task<List<MachineConnectionInformation>> GetAll(string indexName)
        {
            var response = await _client.CreateInstance().SearchAsync<MachineConnectionInformation>(q => q.Index(indexName).Scroll("5m"));
            return response.Documents.ToList();
        }

        public async Task<MachineConnectionInformation> Update(string indexName, MachineConnectionInformation model)
        {
            var response = await _client.CreateInstance().UpdateAsync<MachineConnectionInformation>(model.SensorId,a=>a.Index(indexName).Doc(model));
            return model;
        }
    }
}
