using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Extensions
{
    public interface IElasticSearchService
    {
        Task CheckIndex(string indexName);
    }
}
