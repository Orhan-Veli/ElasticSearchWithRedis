using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Concrete;
using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
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
        private readonly IElasticClient _client;
        private readonly string _indexName;
        public ElasticRepository(IConfiguration configuration,IElasticClient client)
        {
            _indexName = configuration["elasticsearchserver:indexName"].ToString();
            _client = client;         
        }
        public async Task<bool> Create(MachineConnectionInformation model)
        {           
           var response = await _client.CreateAsync(model, q => q.Index(_indexName).Id(model.SensorId));
            return response.IsValid;        
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _client.DeleteAsync<MachineConnectionInformation>(id, x=>x.Index(_indexName));
            return response.IsValid;
        }

        public async Task<MachineConnectionInformation> Get(Guid id)
        {
            var response = await _client.GetAsync<MachineConnectionInformation>(id, q => q.Index(_indexName));
            return response.Source;
        }

        public async Task<List<MachineConnectionInformation>> GetAll()
        {
            var response = await _client.SearchAsync<MachineConnectionInformation>(q => q.Index(_indexName).Scroll("5m"));
            return response.Documents.ToList();
        }

        public async Task<MachineConnectionInformation> Update(MachineConnectionInformation model)
        {
            var response = await _client.UpdateAsync<MachineConnectionInformation>(model.SensorId,a=>a.Index(_indexName).Doc(model));
            return model;
        }
    }
}
