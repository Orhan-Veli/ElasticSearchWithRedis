using ElasticSearchWithRedis.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _client;

        public ElasticSearchService(IConfiguration configuration)
        {
            _configuration = configuration;           
            _client = CreateInstance();
        }
        public ElasticClient CreateInstance()
        {
            string host = _configuration["elasticsearchserver:Host"];
            string userName = _configuration["elasticsearchserver:Username"];
            string password = _configuration["elasticsearchserver:Password"];           
            var settings = new ConnectionSettings(new Uri(host));
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                settings.BasicAuthentication(userName, password);
            return new ElasticClient(settings);
        }

        public async Task CheckIndex(string indexName)
        {
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (!anyy.Exists)
            {
                var response = await _client.Indices.CreateAsync(indexName, ci => ci.Index(indexName).MachineMapping().Settings(s => s.NumberOfShards(3).NumberOfReplicas(1)));
            }
        }
    }
}
