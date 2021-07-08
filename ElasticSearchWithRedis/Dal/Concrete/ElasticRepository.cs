using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Concrete;
using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using ElasticSearchWithRedis.Services;
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
        private readonly string _indexName;
        public ElasticRepository(IConfiguration configuration)
        {
            _indexName = configuration.GetSection("elasticsearch:indexName").ToString();
            _client = new ElasticSearchService(configuration);           
        }
        public async Task<bool> Create(MachineConnectionInformation model)
        {           
           var response = await _client.CreateInstance().CreateAsync(model, q => q.Index(_indexName).Id(model.SensorId));
            return response.IsValid;        
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _client.CreateInstance().DeleteAsync<MachineConnectionInformation>(id, x=>x.Index(_indexName));
            return response.IsValid;
        }

        public async Task<MachineConnectionInformation> Get(Guid id)
        {
            var response = await _client.CreateInstance().GetAsync<MachineConnectionInformation>(id, q => q.Index(_indexName));
            return response.Source;
        }

        public async Task<List<MachineConnectionInformation>> GetAll()
        {
            var response = await _client.CreateInstance().SearchAsync<MachineConnectionInformation>(q => q.Index(_indexName).Scroll("5m"));
            return response.Documents.ToList();
        }

        public async Task<MachineConnectionInformation> Update(MachineConnectionInformation model)
        {
            var response = await _client.CreateInstance().UpdateAsync<MachineConnectionInformation>(model.SensorId,a=>a.Index(_indexName).Doc(model));
            return model;
        }
    }
}
