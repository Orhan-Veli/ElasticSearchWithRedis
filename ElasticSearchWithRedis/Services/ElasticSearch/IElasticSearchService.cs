using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Services
{
    public interface IElasticSearchService
    {
        Task CheckIndex(string indexName);
    }
}
