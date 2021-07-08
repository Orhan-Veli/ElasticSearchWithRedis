using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Abstract
{
    public interface IElasticService
    {
        Task<IResult<bool>> Create(string indexName,MachineConnectionInformation model);

        Task<IResult<MachineConnectionInformation>> Update(MachineConnectionInformation model);
        Task<IResult<MachineConnectionInformation>> Get(string indexName,Guid id);
        Task<IResult<bool>> Delete(string indexName,Guid id);
  
    }
}
